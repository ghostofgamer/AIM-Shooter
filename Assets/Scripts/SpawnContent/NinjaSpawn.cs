using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using Random = UnityEngine.Random;

public class NinjaSpawn : AbstractSpawner
{
    [SerializeField] private GameObject[] _prefabs;

    // [SerializeField] private GameObject[] _testPrefabs;
    
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Collider _spawner;
    [SerializeField] private float _minDelay = 0.3f;
    [SerializeField] private float _maxDelay = 1f;
    [SerializeField] private float _minAngle = -15f;
    [SerializeField] private float _maxAngle = 15f;
    [SerializeField] private float _minForce = 10f;
    [SerializeField] private float _maxForce = 15f;
    [SerializeField] private float _lifeTime = 4f;
    [SerializeField] private float _maxTorque = 5f;
    [SerializeField] private float _spawnBobmChance = 0.05f;

    private List<ObjectPool<SlicedTarget>> _objectPools;
    private ObjectPool<Bomb> _bombPool;

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

    protected override IEnumerator SpawnTarget()
    {
        FriutDifficultySetting fruitDifficultySetting = DifficultySettings as FriutDifficultySetting;

        if (fruitDifficultySetting == null)
            yield break;

        yield return new WaitForSeconds(1.5f);

        while (enabled)
        {
            /*GameObject prefab = _prefabs[Random.Range(0, _prefabs.Length)];

            if (Random.value < fruitDifficultySetting.SpawnBombChance)
                prefab = _bombPrefab;*/

            bool spawnBomb = Random.value < fruitDifficultySetting.SpawnBombChance;
            GameObject spawnedObject = null;

            if (spawnBomb)
            {
                if (_bombPool.TryGetObject(out Bomb target, _bombPrefab.GetComponent<Bomb>()))
                {
                    spawnedObject = target.gameObject;
                }
            }
            else
            {
                int randomIndex = Random.Range(0, _objectPools.Count);

                if (_objectPools[randomIndex]
                    .TryGetObject(out SlicedTarget target, _prefabs[randomIndex].GetComponent<SlicedTarget>()))
                {
                    spawnedObject = target.gameObject;
                }
            }

            if (spawnedObject != null)
            {
                Vector3 position = new Vector3();
                position.x = Random.Range(_spawner.bounds.min.x, _spawner.bounds.max.x);
                position.y = Random.Range(_spawner.bounds.min.y, _spawner.bounds.max.y);
                position.z = Random.Range(_spawner.bounds.min.z, _spawner.bounds.max.z);

                Quaternion rotation = Quaternion.Euler(0f, 0f,
                    Random.Range(fruitDifficultySetting.MinAngle, fruitDifficultySetting.MaxAngle));
                spawnedObject.transform.position = position;
                spawnedObject.transform.rotation = rotation;

                if (spawnedObject.GetComponent<SlicedTarget>())
                    spawnedObject.GetComponent<SlicedTarget>().Init(RecordCounter);
                float force = Random.Range(fruitDifficultySetting.MinForce, fruitDifficultySetting.MaxForce);
                
                Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                spawnedObject.gameObject.SetActive(true);

                spawnedObject.GetComponent<Rigidbody>().AddForce(spawnedObject.transform.up * force, ForceMode.Impulse);

                Vector3 randomTorque = new Vector3(
                    Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                    Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                    Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque)
                );

                spawnedObject.GetComponent<Rigidbody>().AddTorque(randomTorque, ForceMode.Impulse);


                /*Vector3 position = new Vector3();
                position.x = Random.Range(_spawner.bounds.min.x, _spawner.bounds.max.x);
                position.y = Random.Range(_spawner.bounds.min.y, _spawner.bounds.max.y);
                position.z = Random.Range(_spawner.bounds.min.z, _spawner.bounds.max.z);

                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(fruitDifficultySetting.MinAngle, fruitDifficultySetting.MaxAngle));
                GameObject targetObject = Instantiate(prefab, position, rotation, Contaner);

                if (targetObject.GetComponent<SlicedTarget>())
                    targetObject.GetComponent<SlicedTarget>().Init(RecordCounter);

                float force = Random.Range(fruitDifficultySetting.MinForce, fruitDifficultySetting.MaxForce);
                targetObject.GetComponent<Rigidbody>().AddForce(targetObject.transform.up * force, ForceMode.Impulse);

                Vector3 randomTorque = new Vector3(
                    Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                    Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque),
                    Random.Range(-fruitDifficultySetting.MaxTorque, fruitDifficultySetting.MaxTorque)
                );

                targetObject.GetComponent<Rigidbody>().AddTorque(randomTorque, ForceMode.Impulse);*/
                yield return new WaitForSeconds(Random.Range(fruitDifficultySetting.MinDelay,
                    fruitDifficultySetting.MaxDelay));
            }
        }
    }
}