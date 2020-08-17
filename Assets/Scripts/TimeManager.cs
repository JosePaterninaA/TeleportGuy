using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;

    public float slowdownLength = 2f;

    private bool slowMotion = false;

    public void SlowMotion()
    {
        slowMotion = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    void Update()
    {
        if (slowMotion && Time.timeScale<1)
        {
            Time.timeScale += Time.unscaledDeltaTime*(1 / slowdownLength);
        }

        if (Mathf.Abs(Time.timeScale - 1)<0.02f)
        {
            slowMotion = false;
        }
        
    }
}
