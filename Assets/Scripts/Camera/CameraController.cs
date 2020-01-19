using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _tilt;

    void Update()
    {
        // Don't execute look code when Paused
        if (Pause.Active)
        {
            return;
        }
        float mousePosition = Input.GetAxis("Mouse Y");
        _tilt = Mathf.Clamp(_tilt - mousePosition, -15f, 15f);
        transform.localRotation = Quaternion.Euler(_tilt, 0, 0);
    }
}
