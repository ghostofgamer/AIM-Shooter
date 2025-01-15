using TMPro;
using UnityEngine;

public class PercentHitEnemy : MonoBehaviour
{
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField] private HitHandler _hitHandler;

    private float _currentPercent;
    private int _currentShootAmount;
    private int _currentHits;
    private Gun _currentGun;

    private void OnEnable()
    {
        _weaponSwitching.WeaponSwitched += SetGun;
        _hitHandler.HeadHited += AddHitHead;
    }

    private void OnDisable()
    {
        _weaponSwitching.WeaponSwitched -= SetGun;
        _hitHandler.HeadHited += AddHitHead;
        
        if (_currentGun != null)
            _currentGun.Shooting -= AddShoot;
    }

    private void SetGun(Gun gun)
    {
        _currentGun = gun;
        _currentGun.Shooting += AddShoot;
    }

    private void AddShoot()
    {
        _currentShootAmount++;
        CalculatePercent();
    }

    private void AddHitHead()
    {
        _currentHits++;
        CalculatePercent();
    }

    private void Start()
    {
        Show();
    }

    private void CalculatePercent()
    {
        _currentPercent = (_currentHits / (float)_currentShootAmount) * 100f;
        Show();
    }

    private void Show()
    {
        _percentText.text = _currentPercent.ToString("F1") + "%";
    }

    public void Clear()
    {
        _currentHits = 0;
        _currentShootAmount = 0;
        _currentPercent = 0;
        Show();
    }
}