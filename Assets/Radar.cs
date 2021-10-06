using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public float radius = 10;
    public List<BotStats> bots;
    public GameObject[] DetectionMarkers;
    public float projectionRadius = 1;

    void Start()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).gameObject);
        }
        DetectionMarkers = list.ToArray();
        GetComponent<SphereCollider>().radius = radius;
    }

    private void Update()
    {
        var Detections = CalculateDirections();
        DisplayDirections(Detections);
    }

    Vector3[] CalculateDirections()
    {
        var Detections = new List<Vector3>();
        bots = bots.Where(s => s != null).ToList();
        foreach (var bot in bots)
        {
            Vector3 direction = bot.transform.position - transform.position;
            direction /= radius;
            Detections.Add(direction);
        }
        return Detections.ToArray();
    }

    void DisplayDirections(Vector3[] Detections)
    {
        transform.localRotation = Quaternion.Euler(-transform.parent.parent.rotation.eulerAngles);
        for (int i = 0; i < Detections.Length; i++)
        {
            DetectionMarkers[i].transform.rotation = Quaternion.Euler(bots[i].transform.rotation.eulerAngles + Vector3.right * 90);
            DetectionMarkers[i].transform.localPosition = Detections[i] * projectionRadius;
            DetectionMarkers[i].SetActive(true);
        }
        for (int i = Detections.Length; i < DetectionMarkers.Length; i++)
        {
            DetectionMarkers[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var bot = other.transform.parent.GetComponent<BotStats>();
        if (bot != null)
        {
            bots.Add(bot);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var bot = other.transform.parent.GetComponent<BotStats>();
        if (bot != null)
        {
            bots.Remove(bot);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, projectionRadius);
    }
}