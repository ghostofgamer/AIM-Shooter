using System;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _targetHitMetall;
    [SerializeField] private AudioClip _targetHitEnemy;
    [SerializeField] private AudioClip _targetHitHeadshot;
    [SerializeField] private AudioClip _bombExplosion;
    [SerializeField] private HitHandler _hitHandler;

    private void OnEnable()
    {
        _hitHandler.Hit += PlayTargetHitMetal;
        _hitHandler.HeadHited += PlayHeadshot;
        _hitHandler.HitedBomb += PlayBombExplosion;
    }

    private void OnDisable()
    {
        _hitHandler.HeadHited -= PlayHeadshot;
        _hitHandler.Hit -= PlayTargetHitMetal;
        _hitHandler.HitedBomb -= PlayBombExplosion;
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
    
    public void PlayBombExplosion()
    {
        _audioSource.PlayOneShot(_bombExplosion);
    }
}