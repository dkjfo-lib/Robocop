using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool onGround => staysOnGround && onGroundTime > .05f;
    bool staysOnGround = false;
    float onGroundTime => Time.timeSinceLevelLoad - onGroundStart;
    float onGroundStart = 100;

    private void OnTriggerEnter(Collider collision)
    {
        onGroundStart = Time.timeSinceLevelLoad;
        staysOnGround = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        //onGroundStart = Time.timeSinceLevelLoad;
        staysOnGround = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        staysOnGround = false;
    }
}
