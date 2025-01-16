using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitHandler : MonoBehaviour
{
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField] private Decal _decalEffectStone;
    [SerializeField] private Decal _decalEffectMetall;
    [SerializeField] private Decal[] _headShootEffects;
    [SerializeField] private Decal[] _bloodHitEffects;
    [SerializeField] private Transform _bloodContainer;

    public event Action Hit;

    public event Action HitedBomb;

    public event Action HeadHited;

    public void ProcessHit(RaycastHit hit, int damage, float force)
    {
        GameObject impactBlood;

        if (hit.transform.TryGetComponent(out HitPositionEnemy hitPosition))
        {
            // hitPosition.Damage(damage);

            if (hitPosition.IsHead)
            {
                hitPosition.Damage(damage);
                HeadHited?.Invoke();
                int index = Random.Range(0, _headShootEffects.Length);
                impactBlood = Instantiate(_headShootEffects[index].gameObject, hit.point,
                    Quaternion.LookRotation(hit.normal),
                    _bloodContainer);
                impactBlood.transform.Translate(impactBlood.transform.forward * 0.01f, Space.World);
            }
            else
            {
                int index = Random.Range(0, _bloodHitEffects.Length);

                impactBlood = Instantiate(_bloodHitEffects[index].gameObject, hit.point,
                    Quaternion.LookRotation(hit.normal), _bloodContainer);
                impactBlood.transform.Translate(impactBlood.transform.forward * 0.01f, Space.World);
            }

            return;
        }


        /*
        if (hit.transform.TryGetComponent(out Bomb bomb))
            HitedBomb?.Invoke();
            */
        
        if (hit.transform.TryGetComponent<Bomb>(out _))
            HitedBomb?.Invoke();
        

        if (hit.transform.TryGetComponent(out ISettingsHandler settingsHandler))
            settingsHandler.SetSettings();

        if (hit.transform.TryGetComponent(out ITargetHandler targetHit))
        {
            targetHit.HandleHit();
            Hit.Invoke();
            return;
        }

        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * force);

        if (hit.transform.TryGetComponent(out IValueChanger valueChanger))
        {
            valueChanger.ChangeValue();
            // return;
        }

        GameObject impactGO;

        if (hit.collider.TryGetComponent(out Environment environment))
        {
            if (environment.IsStone)
            {
                impactGO = Instantiate(_decalEffectStone.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.transform.SetParent(hit.collider.transform);
                impactGO.transform.Translate(impactGO.transform.forward * 0.01f, Space.World);
            }
            else if (!environment.IsStone)
                impactGO = Instantiate(_decalEffectMetall.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        }


        /*if (hit.transform.GetComponent<Environment>().IsStone)
        {
            // impactGO = Instantiate(_impactEffectStone, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO = Instantiate(_decalEffectStone.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.SetParent(hit.collider.transform);
            impactGO.transform.Translate(impactGO.transform.forward * 0.01f, Space.World);
        }
        else if (!hit.transform.GetComponent<Environment>().IsStone)
        {
            // impactGO = Instantiate(_impactEffectMetall, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO = Instantiate(_decalEffectMetall.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
        }*/
    }
}