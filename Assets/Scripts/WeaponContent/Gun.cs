using System.Collections;
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
    [SerializeField] private Decal _decalEffectStone;
    [SerializeField] private Decal _decalEffectMetall;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isAutomatic;


    [SerializeField] private GameObject _decal;
    [SerializeField] private Transform _container;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioReload;
    [SerializeField] private AudioClip _audioFire;
    [SerializeField] private AudioClip _audioReady;


    [SerializeField] private LookMouse _lookMouse;
    [SerializeField] private float _recoilX;
    [SerializeField] private float _recoilY;
    

    private float _nextTimeToFire = 0f;
    private bool _isReloading = false;

    private void OnEnable()
    {
        _isReloading = false;
        _animator.SetBool("Reloading", false);
    }

    public void ReadyGun()
    {
        _audioSource.PlayOneShot(_audioReady);
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

        if (Input.GetMouseButtonDown(0) && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }

        if (Input.GetMouseButton(0) && Time.time >= _nextTimeToFire && _isAutomatic)
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
        _audioSource.PlayOneShot(_audioReload);
        _animator.SetBool("Reloading", _isReloading);
        yield return new WaitForSeconds(_reloadTime - 0.25f);
        _animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }

    private void Shoot()
    {
        // _lookMouse.ChangeOffset(Random.Range(0,_recoilX),Random.Range(-_recoilY,_recoilY));
        
        RaycastHit hit;
        _audioSource.PlayOneShot(_audioFire);
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

            GameObject impactGO;

            if (hit.transform.GetComponent<Environment>().IsStone)
            {
                Debug.Log("Stone");
                // impactGO = Instantiate(_impactEffectStone, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO = Instantiate(_decalEffectStone.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
              
                impactGO.transform.SetParent(hit.collider.transform);
                impactGO.transform.Translate(impactGO.transform.forward * 0.01f, Space.World);
                
            }
            else
            {
                Debug.Log("Metall");
                // impactGO = Instantiate(_impactEffectMetall, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO = Instantiate(_decalEffectMetall.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
            }
            // _lookMouse.ChangeOffset(Random.Range(0,_recoilX),Random.Range(-_recoilY,_recoilY));
            _lookMouse.ChangeOffset(Random.Range(-_recoilY,_recoilY),Random.Range(0,_recoilX));

            // GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // Destroy(impactGO, 2f);

            // GameObject decal = Instantiate(_decal, _container);

            /*GameObject decal = Instantiate(_decal, hit.point, Quaternion.LookRotation(hit.normal));
            decal.transform.Translate(decal.transform.forward * 0.1f, Space.World);*/

            /*decal.transform.position = hit.point + hit.normal * 0.01f;
            decal.transform.rotation = Quaternion.FromToRotation(decal.transform.up, hit.normal);*/
        }
    }
}