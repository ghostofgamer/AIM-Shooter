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

        if (hit.transform.TryGetComponent(out IValueChanger valueChanger))
        {
            valueChanger.ChangeValue();
            return;
        }
        
        GameObject impactGO;

        if (hit.transform.GetComponent<Environment>().IsStone)
        {
            // impactGO = Instantiate(_impactEffectStone, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO = Instantiate(_decalEffectStone.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.SetParent(hit.collider.transform);
            impactGO.transform.Translate(impactGO.transform.forward * 0.01f, Space.World);
        }
        else if(!hit.transform.GetComponent<Environment>().IsStone)
        {
            // impactGO = Instantiate(_impactEffectMetall, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO = Instantiate(_decalEffectMetall.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}