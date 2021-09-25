using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSinglton : MonoBehaviour
{
    public static bool IsGood => thePlayer != null;
    public static Vector3 PlayerPosition => thePlayer.transform.position;
    public static Vector3 PlayerDirection => thePlayer.transform.forward;
    static PlayerSinglton thePlayer;

    public static PlayerHittable PlayerHealth => thePlayer.playerHealth;
    public PlayerHittable playerHealth;

    public static Transform CamPosition => thePlayer.camPosition;
    public Transform camPosition;

    void Awake()
    {
        thePlayer = this;
    }
}
