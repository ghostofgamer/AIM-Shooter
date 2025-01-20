using UnityEngine;
using UnityEngine.UI;

public class AimSettingsLoader : MonoBehaviour
{
    [SerializeField] private Image _aim;
    [SerializeField]private Sprite[] _aimSprites;
    [SerializeField]private Color[] _aimColors;
    
    private void Start()
    {
        int indexAim = PlayerPrefs.GetInt("Aim",1);
        int indexColor = PlayerPrefs.GetInt("AimColor",1);
        int factorScale = PlayerPrefs.GetInt("AimScale",6);
        
        _aim.sprite = _aimSprites[indexAim];
        _aim.color = _aimColors[indexColor];
        _aim.transform.localScale = Vector3.one * factorScale;
    }
}
