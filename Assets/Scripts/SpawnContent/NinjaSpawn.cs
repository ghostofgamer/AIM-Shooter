using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnContent
{
    public class NinjaSpawn : AbstractSpawner
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private Collider _spawner;
        [SerializeField]private HitHandler _hitHandler;

        private List<ObjectPool<SlicedTarget>> _objectPools;
        private ObjectPool<Bomb> _bombPool;
        private float _delay = 1.5f;
        private float _force;
        private bool _spawnBomb;
        private int _randomIndex;
        private Vector3 _position;
        private Vector3 _randomTorque;

        private void Awake()
        {
            _spawner = GetComponent<Collider>();
            _objectPools = new List<ObjectPool<SlicedTarget>>();
            _bombPool = new ObjectPool<Bomb>(_bombPrefab.GetComponent<Bomb>(),15, Contaner);

            foreach (var prefab in _prefabs)
            {
                ObjectPool<SlicedTarget> pool = new ObjectPool<SlicedTarget>(prefab.GetComponent<SlicedTarget>(), 15, Contaner);
                pool.EnableAutoExpand();
                _objectPools.Add(pool);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _hitHandler.HitedBomb += StopSpawn;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _hitHandler.HitedBomb -= StopSpawn;
        }

        protected override IEnumerator SpawnTarget()
        {
            FriutDifficultySetting fruitDifficultySetting = DifficultySettings as FriutDifficultySetting;

            if (fruitDifficultySetting == null)
                yield break;

            yield return new WaitForSeconds(_delay);

            while (enabled)
            {
                _spawnBomb = Random.value < fruitDifficultySetting.SpawnBombChance;
                GameObject spawnedObject = null;

                if (_spawnBomb)
                {
                    if (_bombPool.TryGetObject(out Bomb target, _bombPrefab.GetComponent<Bomb>()))
                        spawnedObject = target.gameObject;
                }
                else
                {
                    _randomIndex = Random.Range(0, _objectPools.Count);

                    if (_objectPools[_randomIndex]
                        .TryGetObject(out SlicedTarget target, _prefabs[_randomIndex].GetComponent<SlicedTarget>()))
                        spawnedObject = target.gameObject;
                }

                if (spawnedObject != null)
                {
                    _position = new Vector3();
                    _position.x = Random.Range(_spawner.bounds.min.x, _spawner.bounds.max.x);
                    _position.y = Random.Range(_spawner.bounds.min.y, _spawner.bounds.max.y);
                    _position.z = Random.Range(_spawner.bounds.min.z, _spawner.bounds.max.z);
                    Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(fruitDifficultySetting.MinAngle, fruitDifficultySetting.MaxAngle));
                    spawnedObject.transform.position = _position;
                    spawnedObject.transform.rotation = rotation;

                    if (spawnedObject.GetComponent<SlicedTarget>())
                        spawnedObject.GetComponent<SlicedTarget>().Init(RecordCounter);
                    
                    _force = Random.Range(fruitDifficultySetting.MinForce, fruitDifficultySetting.MaxForce);
                    Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    spawnedObject.gameObject.SetActive(true);
                    spawnedObject.GetComponent<Rigidbody>().AddForce(spawnedObject.transform.up * _force, ForceMode.Impulse);

                    _randomTorque = new Vector3(
                        Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                        Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                        Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque)
                    );

                    spawnedObject.GetComponent<Rigidbody>().AddTorque(_randomTorque, ForceMode.Impulse);
                    yield return new WaitForSeconds(Random.Range(fruitDifficultySetting.MinDelay,
                        fruitDifficultySetting.MaxDelay));
                }
            }
        }
    }
}