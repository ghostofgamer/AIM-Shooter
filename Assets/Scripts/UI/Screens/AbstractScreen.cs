using UnityEngine;

namespace UI.Screens
{
    public abstract class AbstractScreen : MonoBehaviour
    {
        public virtual void Open()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}
