using UnityEngine;

public class HitHandler : MonoBehaviour
{
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField] private Decal _decalEffectStone;
    [SerializeField] private Decal _decalEffectMetall;

    public void ProcessHit(RaycastHit hit, int damage, float force)
    {
        if (hit.transform.TryGetComponent(out IDamageable target))
            target.TakeDamage(damage);

        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * force);
        
        if (hit.transform.TryGetComponent(out WeaponChanger weaponChanger))
            _weaponSwitching.Selected(weaponChanger.Index);
        
        if (hit.transform.TryGetComponent(out AimChanger aimChanger))
            aimChanger.ChangeAim();
        
        if (hit.transform.TryGetComponent(out AimScaleChanger aimScaleChanger))
            aimScaleChanger.ChangeScale();
        
        if (hit.transform.TryGetComponent(out AimColorChanger aimColorChanger))
            aimColorChanger.ChangeColor();
        
        GameObject impactGO;

        if (hit.transform.GetComponent<Environment>().IsStone)
        {
            // impactGO = Instantiate(_impactEffectStone, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO = Instantiate(_decalEffectStone.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.SetParent(hit.collider.transform);
            impactGO.transform.Translate(impactGO.transform.forward * 0.01f, Space.World);
        }
        else
        {
            // impactGO = Instantiate(_impactEffectMetall, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO = Instantiate(_decalEffectMetall.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}