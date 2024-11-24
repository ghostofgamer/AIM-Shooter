using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NinjaSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Collider _spawner;
    [SerializeField] private float _minDelay = 0.3f;
    [SerializeField] private float _maxDelay = 1f;
    [SerializeField] private float _minAngle = -15f;
    [SerializeField] private float _maxAngle = 15f;
    [SerializeField] private float _minForce = 10f;
    [SerializeField] private float _maxForce = 15f;
    [SerializeField] private float _lifeTime = 4f;

    private void Awake()
    {
        _spawner = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);

        while (enabled )
        {
            Vector3 position = new Vector3();
            position.x = Random.Range(_spawner.bounds.min.x, _spawner.bounds.max.x);
            position.y = Random.Range(_spawner.bounds.min.y, _spawner.bounds.max.y);
            position.z = Random.Range(_spawner.bounds.min.z, _spawner.bounds.max.z);

            Quaternion rotation  = Quaternion.Euler(0f,0f,Random.Range(_minAngle,_maxAngle));
            GameObject targetObject = Instantiate(_prefab,position,rotation);
            Destroy(targetObject, _lifeTime);
            
            float force = Random.Range(_minForce,_maxForce);
            targetObject.GetComponent<Rigidbody>().AddForce(targetObject.transform.up * force,ForceMode.Impulse);
            
            yield return new WaitForSeconds(Random.Range(_minDelay,_maxDelay));
        }
    }
}