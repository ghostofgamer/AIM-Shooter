using System.Collections;
using System.Collections.Generic;
using TargetContent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnContent
{
    public class NinjaSpawn : AbstractSpawner
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private Collider _spawner;
        [SerializeField] private HitHandler _hitHandler;

        private List<ObjectPool<SlicedTarget>> _objectPools;
        private ObjectPool<Bomb> _bombPool;
        private float _delay = 1.5f;
        private float _force;
        private bool _spawnBomb;
        private int _randomIndex;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _randomTorque;
        private GameObject _spawnedObject;
        private Rigidbody _rb;
        private WaitForSeconds _waitForSeconds; 

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_delay); 
            _spawner = GetComponent<Collider>();
            _objectPools = new List<ObjectPool<SlicedTarget>>();
            _bombPool = new ObjectPool<Bomb>(_bombPrefab.GetComponent<Bomb>(), 15, Contaner);

            foreach (var prefab in _prefabs)
            {
                ObjectPool<SlicedTarget> pool =
                    new ObjectPool<SlicedTarget>(prefab.GetComponent<SlicedTarget>(), 15, Contaner);
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

            yield return _waitForSeconds;
            
            while (enabled)
            {
                _spawnedObject = SpawnObject(fruitDifficultySetting, _spawner.bounds);

                if (_spawnedObject != null)
                {
                    _position = GetRandomPosition(_spawner.bounds);
                    _rotation = GetRandomRotation(fruitDifficultySetting);
                    _spawnedObject.transform.position = _position;
                    _spawnedObject.transform.rotation = _rotation;

                    if (_spawnedObject.GetComponent<SlicedTarget>())
                        _spawnedObject.GetComponent<SlicedTarget>().Init(RecordCounter);

                    _force = GetRandomForce(fruitDifficultySetting);
                    _rb = _spawnedObject.GetComponent<Rigidbody>();
                    _rb.velocity = Vector3.zero;
                    _rb.angularVelocity = Vector3.zero;
                    _spawnedObject.gameObject.SetActive(true);
                    _rb.AddForce(_spawnedObject.transform.up * _force, ForceMode.Impulse);
                    _randomTorque = GetRandomTorque(fruitDifficultySetting);
                    _rb.AddTorque(_randomTorque, ForceMode.Impulse);
                    yield return new WaitForSeconds(GetRandomDelay(fruitDifficultySetting));
                }
            }
        }

        private Vector3 GetRandomPosition(Bounds spawnerBounds)
        {
            return new Vector3(
                Random.Range(spawnerBounds.min.x, spawnerBounds.max.x),
                Random.Range(spawnerBounds.min.y, spawnerBounds.max.y),
                Random.Range(spawnerBounds.min.z, spawnerBounds.max.z)
            );
        }

        private float GetRandomForce(FriutDifficultySetting fruitDifficultySetting)
        {
            return Random.Range(fruitDifficultySetting.MinForce, fruitDifficultySetting.MaxForce);
        }

        private Vector3 GetRandomTorque(FriutDifficultySetting fruitDifficultySetting)
        {
            return new Vector3(
                Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque)
            );
        }

        private float GetRandomDelay(FriutDifficultySetting fruitDifficultySetting)
        {
            return Random.Range(fruitDifficultySetting.MinDelay, fruitDifficultySetting.MaxDelay);
        }

        private Quaternion GetRandomRotation(FriutDifficultySetting fruitDifficultySetting)
        {
            return Quaternion.Euler(0f, 0f,
                Random.Range(fruitDifficultySetting.MinAngle, fruitDifficultySetting.MaxAngle));
        }
        
        private GameObject SpawnObject(FriutDifficultySetting fruitDifficultySetting, Bounds spawnerBounds)
        {
            GameObject spawnedObject = null;
            bool _spawnBomb = Random.value < fruitDifficultySetting.SpawnBombChance;

            if (_spawnBomb)
            {
                if (_bombPool.TryGetObject(out Bomb target, _bombPrefab.GetComponent<Bomb>()))
                    spawnedObject = target.gameObject;
            }
            else
            {
                int _randomIndex = Random.Range(0, _objectPools.Count);

                if (_objectPools[_randomIndex]
                    .TryGetObject(out SlicedTarget target, _prefabs[_randomIndex].GetComponent<SlicedTarget>()))
                    spawnedObject = target.gameObject;
            }

            return spawnedObject;
        }
    }
}