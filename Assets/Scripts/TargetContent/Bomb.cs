using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private GameObject _item;
    [SerializeField]private Collider _collider;
    [SerializeField]private AudioSource _audio;
    
    public void Explosion()
    {
        _item.gameObject.SetActive(false);
        _collider.enabled = false;
        // _audio.PlayOneShot(_audio.clip);
        // _effect.Play();
    }
}