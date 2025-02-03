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
                bool isValidPosition = false;

                while (!isValidPosition)
                {
                    _newPos = new Vector3(
                        transform.position.x + Random.Range(-_spawnRange.x, _spawnRange.x),
                        transform.position.y + Random.Range(-_spawnRange.y, _spawnRange.y),
                        transform.position.z + Random.Range(-_spawnRange.z, _spawnRange.z)
                    );

                    isValidPosition = true;
                    
                    foreach (Transform child in Contaner)
                    {
                        if (child.gameObject.activeSelf && Vector3.Distance(child.position, _newPos) < _minDistance)
                        {
                            isValidPosition = false;
                            break;
                        }
                    }
                }

                if (_objectPool.TryGetObject(out Target target, _prefab.GetComponent<Target>()))
                {
                    target.gameObject.SetActive(true);
                    target.transform.position = _newPos;
                    SpawnTargetAmount++;
                    target.Init(RecordCounter);
                }
                
                yield return new WaitForSeconds(DifficultySettings.spawnDelay);
            }
        }
    }
}