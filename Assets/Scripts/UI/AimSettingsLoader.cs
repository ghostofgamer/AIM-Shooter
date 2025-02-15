using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace UI
{
    public class AimSettingsLoader : MonoBehaviour
    {
        private const string Aim = "Aim";
        private const string AimColor = "AimColor";
        private const string AimScale = "AimScale";

        [SerializeField] private Image _aim;
        [SerializeField] private Color[] _aimColors;
        [SerializeField] private AssetReference[] _aimSpritesReferences;

        private Dictionary<AssetReference, Sprite> _spriteCache = new Dictionary<AssetReference, Sprite>();
        private Dictionary<AssetReference, AsyncOperationHandle<Sprite>> _spriteHandles = new Dictionary<AssetReference, AsyncOperationHandle<Sprite>>();
        private int _indexAim;
        private int _indexColor;
        private int _factorScale;

        private async void Start()
        {
            _indexAim = PlayerPrefs.GetInt(Aim, 1);
            _indexColor = PlayerPrefs.GetInt(AimColor, 1);
            _factorScale = PlayerPrefs.GetInt(AimScale, 6);
            _aim.color = _aimColors[_indexColor];
            _aim.transform.localScale = Vector3.one * _factorScale;
            
            Sprite sprite;

            if (_spriteCache.TryGetValue(_aimSpritesReferences[_indexAim], out sprite))
            {
                _aim.sprite = sprite;
            }
            else
            {
                AsyncOperationHandle<Sprite> handle = _aimSpritesReferences[_indexAim].LoadAssetAsync<Sprite>();
                _spriteHandles[_aimSpritesReferences[_indexAim]] = handle;
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    sprite = handle.Result;
                    _spriteCache[_aimSpritesReferences[_indexAim]] = sprite;
                    _aim.sprite = sprite;
                }
                else
                {
                    Debug.LogError("Failed to load sprite.");
                }

                Addressables.Release(handle);
            }
        }

        private void OnDestroy()
        {
            foreach (var handle in _spriteHandles.Values)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }

            _spriteHandles.Clear();
            _spriteCache.Clear();
        }
    }
}