using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _tilt;

    void Update()
    {
        float mousePosition = Input.GetAxis("Mouse Y");
        _tilt = Mathf.Clamp(_tilt - mousePosition, -15f, 15f);
        transform.localRotation = Quaternion.Euler(_tilt, 0, 0);
    }
}
