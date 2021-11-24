using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveCountdown : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = $"NEXT WAVE IN {Mathf.RoundToInt(WaveSpawner.waveCountdown)}";
    }
}
