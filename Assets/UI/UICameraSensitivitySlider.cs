using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICameraSensitivitySlider : MonoBehaviour
{
    public Number CameraSensitivity;
    public Slider CameraSensitivitySlider;
    float localValue;

    private void Start()
    {
        localValue = CameraSensitivity.value;
        CameraSensitivitySlider.SetValueWithoutNotify(CameraSensitivity.value);
    }

    public void SetNewValue(float newValue)
    {
        CameraSensitivity.value = newValue;
    }

    void Update()
    {
        if (localValue == CameraSensitivity.value) return;

        localValue = CameraSensitivity.value;
        CameraSensitivitySlider.SetValueWithoutNotify(localValue);
    }
}
