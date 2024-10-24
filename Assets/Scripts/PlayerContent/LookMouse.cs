using UnityEngine;

public class LookMouse : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _sensitivityMouse = 100f;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _sensitivityMouse * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivityMouse * Time.deltaTime;
        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _body.Rotate(Vector3.up * _mouseX);
    }
}