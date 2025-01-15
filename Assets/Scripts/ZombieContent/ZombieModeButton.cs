using UnityEngine;

public class ZombieModeButton : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField]private ZombieMode _zombieMode;

    public void Click()
    {
        _zombieMode.ChangeMode(_index);
    }
}
