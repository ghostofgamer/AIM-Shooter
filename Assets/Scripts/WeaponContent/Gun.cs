using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _force = 30f;
    [SerializeField] private float _fireRate = 15f;

    [SerializeField] private int _maxAmmo = 10;
    [SerializeField] private int _currentAmmo = -1;
    [SerializeField] private float _reloadTime = 1f;

    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _impactEffect;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isAutomatic;

    private float _nextTimeToFire = 0f;
    private bool _isReloading = false;

    private void OnEnable()
    {
        _isReloading = false;
        _animator.SetBool("Reloading", false);
    }

    private void Start()
    {
        if (_currentAmmo == -1)
            _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if (_isReloading)
            return;

        if (_currentAmmo <= 0 || Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }

        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && _isAutomatic)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    private IEnumerator Reload()
    {
        _animator.ResetTrigger("Fire");

        Debug.Log("Reloading");
        _isReloading = true;
        _animator.SetBool("Reloading", _isReloading);
        yield return new WaitForSeconds(_reloadTime - 0.25f);
        _animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }

    private void Shoot()
    {
        Debug.Log("Выстрел");
        RaycastHit hit;

        _animator.SetTrigger("Fire");

        _muzzleFlash.Play();
        _currentAmmo--;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
                target.TakeDamage(_damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * _force);

            GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}