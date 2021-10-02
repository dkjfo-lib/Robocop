using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegsController : MonoBehaviour
{
    public Transform controlledObj;
    public Leg[] pair1;
    public Leg[] pair2;

    float offsetY = 0;

    private void Start()
    {
        offsetY = GetY() - controlledObj.position.y;
    }

    void Update()
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

        controlledObj.position = new Vector3(controlledObj.position.x, GetY() + offsetY, controlledObj.position.z);
    }

    bool LegsAreMoving(IEnumerable<Leg> legs) => !legs.All(s => !s.IsWalking);

    float GetY()
    {
        float posY = 0;
        foreach (var item in pair1)
        {
            posY += item.feetIK.Target.position.y;
        }
        foreach (var item in pair2)
        {
            posY += item.feetIK.Target.position.y;
        }
        posY /= (pair1.Length + pair2.Length);
        return posY;
    }
}
