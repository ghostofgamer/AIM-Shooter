using System.Collections;
using UnityEngine;

namespace TargetContent
{
    public class TargetScaler : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _defaultScale;

        private float _elapsedTime;
        private Target _target;
        private Coroutine _coroutine;
        private Vector3 _originalScale;
        private float  _scale;

        private void OnEnable()
        {
            _target = GetComponent<Target>();
            transform.localScale = _defaultScale;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ScaleTarget());
        }

        private IEnumerator ScaleTarget()
        {
            _originalScale = transform.localScale;
            _elapsedTime = 0;

            while (_elapsedTime < _duration)
            {
                _scale = Mathf.Lerp(1f, 0f, _elapsedTime / _duration);
                _elapsedTime += Time.deltaTime;
                transform.localScale = _originalScale * _scale;
                yield return null;
            }

            transform.localScale = Vector3.zero;
            _target.Die();
        }
    }
}