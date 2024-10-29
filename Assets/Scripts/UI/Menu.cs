using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuScreen;
     
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            _menuScreen.SetActive(true);
        }
    }
}
