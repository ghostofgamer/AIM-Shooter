using Interfaces;
using UnityEngine;

namespace EnvironmentContent
{
    public class MenuButtons : MonoBehaviour,IValueChanger
    {
        [SerializeField] private PauseScreen _pauseScreen;
    
        public void Stop()
        {
            _pauseScreen.Open();
        }
    }
}
