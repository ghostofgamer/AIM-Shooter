using System;
using UnityEngine;
using UnityEngine.Serialization;

public class RecordCounter : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;
    [SerializeField] private HitHandler _hitHandler;
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField]private Timer _timer;

    private Gun _gun;
    public int Shots;
    public int Hits;
    public int Dies;
    public float Percent;

    public event Action<int, int, float> LevelCompleted;

    private void OnEnable()
    {
        _timer.GameEnded += LevelOver;
        _startGame.GameStarting += ClearAllData;
        _hitHandler.Hit += AddHit;
        _hitHandler.HitedBomb += LevelOver;
        _weaponSwitching.WeaponSwitched += ChangeGun;
    }

    private void OnDisable()
    {
        _timer.GameEnded -= LevelOver;
        _startGame.GameStarting -= ClearAllData;
        _hitHandler.Hit -= AddHit;
        _hitHandler.HitedBomb -= LevelOver;
        _gun.Shooting -= AddShoot;
        _weaponSwitching.WeaponSwitched -= ChangeGun;
    }

    public void AddDie()
    {
        Dies++;

        if (Dies >= 3)
            LevelOver();
    }

    public void LevelOver()
    {
        LevelCompleted?.Invoke(Shots, Hits, GetPercent());
    }

    public float GetPercent()
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