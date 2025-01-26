using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AimSettingsLoader : MonoBehaviour
{
    [SerializeField] private Image _aim;
    [SerializeField]private Color[] _aimColors;
    [SerializeField] private AssetReference[] _aimSpritesReferences;
    
    private Dictionary<AssetReference, Sprite> _spriteCache = new Dictionary<AssetReference, Sprite>();
    private Dictionary<AssetReference, AsyncOperationHandle<Sprite>> _spriteHandles = new Dictionary<AssetReference, AsyncOperationHandle<Sprite>>();
    
    private async void Start()
    {
        int indexAim = PlayerPrefs.GetInt("Aim",1);
        int indexColor = PlayerPrefs.GetInt("AimColor",1);
        int factorScale = PlayerPrefs.GetInt("AimScale",6);
        
        _aim.color = _aimColors[indexColor];
        _aim.transform.localScale = Vector3.one * factorScale;
        
        
        Sprite sprite;
        
        if (_spriteCache.TryGetValue(_aimSpritesReferences[indexAim], out sprite))
        {
            _aim.sprite = sprite;
        }
        else
        {
            // Загрузка спрайта, если он не найден в кэше
            AsyncOperationHandle<Sprite> handle = _aimSpritesReferences[indexAim].LoadAssetAsync<Sprite>();
            _spriteHandles[_aimSpritesReferences[indexAim]] = handle;
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                sprite = handle.Result;
                _spriteCache[_aimSpritesReferences[indexAim]] = sprite; // Сохранение спрайта в кэше
                _aim.sprite = sprite;
            }
            else
            {
                Debug.LogError("Failed to load sprite.");
            }

            Addressables.Release(handle); // Освобождение ресурсов
        }
    }
    
    private void OnDestroy()
    {
        foreach (var handle in _spriteHandles.Values)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }

        _spriteHandles.Clear();
        _spriteCache.Clear();
    }
}
