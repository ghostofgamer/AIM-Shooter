using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScrollPosition : MonoBehaviour
    {
        public ScrollRect scrollRect;

        private void OnEnable()
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
}
