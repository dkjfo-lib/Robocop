using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public ShakeCamera Addon_CamShake;
    [Space]
    public Vector3 offset = Vector2.zero;
    //public float directionOffset = 2;
    //public float zoom = 2;
    [Space]
    [Range(0f, 1f)] public float stickness = .5f;
    [Range(1f, 2f)] public float screenBorder = 1.2f;

    Vector3 Offset => offset;

    Vector3 lastTargetPosition;

    private void Start()
    {
        if (PlayerSinglton.IsGood)
        {
            transform.position = PlayerSinglton.PlayerPosition + Offset;
        }
    }

    void Update()
    {
        if (PlayerSinglton.IsGood)
        {
            lastTargetPosition = PlayerSinglton.PlayerPosition;
        }

        var pX = Mathf.Clamp((Input.mousePosition.x / Screen.width * 2 - 1) * screenBorder, -1, 1);
        var pY = Mathf.Clamp((Input.mousePosition.y / Screen.height * 2 - 1) * screenBorder, -1, 1);

        //var directionOffset = new Vector3(pX, pY, 0) * this.directionOffset;
        Vector3 targetPosition = lastTargetPosition + Offset /*+ directionOffset*/;

        if (Addon_CamShake != null)
        {
            targetPosition += Addon_CamShake.CurrentDisplacement;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, stickness * stickness);
    }
}
