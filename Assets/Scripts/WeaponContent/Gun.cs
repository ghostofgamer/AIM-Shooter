using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour, IShootable
{
    int layerToIgnore = 8;

    [SerializeField] private int _damage = 10;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _force = 30f;
    [SerializeField] private float _fireRate = 15f;
    [SerializeField] private int _maxAmmo = 10;
    [SerializeField] private int _currentAmmo = -1;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _recoilX;
    [SerializeField] private float _recoilY;
    [SerializeField] private float _timeToReady;
    [SerializeField] private bool _isAutomatic;
    [SerializeField] private bool _isEndlessAmmo = false;
    [SerializeField] private LookMouse _lookMouse;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private HitHandler _hitHandler;

    private WaitForSeconds _waitForSeconds;
    private float _nextTimeToFire = 0f;
    private bool _isReloading = false;
    private bool _isReady;
    private Coroutine _coroutine;

    public event Action Shooting;

    public event Action<float> Reloading;

    public event Action<int> AmmoChanged;

    public int CurrentAmmo => _currentAmmo;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeToReady);

        if (_currentAmmo == -1)
        {
            _currentAmmo = _maxAmmo;
            AmmoChanged?.Invoke(_currentAmmo);
        }
    }

    private void OnEnable()
    {
        _playerInput.RKeyPressed += ReloadWeapon;
        _playerInput.MouseZeroKeyPressed += SingleShotHandler;
        _playerInput.MouseZeroKeyHoldDown += AutomaticShotHandler;
        _isReloading = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PrepareWeapon());
    }

    private void OnDisable()
    {
        _playerInput.RKeyPressed -= ReloadWeapon;
        _playerInput.MouseZeroKeyPressed -= SingleShotHandler;
        _playerInput.MouseZeroKeyHoldDown -= AutomaticShotHandler;
    }

    private void Update()
    {
        if (_isReloading)
            return;

        if (_currentAmmo <= 0)
            ReloadWeapon();
    }

    private IEnumerator PrepareWeapon()
    {
        _isReady = false;
        yield return _waitForSeconds;
        _isReady = true;
    }

    private void ReloadWeapon()
    {
        if (_isReloading)
            return;

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        Reloading?.Invoke(_reloadTime);
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        AmmoChanged?.Invoke(_currentAmmo);
    }

    private void SingleShotHandler()
    {
        ShotHandler();
    }

    private void AutomaticShotHandler()
    {
        if (_isAutomatic)
            ShotHandler();
    }

    private void ShotHandler()
    {
        if (_isReloading)
            return;

        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!_isReady) return;

        Shooting?.Invoke();
        RaycastHit hit;

        if (!_isEndlessAmmo)
        {
            _currentAmmo--;
            AmmoChanged?.Invoke(_currentAmmo);
        }
       

        int layerMask = ~(1 << layerToIgnore);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range, layerMask))
        {
            _hitHandler.ProcessHit(hit, _damage, _force);
            // _lookMouse.ChangeOffset(Random.Range(-_recoilY, _recoilY), Random.Range(0, _recoilX));
        }

        _lookMouse.ChangeOffset(Random.Range(-_recoilY, _recoilY), Random.Range(0, _recoilX));
    }

    public void DefaultAmmo()
    {
        _currentAmmo = _maxAmmo;
        AmmoChanged?.Invoke(_currentAmmo);
    }
}