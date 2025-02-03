using System;
using System.Collections;
using SpawnContent;
using StartGameContent;
using StatisticContent;
using UnityEngine;
using WeaponContent;

public class RecordCounter : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;
    [SerializeField] private HitHandler _hitHandler;
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField] private Timer _timer;
    [SerializeField] private Spawner _spawner;

    private Gun _gun;
    private int _shots;
    private int _hits;
    private int _dies;
    private float _percent;
    private int _maxDie = 3;
    private float _factor=100f;
    private float _delay = 1f;

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
        _dies++;
        
        if (_dies >= _maxDie)
            LevelOver();
    }

    private void LevelOverPause()
    {
        StartCoroutine(Over());
    }

    private IEnumerator Over()
    {
        yield return new WaitForSeconds(_delay);
        LevelCompleted?.Invoke(_shots, _hits, GetPercent(), _spawner.SpawnTargetAmount, false);
    }
    
    private void LevelOver()
    {
        LevelCompleted?.Invoke(_shots, _hits, GetPercent(), _spawner.SpawnTargetAmount, false);
    }

    private void TimeOver()
    {
        LevelCompleted?.Invoke(_shots, _hits, GetPercent(), _spawner.SpawnTargetAmount, true);
    }

    private float GetPercent()
    {
        _percent = (float)_hits / _shots * _factor;

        if (_shots == 0)
            return 0;

        return _percent;
    }

    private void ClearAllData()
    {
        _shots = 0;
        _hits = 0;
        _percent = 0;
    }
    
    private void AddHit()
    {
        _hits++;
    }
    private void AddShoot() => _shots++;

    private void ChangeGun(Gun gun)
    {
        _gun = gun;
        _gun.Shooting += AddShoot;
    }
}