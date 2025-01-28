using UnityEngine;

namespace EnemyContent
{
    public class EnemyRigidbodies : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;

        private Vector3[] _initialPositions;
        private Quaternion[] _initialRotations;
    
        private void Start()
        {
            _initialPositions = new Vector3[_rigidbodies.Length];
            _initialRotations = new Quaternion[_rigidbodies.Length];
        
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _initialPositions[i] = _rigidbodies[i].transform.localPosition;
                _initialRotations[i] = _rigidbodies[i].transform.localRotation;
            }
        }
    
        public void DisableKinematic()
        {
            foreach (var rigidbody in _rigidbodies)
                rigidbody.isKinematic = false;
        }

        public void SetDefaultSettings()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = true;
                rb.transform.localPosition = _initialPositions[System.Array.IndexOf(_rigidbodies, rb)];
                rb.transform.localRotation = _initialRotations[System.Array.IndexOf(_rigidbodies, rb)];
            }
        }
    }
}
