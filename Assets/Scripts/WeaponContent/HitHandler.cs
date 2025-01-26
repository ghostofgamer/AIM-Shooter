using System;
using System.Collections.Generic;
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
    [SerializeField] private ParticleSystem _explosionEffect;

    private Dictionary<string, ObjectPool<Decal>> _effectsPools;

    public event Action Hit;

    public event Action HitedBomb;

    public event Action HeadHited;

    private void Awake()
    {
        _effectsPools = new Dictionary<string, ObjectPool<Decal>>();


        if (_headShootEffects.Length > 0 || _bloodHitEffects.Length > 0)
        {
            InitializePool("HeadShoot", _headShootEffects, 10);
            InitializePool("BloodHit", _bloodHitEffects, 10);
        }
        // Initialize pools for each type of effect

        InitializePool("StoneDecal", new Decal[] { _decalEffectStone }, 10);
        InitializePool("MetalDecal", new Decal[] { _decalEffectMetall }, 10);
    }

    private void InitializePool(string key, Decal[] prefabs, int count)
    {
        ObjectPool<Decal> pool = new ObjectPool<Decal>(prefabs[0], count, _bloodContainer);
        pool.EnableAutoExpand();

        _effectsPools[key] = pool;

        for (int i = 1; i < prefabs.Length; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Decal newDecal = GameObject.Instantiate(prefabs[i], _bloodContainer);
                newDecal.gameObject.SetActive(false);
                pool.AddObject(newDecal);
            }
        }
    }

    public void ProcessHit(RaycastHit hit, int damage, float force)
    {
        GameObject impactBlood;

        if (hit.transform.TryGetComponent(out HitPositionEnemy hitPosition))
        {
            // hitPosition.Damage(damage);
            string poolKey = hitPosition.IsHead ? "HeadShoot" : "BloodHit";

            if (hitPosition.IsHead)
            {
                hitPosition.Damage(damage);
                HeadHited?.Invoke();

                if (_effectsPools[poolKey].TryGetObject(out Decal decal, _headShootEffects[0]))
                {
                    decal.transform.position = hit.point;
                    decal.transform.rotation = Quaternion.LookRotation(hit.normal);
                    decal.transform.Translate(decal.transform.forward * 0.01f, Space.World);
                    decal.gameObject.SetActive(true);
                    // decal.GetComponent<ParticleSystem>().Play();
                }

                /*int index = Random.Range(0, _headShootEffects.Length);
                impactBlood = Instantiate(_headShootEffects[index].gameObject, hit.point,
                    Quaternion.LookRotation(hit.normal),
                    _bloodContainer);
                impactBlood.transform.Translate(impactBlood.transform.forward * 0.01f, Space.World);*/
            }
            else
            {
                if (_effectsPools[poolKey].TryGetObject(out Decal decal, _bloodHitEffects[0]))
                {
                    decal.transform.position = hit.point;
                    decal.transform.rotation = Quaternion.LookRotation(hit.normal);
                    decal.transform.Translate(decal.transform.forward * 0.01f, Space.World);
                    decal.gameObject.SetActive(true);
                    // decal.GetComponent<ParticleSystem>().Play();
                }


                /*int index = Random.Range(0, _bloodHitEffects.Length);

                impactBlood = Instantiate(_bloodHitEffects[index].gameObject, hit.point,
                    Quaternion.LookRotation(hit.normal), _bloodContainer);
                impactBlood.transform.Translate(impactBlood.transform.forward * 0.01f, Space.World);*/
            }

            return;
        }


        /*
        if (hit.transform.TryGetComponent(out Bomb bomb))
            HitedBomb?.Invoke();
            */

        if (hit.transform.TryGetComponent<Bomb>(out _))
        {
            _explosionEffect.transform.position = hit.point;
            _explosionEffect.gameObject.SetActive(true);
            
            hit.collider.TryGetComponent<Bomb>(out Bomb bomb);
            {
                bomb.Explosion();
            }
            
            HitedBomb?.Invoke();
        }


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
            string poolKey = environment.IsStone ? "StoneDecal" : "MetalDecal";

            if (_effectsPools[poolKey].TryGetObject(out Decal impactDecal,
                    environment.IsStone ? _decalEffectStone : _decalEffectMetall))
            {
                impactDecal.transform.position = hit.point;
                impactDecal.transform.rotation = Quaternion.LookRotation(hit.normal);
                // impactDecal.transform.SetParent(hit.collider.transform);
                impactDecal.transform.Translate(impactDecal.transform.forward * 0.01f, Space.World);
                impactDecal.gameObject.SetActive(true);
            }

            /*if (environment.IsStone)
            {
                impactGO = Instantiate(_decalEffectStone.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.transform.SetParent(hit.collider.transform);
                impactGO.transform.Translate(impactGO.transform.forward * 0.01f, Space.World);
            }
            else if (!environment.IsStone)
                impactGO = Instantiate(_decalEffectMetall.gameObject, hit.point, Quaternion.LookRotation(hit.normal));*/
        }
    }
}