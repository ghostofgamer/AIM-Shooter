using UnityEngine;
using UnityEngine.UI;

public class AimChanger : MonoBehaviour
{
    [SerializeField] private Sprite _targetAim;
    [SerializeField] private Image _aim;

    public void ChangeAim()
    {
        _aim.sprite = _targetAim;
    }
}
