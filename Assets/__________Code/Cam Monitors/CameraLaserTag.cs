using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaserTag : MonoBehaviour
{
    public static bool IsHitting;
    public static Vector3 CamHitPosition;

    public int maxDistance = 100;

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, Layers.UI))
        {
            IsHitting = true;
            CamHitPosition = hit.point;

            Debug.Log(hit.point);
            Debug.Log(hit.point - hit.transform.position);
        }
        else
        {
            IsHitting = false;
        }
    }


    private void OnDrawGizmos()
    {
        if (IsHitting)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, CamHitPosition);
        }
    }
}
