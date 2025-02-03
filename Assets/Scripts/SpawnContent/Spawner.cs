using System.Collections;
using TargetContent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnContent
{
    public class Spawner : AbstractSpawner
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _maxSize;
    
        private ObjectPool<Target> _objectPool;
        private float _minDistance;
        private Vector3 _spawnRange;
        private Vector3 _newPos;
        private bool _isValidPosition;
        
        public int SpawnTargetAmount { get; private set; }
        
        private void Start()
        {
            _objectPool = new ObjectPool<Target>(_prefab.GetComponent<Target>(),_maxSize, Contaner);
            _objectPool.EnableAutoExpand();
        }

        protected override IEnumerator SpawnTarget()
        {
            SpawnTargetAmount = 0;
            _minDistance = DifficultySettings.minDistanceBetweenTargets;
            _spawnRange = DifficultySettings.spawnRange;

            while (IsWork)
            {
                _newPos = Vector3.zero;
                _isValidPosition = false;

                while (!_isValidPosition)
                {
                    _newPos = GetRandomPosition(_spawnRange);
                    _isValidPosition = IsValidPosition(_newPos, _minDistance);
                }

                SpawnObject(_newPos);

                yield return new WaitForSeconds(DifficultySettings.spawnDelay);
            }
        }
        
        private Vector3 GetRandomPosition(Vector3 spawnRange)
        {
            return new Vector3(
                transform.position.x + Random.Range(-spawnRange.x, spawnRange.x),
                transform.position.y + Random.Range(-spawnRange.y, spawnRange.y),
                transform.position.z + Random.Range(-spawnRange.z, spawnRange.z)
            );
        }
        
        private bool IsValidPosition(Vector3 position, float minDistance)
        {
            foreach (Transform child in Contaner)
            {
                if (child.gameObject.activeSelf && Vector3.Distance(child.position, position) < minDistance)
                    return false;
            }
            
            return true;
        }
        
        private void SpawnObject(Vector3 position)
        {
            if (_objectPool.TryGetObject(out Target target, _prefab.GetComponent<Target>()))
            {
                target.gameObject.SetActive(true);
                target.transform.position = position;
                SpawnTargetAmount++;
                target.Init(RecordCounter);
            }
        }
    }
}