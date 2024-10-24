using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _force = 30f;
    [SerializeField] private float _fireRate = 15f;
    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _impactEffect;

    private float _nextTimeToFire = 0f;
    
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
            
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range))
        {
            _muzzleFlash.Play();
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
                target.TakeDamage(_damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * _force);

            GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            Debug.Log(hit.transform);
        }
    }
}