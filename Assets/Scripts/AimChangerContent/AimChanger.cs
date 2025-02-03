using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace AimChangerContent
{
    public class AimChanger : MonoBehaviour, IValueChanger
    {
        private const string Aim = "Aim";
        
        [SerializeField] private Image _aim;
        [SerializeField] private int _index;
        [SerializeField] private AssetReference _targetAimReference;

        private AsyncOperationHandle<Sprite> _loadOperationHandle;
    
        private static Dictionary<AssetReference, Sprite> _spriteCache = new Dictionary<AssetReference, Sprite>();

        public async void Stop()
        {
            Sprite sprite;

            if (_spriteCache.TryGetValue(_targetAimReference, out sprite))
            {
                _aim.sprite = sprite;
                PlayerPrefs.SetInt(Aim, _index);
            }
            else
            {
                AsyncOperationHandle<Sprite> handle = _targetAimReference.LoadAssetAsync<Sprite>();
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    sprite = handle.Result;
                    _spriteCache[_targetAimReference] = sprite;
                    _aim.sprite = sprite;
                    PlayerPrefs.SetInt(Aim, _index);
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
            if (_loadOperationHandle.IsValid())
                Addressables.Release(_loadOperationHandle);
        }
    }
}