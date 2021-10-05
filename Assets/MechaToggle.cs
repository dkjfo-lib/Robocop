using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaToggle : MonoBehaviour
{
    public float mechaHeightOn = 1;
    public float mechaHeightOff = 0;
    public float speed = 10;
    [Space]
    public LegsController LegsController;
    public PlayerMovement PlayerMovement;
    public PlayerShoot PlayerShoot;
    public MechaHealer MechaHealer;
    public CamMonitor[] monitors;
    public GameObject radar;
    [Space]
    public bool isOn = true;

    Coroutine curProcess;

    void Start()
    {

    }

    void Update()
    {
        if (curProcess == null && Input.GetKeyDown(KeyCode.Z))
        {
            if (isOn)
                curProcess = StartCoroutine(TurnOff());
            else
                curProcess = StartCoroutine(TurnOn());
        }
    }

    IEnumerator TurnOn()
    {
        MechaHealer.enabled = false;
        radar.SetActive(true);
        yield return new WaitForSeconds(.5f);
        ToggleMonitors(true);
        yield return new WaitForSeconds(.25f);
        float lerp = 0;
        while (lerp < 1)
        {
            LegsController.offsetHeight = mechaHeightOff + (mechaHeightOn - mechaHeightOff) * lerp;
            lerp += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        LegsController.offsetHeight = mechaHeightOn;
        isOn = true;
        curProcess = null;
        PlayerMovement.enabled = true;
        PlayerShoot.enabled = true;
    }
    IEnumerator TurnOff()
    {
        PlayerMovement.enabled = false;
        PlayerShoot.enabled = false;
        radar.SetActive(false);
        yield return new WaitForSeconds(.5f);
        float lerp = 0;
        while (lerp < 1)
        {
            LegsController.offsetHeight = mechaHeightOn - (mechaHeightOn - mechaHeightOff) * lerp;
            lerp += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        LegsController.offsetHeight = mechaHeightOff;
        MechaHealer.enabled = true;
        yield return new WaitForSeconds(.25f);
        ToggleMonitors(false);
        isOn = false;
        curProcess = null;
    }

    void ToggleMonitors(bool value)
    {
        foreach (var monitor in monitors)
        {
            monitor.isOn = value;
        }
    }
}
