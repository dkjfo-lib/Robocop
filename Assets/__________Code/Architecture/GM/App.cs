using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    static App app;

    private void Start()
    {
        if (app != null) Destroy(gameObject);

        app = this;
        DontDestroyOnLoad(gameObject);
    }
}
