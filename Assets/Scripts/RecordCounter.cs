using UnityEngine;

public class RecordCounter : MonoBehaviour
{
    [SerializeField] private StartGame _startGame;
    [SerializeField] private HitHandler _hitHandler;
    [SerializeField] private WeaponSwitching _weaponSwitching;

    private Gun _gun;
    private int _shots;
    private int _hits;
    private float _percent;

    private void OnEnable()
    {
        _startGame.GameStarting += ClearAllData;
        _hitHandler.Hit += AddHit;
        // _gun.Shooting += AddShoot;
        _weaponSwitching.WeaponSwitched += ChangeGun;
    }

    private void OnDisable()
    {
        _startGame.GameStarting -= ClearAllData;
        _hitHandler.Hit -= AddHit;
        _gun.Shooting -= AddShoot;
        _weaponSwitching.WeaponSwitched -= ChangeGun;
    }

    private void ClearAllData()
    {
        _shots = 0;
        _hits = 0;
        _percent = 0;
    }

    private void AddHit() => _hits++;

    private void AddShoot() => _shots++;

    public float GetPercent()
    {
        _percent = (float)_hits / _shots * 100;
        return _percent;
    }

    private void ChangeGun(Gun gun)
    {
        _gun = gun;
        _gun.Shooting += AddShoot;
    }
}