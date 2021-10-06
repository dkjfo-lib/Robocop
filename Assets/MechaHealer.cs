using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaHealer : MonoBehaviour
{
    public float healPerSecond = 10;
    [Space]
    public PlayerHittable PlayerHittable;

    void FixedUpdate()
    {
        PlayerHittable.Heal(healPerSecond * Time.fixedDeltaTime);
    }
}
