using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSScript : MonoBehaviour
{
    public int targetFrameRate = 144;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
    private void Awake()
    {
        float fixedTimestep = 1f / targetFrameRate;
        Time.fixedDeltaTime = fixedTimestep;
    }

    void Update()
    {

    }
}
