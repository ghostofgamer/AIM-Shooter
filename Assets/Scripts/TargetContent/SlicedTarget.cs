using System;
using UnityEngine;

public class SlicedTarget : MonoBehaviour, IDamageable, ITargetHandler
{
    [SerializeField] private GameObject _defaultPrefab;
    [SerializeField] private GameObject _slicedPrefab;
    [SerializeField] private Collider[] _targetColliders;
    [SerializeField] private ParticleSystem _slicedParticles;

    private Rigidbody _targetRigidbody;
    private RecordCounter _recordCounter;


    private void Awake()
    {
        _targetRigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
    }

    public void HandleHit()
    {
        Slice();
    }

    private void Slice()
    {
        if (_slicedParticles != null)
            _slicedParticles.Play();

        _defaultPrefab.SetActive(false);
        _slicedPrefab.SetActive(true);

        foreach (var collider in _targetColliders)
            collider.enabled = false;

        _slicedPrefab.transform.rotation = Quaternion.Euler(0, 0, 15f);

        Rigidbody[] slices = _slicedPrefab.GetComponentsInChildren<Rigidbody>();

        foreach (var slice in slices)
        {
            slice.velocity = _targetRigidbody.velocity;
            slice.AddExplosionForce(6f, _targetRigidbody.transform.position, 5f, 3.0f, ForceMode.Impulse);

            Vector3 randomForce = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f)
            );
            slice.AddForce(randomForce * 3f, ForceMode.Impulse);

            // slice.AddForceAtPosition(Vector3.up * 5f, slice.transform.position,ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Environment environment))
            Die();
    }

    public void Die()
    {
        Debug.Log("Сам ВЫКЛ");
        _recordCounter.AddDie();
        gameObject.SetActive(false);
    }

    public void Init(RecordCounter recordCounter)
    {
        _recordCounter = recordCounter;
    }
}