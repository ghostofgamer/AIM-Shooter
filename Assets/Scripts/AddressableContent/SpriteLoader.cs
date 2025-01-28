using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressableContent
{
    public class SpriteLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference spriteReference;
    
        private SpriteRenderer spriteRenderer;
        private AsyncOperationHandle<Sprite> _loadedSpriteHandle;
    
        private async void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            LoadSprite();
        }

        private async void LoadSprite()
        {
            _loadedSpriteHandle = spriteReference.LoadAssetAsync<Sprite>();
            await _loadedSpriteHandle.Task;
        
            if (_loadedSpriteHandle.Status == AsyncOperationStatus.Succeeded)
                spriteRenderer.sprite = _loadedSpriteHandle.Result;
            else
                Debug.LogError("Failed to load sprite.");
        }
    
        private void OnDestroy()
        {
            if (_loadedSpriteHandle.IsValid())
                Addressables.Release(_loadedSpriteHandle);
        }
    }
}