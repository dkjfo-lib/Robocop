using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotationY : MonoBehaviour
{
    public static float RotationY { get; private set; } = 0;

    public Number CameraSensitivity;
    float rotationSpeed => CameraSensitivity.value;
    public float limitAngle = 80;

    void FixedUpdate()
    {
        float addAngleY = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;

        float newAngleY;
        if (transform.localRotation.eulerAngles.y > 180)
            newAngleY = Mathf.Clamp(transform.localRotation.eulerAngles.y - 360 + addAngleY, -limitAngle, limitAngle);
        else
            newAngleY = Mathf.Clamp(transform.localRotation.eulerAngles.y + addAngleY, -limitAngle, limitAngle);

        RotationY = newAngleY;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, newAngleY, transform.localRotation.eulerAngles.z);
    }
}
