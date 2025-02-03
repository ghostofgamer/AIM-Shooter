using StatisticContent;
using UnityEngine;
using Environment = EnvironmentContent.Environment;

namespace TargetContent
{
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
        private float _factor = 1;
        private float _explosionRadius = 5f;
        private float _explosionForce = 6f;
        private float _upwordsModifier = 3f;
        private Vector3 _randomForce;

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
            DisableColliders();
            _slicedPrefab.transform.rotation = Quaternion.Euler(0, 0, 15f);
            Rigidbody[] slices = _slicedPrefab.GetComponentsInChildren<Rigidbody>();
            AddForceToSlices(slices);
        }

        public void Init(RecordCounter recordCounter)
        {
            _recordCounter = recordCounter;
        }

        private void DisableColliders()
        {
            foreach (var collider in _targetColliders)
                collider.enabled = false;
        }

        private void AddForceToSlices(Rigidbody[] slices)
        {
            foreach (var slice in slices)
            {
                slice.velocity = _targetRigidbody.velocity;
                slice.AddExplosionForce(_explosionForce, _targetRigidbody.transform.position, _explosionRadius,
                    _upwordsModifier, ForceMode.Impulse);

                _randomForce = new Vector3(
                    UnityEngine.Random.Range(-_factor, _factor),
                    UnityEngine.Random.Range(-_factor, _factor),
                    UnityEngine.Random.Range(-_factor, _factor)
                );

                slice.AddForce(_randomForce * 3f, ForceMode.Impulse);
            }
        }

        private void Die()
        {
            if (!_isSliced)
                _recordCounter.AddDie();

            gameObject.SetActive(false);
        }
    }
}