using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames.FastIK;

public class Leg : MonoBehaviour
{
    public FastIKFabric feetIK;
    public LegTargetCaster targetCaster;
    [Space]
    public float stepDist = 2;
    public float stepHeight = 2;
    public float legSpeed = 10;

    Transform target;

    void Start()
    {
        target = feetIK.Target;
        StartCoroutine(UpdateLeg());
    }

    IEnumerator UpdateLeg()
    {
        Vector3 diff() => targetCaster.targetPosition - target.position;
        float dist() => diff().sqrMagnitude;

        while (true)
        {
            yield return new WaitUntil(() => targetCaster.HasTarget && dist() > stepDist);
            yield return Step(diff().normalized * stepDist);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator Step(Vector3 direction)
    {
        var lerp = 0f;
        var startPos = target.transform.position;
        var endPos = startPos + direction;
        while (lerp < 1)
        {
            Vector3 newPos = Vector3.Lerp(startPos, endPos, lerp);
            newPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            target.transform.position = newPos;
            lerp += Time.deltaTime * legSpeed;
            yield return new WaitForEndOfFrame();
        }
        target.transform.position = endPos;
    }

    void OnDestroy()
    {
        Destroy(target.gameObject);
    }
}
