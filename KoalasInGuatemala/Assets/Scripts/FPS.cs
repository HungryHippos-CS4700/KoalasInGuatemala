using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    float timeSinceLastRefresh;

    void Start()
    {
        // Application.targetFrameRate = 60;
        timeSinceLastRefresh = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastRefresh > .25)
        {
            GetComponent<Text>().text = "" + 1/Time.unscaledDeltaTime;
            timeSinceLastRefresh = 0;
        }
        else
        {
            timeSinceLastRefresh += Time.deltaTime;
        }
    }
}
