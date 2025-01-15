using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScaler : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _defaultScale;

    private float _elapsedTime;
    private Target _target;

    private void OnEnable()
    {
        transform.localScale = _defaultScale;
    }

    private void Start()
    {
        _target = GetComponent<Target>();
        StartCoroutine(ScaleTarget());
    }

    private IEnumerator ScaleTarget()
    {
        Vector3 originalScale = transform.localScale;
        _elapsedTime = 0;

        while (_elapsedTime < _duration)
        {
            float scale = Mathf.Lerp(1f, 0f, _elapsedTime / _duration);
            _elapsedTime += Time.deltaTime;
            transform.localScale = originalScale * scale;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        _target.Die();
    }
}