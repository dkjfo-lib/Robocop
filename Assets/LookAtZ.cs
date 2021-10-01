using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtZ : MonoBehaviour
{
    public Transform target;

    Vector3 direction => target.position - transform.position;
    Vector3 direction2D => new Vector3(direction.x, 0, direction.z);
    Vector3 offset;

    void Start()
    {
        offset = (transform.forward - direction2D.normalized).normalized;
    }

    void Update()
    {
        var angleZ = Vector3.SignedAngle(direction2D, transform.up, Vector3.up);
        Debug.Log(angleZ);
        Debug.Log(transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(new Vector3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z - angleZ));

        //target.localRotation = Quaternion.Euler(new Vector3(
        //    target.rotation.eulerAngles.x,
        //    target.rotation.eulerAngles.y - angleZ,
        //    target.rotation.eulerAngles.z));
    }
}
