using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOnDeathDetach : MonoBehaviour
{
    public Transform cameraHast;

    private void OnDestroy()
    {
        cameraHast.parent = transform.parent;
    }
}
