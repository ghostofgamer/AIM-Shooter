using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordCounter : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;
    [SerializeField] private HitHandler _hitHandler;
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField] private Timer _timer;
    [SerializeField] private Spawner _spawner;

    private Gun _gun;
    public int Shots;
    public int Hits;
    public int Dies;
    public float Percent;

    public event Action<int, int, float, int, bool> LevelCompleted;

    private void OnEnable()
    {
        if (_timer != null)
            _timer.GameEnded += TimeOver;

        _startGame.GameStarting += ClearAllData;
        _hitHandler.Hit += AddHit;
        _hitHandler.HitedBomb += LevelOverPause;
        _weaponSwitching.WeaponSwitched += ChangeGun;
    }

    private void OnDisable()
    {
        if (_timer != null)
            _timer.GameEnded -= TimeOver;

        _startGame.GameStarting -= ClearAllData;
        _hitHandler.Hit -= AddHit;
        _hitHandler.HitedBomb -= LevelOverPause;
        _gun.Shooting -= AddShoot;
        _weaponSwitching.WeaponSwitched -= ChangeGun;
    }

    public void AddDie()
    {
        Dies++;
        
        // Debug.Log("ДАЙ");
        
        if (Dies >= 3)
            LevelOver();
    }

    private void LevelOverPause()
    {
        StartCoroutine(Over());
    }

    private IEnumerator Over()
    {
        yield return new WaitForSeconds(1f);
        LevelCompleted?.Invoke(Shots, Hits, GetPercent(), _spawner.SpawnTargetAmount, false);
    }
    
    private void LevelOver()
    {
        LevelCompleted?.Invoke(Shots, Hits, GetPercent(), _spawner.SpawnTargetAmount, false);
    }

    private void TimeOver()
    {
        LevelCompleted?.Invoke(Shots, Hits, GetPercent(), _spawner.SpawnTargetAmount, true);
    }

    private float GetPercent()
    {
        Percent = (float)Hits / Shots * 100;

        if (Shots == 0)
            return 0;

        return Percent;
    }

    private void ClearAllData()
    {
        Shots = 0;
        Hits = 0;
        Percent = 0;
    }

    private void AddHit() => Hits++;

    private void AddShoot() => Shots++;

    private void ChangeGun(Gun gun)
    {
        _gun = gun;
        _gun.Shooting += AddShoot;
    }
}