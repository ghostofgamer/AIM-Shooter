using System;
using UnityEngine;

public class RecordCounter : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;
    [SerializeField] private HitHandler _hitHandler;
    [SerializeField] private WeaponSwitching _weaponSwitching;

    private Gun _gun;
    private int _shots;
    private int _hits;
    private int _dies;
    private float _percent;

    public event Action<int,int,float > LevelCompleted;
  
    private void OnEnable()
    {
        _startGame.GameStarting += ClearAllData;
        _hitHandler.Hit += AddHit;
        _weaponSwitching.WeaponSwitched += ChangeGun;
    }

    private void OnDisable()
    {
        _startGame.GameStarting -= ClearAllData;
        _hitHandler.Hit -= AddHit;
        _gun.Shooting -= AddShoot;
        _weaponSwitching.WeaponSwitched -= ChangeGun;
    }

    public void AddDie()
    {
        _dies++;

        if (_dies >= 3)
            LevelCompleted?.Invoke(_shots,_hits,GetPercent());
    }

    public float GetPercent()
    {
        _percent = (float)_hits / _shots * 100;
        return _percent;
    }

    private void ClearAllData()
    {
        _shots = 0;
        _hits = 0;
        _percent = 0;
    }

    private void AddHit() => _hits++;

    private void AddShoot() => _shots++;

    private void ChangeGun(Gun gun)
    {
        _gun = gun;
        _gun.Shooting += AddShoot;
    }
}