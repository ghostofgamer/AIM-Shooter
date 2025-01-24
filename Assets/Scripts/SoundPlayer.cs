using System;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _targetHitMetall;
    [SerializeField] private AudioClip _targetHitEnemy;
    [SerializeField] private AudioClip _targetHitHeadshot;
    [SerializeField] private HitHandler _hitHandler;

    private void OnEnable()
    {
        _hitHandler.Hit += PlayTargetHitMetal;
        _hitHandler.HeadHited += PlayHeadshot;
    }

    private void OnDisable()
    {
        _hitHandler.HeadHited -= PlayHeadshot;
        _hitHandler.Hit -= PlayTargetHitMetal;
    }

    public void PlayTargetHitMetal()
    {
        _audioSource.PlayOneShot(_targetHitMetall);
    }

    public void PlayTargetHitEnemy()
    {
        _audioSource.PlayOneShot(_targetHitEnemy);
    }

    public void PlayHeadshot()
    {
        _audioSource.PlayOneShot(_targetHitHeadshot);
    }
}