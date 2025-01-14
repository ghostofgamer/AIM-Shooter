using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;
    [SerializeField] private float _minX = 3f;
    [SerializeField] private float _maxX = 3f;
    [SerializeField] private float _minZ = 3f;
    [SerializeField] private float _maxZ = 3f;

    [SerializeField] private Animator _animator;

    private Vector3 targetPosition;
    private float waitTime;
    private float waitCounter;
    private bool _isWaiting = true;

    private void OnEnable()
    {
        if (_enemy.Health <= 0)
        {
            _enemy.StartRevive();
            StartCoroutine(NewPosition());
        }
    }

    private void Start()
    {
        // SetNewTargetPosition();
        StartCoroutine(NewPosition());
    }

    private void Update()
    {
        if (!_isWaiting && _enemy.Health > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.LookAt(targetPosition);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                StartCoroutine(NewPosition());
        }
        else
        {
            _isWaiting = true;
            StopCoroutine(NewPosition());
        }


        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            /*transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.LookAt(targetPosition);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                StartCoroutine(NewPosition());*/


            /*if (waitCounter > 0)
            {
                waitCounter -= Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                transform.LookAt(targetPosition);

                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    /*Debug.Log("IDLE");
                    _animator.Play("Idle");
                    waitCounter = waitTime;
                    SetNewTargetPosition();#1#
                    NewPosition();
                }
            }*/
        }
    }

    private IEnumerator NewPosition()
    {
        _isWaiting = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        _animator.Play("Idle");
        Debug.Log("Time: " + waitTime);
        yield return new WaitForSeconds(waitTime);
        _animator.Play("Walk");
        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        targetPosition = new Vector3(x, transform.position.y, z);
        _isWaiting = false;
    }

    private void SetNewTargetPosition()
    {
        Debug.Log("SetNewTargetPosition");
        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        targetPosition = new Vector3(x, transform.position.y, z);
        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Wall wall))
        {
            SetNewTargetPosition();
        }
    }*/
}