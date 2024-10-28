using UnityEngine;

public class LookMouse : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _sensitivityMouse = 100f;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation;

    private float _xOffset = 0;
    private float _yOffset = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        /*_mouseX = Input.GetAxis("Mouse X") * _sensitivityMouse * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivityMouse * Time.deltaTime;*/
        _mouseX = Input.GetAxis("Mouse X") * _sensitivityMouse * Time.deltaTime + _xOffset * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivityMouse * Time.deltaTime + _yOffset * Time.deltaTime;

        _xOffset = 0f;
        _yOffset = 0f;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _body.Rotate(Vector3.up * _mouseX);
    }

    public void ChangeOffset(float x, float y)
    {
        _xOffset = x;
        _yOffset = y;
    }
}