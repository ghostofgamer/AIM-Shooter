using UnityEngine;

namespace SDKContent
{
    public class SDKGameReady : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
YandexGamesSdk.GameReady();
#endif
        }
    }
}