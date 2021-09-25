using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICameraSensitivityField : MonoBehaviour
{
    public Number CameraSensitivity;
    public InputField InputField;
    float localValue;

    private void Start()
    {
        localValue = CameraSensitivity.value;
        InputField.SetTextWithoutNotify(CameraSensitivity.value.ToString());
    }

    public void OnNewValue(string newString)
    {
        int newValue;
        if (int.TryParse(newString, out newValue))
        {
            CameraSensitivity.value = newValue;
        }
        else
        {
            InputField.SetTextWithoutNotify(CameraSensitivity.value.ToString());
        }
    }

    void Update()
    {
        if (localValue == CameraSensitivity.value) return;

        localValue = CameraSensitivity.value;
        InputField.SetTextWithoutNotify(localValue.ToString());
    }
}
