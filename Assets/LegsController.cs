using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegsController : MonoBehaviour
{
    public Transform controlledObj;
    public Leg[] pair1;
    public Leg[] pair2;

    Vector3 offsetY;

    private void Start()
    {
        offsetY = GetY() - controlledObj.position;
    }

    void FixedUpdate()
    {
        if (!LegsAreMoving(pair2) && !pair1.All(s => !s.NeedsWalking && !s.IsWalking))
        {
            foreach (var leg in pair1.Where(s => !s.IsWalking))
            {
                leg.Step();
            }
        }
        if (!LegsAreMoving(pair1) && !pair2.All(s => !s.NeedsWalking && !s.IsWalking))
        {
            foreach (var leg in pair2.Where(s => !s.IsWalking))
            {
                leg.Step();
            }
        }

        controlledObj.position = GetY() + offsetY;
    }

    bool LegsAreMoving(IEnumerable<Leg> legs) => !legs.All(s => !s.IsWalking);

    Vector3 GetY()
    {
        Vector3 pos = Vector3.zero;
        float posY = 0;
        foreach (var item in pair1)
        {
            pos += item.targetCaster.targetPosition;
            pos += item.feetIK.Target.position;
            posY += item.feetIK.Target.position.y;
        }
        foreach (var item in pair2)
        {
            pos += item.targetCaster.targetPosition;
            pos += item.feetIK.Target.position;
            posY += item.feetIK.Target.position.y;
        }
        pos /= (pair1.Length + pair2.Length) * 2;
        posY /= (pair1.Length + pair2.Length);
        pos.y = posY;
        return pos;
    }
}
