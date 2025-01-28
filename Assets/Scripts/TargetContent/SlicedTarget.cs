using System;
using UnityEngine;
using Environment = EnvironmentContent.Environment;

public class SlicedTarget : MonoBehaviour
{
    [SerializeField] private GameObject _defaultPrefab;
    [SerializeField] private GameObject _slicedPrefab;
    [SerializeField] private Collider[] _targetColliders;
    [SerializeField] private ParticleSystem _slicedParticles;
    [SerializeField] private GameObject[] _slicedParts;

    private Rigidbody _targetRigidbody;
    private RecordCounter _recordCounter;
    private Vector3[] _slicedPartsPositions;
    private Quaternion[] _slicedPartsRotations;
    private bool _isSliced;

    private void Awake()
    {
        _targetRigidbody = GetComponent<Rigidbody>();

        _slicedPartsPositions = new Vector3[_slicedParts.Length];
        _slicedPartsRotations = new Quaternion[_slicedParts.Length];

        for (int i = 0; i < _slicedPartsPositions.Length; i++)
        {
            _slicedPartsPositions[i] = _slicedParts[i].transform.localPosition;
        }

        for (int i = 0; i < _slicedPartsRotations.Length; i++)
        {
            _slicedPartsRotations[i] = _slicedParts[i].transform.rotation;
        }
    }

    private void OnEnable()
    {
        _isSliced = false;

        _defaultPrefab.gameObject.SetActive(true);
        _slicedPrefab.gameObject.SetActive(false);

        foreach (var collider in _targetColliders)
            collider.enabled = true;

        for (int i = 0; i < _slicedParts.Length; i++)
        {
            _slicedParts[i].transform.localPosition = _slicedPartsPositions[i];
            Rigidbody rb = _slicedParts[i].GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        /*for (int i = 0; i < _slicedParts.Length; i++)
        {
            _slicedParts[i].transform.position = _slicedPartsPositions[i];
        }

        for (int i = 0; i < _slicedPartsRotations.Length; i++)
        {
            _slicedParts[i].transform.rotation = _slicedPartsRotations[i];
        }*/
    }

    /*public void TakeDamage(int damage)
    {
    }*/

    /*public void HandleHit()
    {
        Slice();
    }*/

    public void Slice()
    {
        _isSliced = true;
        Debug.Log("Slice");

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
        if (!_isSliced)
            _recordCounter.AddDie();

        gameObject.SetActive(false);
    }

    public void Init(RecordCounter recordCounter)
    {
        _recordCounter = recordCounter;
    }
}