using System.Collections;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioFire;
    [SerializeField] private AudioClip _audioReload;
    [SerializeField] private Gun _gun;

    private void OnEnable()
    {
        _gun.Shooting += ShowShootEffect;
        _gun.Reloading += ReloadEffect;
        _animator.SetBool("Reloading", false);
    }

    private void OnDisable()
    {
        _gun.Shooting -= ShowShootEffect;
        _gun.Reloading -= ReloadEffect;
    }

    private void ShowShootEffect()
    {
        _audioSource.PlayOneShot(_audioFire);
        _animator.SetTrigger("Fire");
        _muzzleFlash.Play();
    }

    private void ReloadEffect(float reloadTime)
    {
        StartCoroutine(Reload(reloadTime));
    }

    private IEnumerator Reload(float reloadTime)
    {
        _animator.ResetTrigger("Fire");
        _audioSource.PlayOneShot(_audioReload);
        _animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        _animator.SetBool("Reloading", false);
    }
}