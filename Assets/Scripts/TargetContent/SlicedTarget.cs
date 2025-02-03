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
            _slicedPartsPositions[i] = _slicedParts[i].transform.localPosition;

        for (int i = 0; i < _slicedPartsRotations.Length; i++)
            _slicedPartsRotations[i] = _slicedParts[i].transform.rotation;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Environment environment))
            Die();
    }

    public void Slice()
    {
        _isSliced = true;

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
        }
    }

    public void Init(RecordCounter recordCounter)
    {
        _recordCounter = recordCounter;
    }

    private void Die()
    {
        if (!_isSliced)
            _recordCounter.AddDie();

        gameObject.SetActive(false);
    }
}