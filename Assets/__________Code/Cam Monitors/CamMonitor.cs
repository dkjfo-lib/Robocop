using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMonitor : MonoBehaviour
{
    public static bool IsHitting;
    public static Vector3 CamHitPosition;
    public static Vector3 CamDirection;

    public RenderTexture OriginalRenderTexture;
    public Material OriginalMaterial;
    public Camera targetCam;
    RenderTexture renderTexture => targetCam.targetTexture;
    [Space]
    public bool isOn;
    bool _isOn;
    public bool isBw;
    bool _isBw;

    // mesh -> verts -> planeDeltaPoints -> planeSize
    //MeshFilter meshFilter;
    //Mesh mesh => meshFilter.mesh;
    //Vector2 max = new Vector2(-.2f, .2f);
    //Vector2 planeSize = new Vector2(.4f, .4f);

    Material material;

    void Start()
    {
        //meshFilter = GetComponent<MeshFilter>();

        var renderTexture = new RenderTexture(OriginalRenderTexture);
        renderTexture.name = "mecha view";
        targetCam.targetTexture = renderTexture;
        material = new Material(OriginalMaterial);
        material.name = "mecha view material";
        material.SetTexture("_BaseMap", renderTexture);
        GetComponent<MeshRenderer>().material = material;

        StartCoroutine(KeepOn());
        StartCoroutine(KeepBW());
    }

    IEnumerator KeepOn()
    {
        while (true)
        {
            material.SetInt("_isOn", isOn ? 1 : 0);
            _isOn = isOn;
            yield return new WaitUntil(() => _isOn != isOn);
        }
    }

    IEnumerator KeepBW()
    {
        while (true)
        {
            material.SetInt("_isBw", isBw ? 1 : 0);
            _isBw = isBw;
            yield return new WaitUntil(() => _isBw != isBw);
        }
    }

    void FixedUpdate()
    {
        var deltaPoint = GetDeltaPoint();
        if (deltaPoint.x > 1 || deltaPoint.x < 0 || deltaPoint.y > 1 || deltaPoint.y < 0) return;

        var renderTexturePixelSize = new Vector2(renderTexture.width, renderTexture.height);
        var pixelPoint = renderTexturePixelSize * deltaPoint;

        Ray ray = targetCam.ScreenPointToRay(pixelPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, Layers.CharactersAndGround))
        {
            IsHitting = true;
            CamHitPosition = hit.point;
            CamDirection = ray.direction;
        }
        else
        {
            IsHitting = false;
            CamDirection = ray.direction;
        }
    }

    Vector3 GetDeltaPoint()
    {
        var hitPoint = CameraLaserTag.CamHitPosition - transform.position;
        var hitPointLocal = transform.worldToLocalMatrix * hitPoint;
        var hitPointLocal2D = new Vector2(hitPointLocal.x, hitPointLocal.z) + Vector2.one * .2f;
        var hitPointPercent = hitPointLocal2D / .4f;
        return hitPointPercent;
    }

    private void OnDrawGizmosSelected()
    {
        if (IsHitting)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(targetCam.transform.position, CamHitPosition);
        }
    }
}
