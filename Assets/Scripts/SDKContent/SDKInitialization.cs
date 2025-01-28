using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SDKContent
{
    public class SDKInitialization : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene(1);
        }
    }
}