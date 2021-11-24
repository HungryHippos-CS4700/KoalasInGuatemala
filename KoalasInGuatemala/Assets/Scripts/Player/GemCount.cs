using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCount : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = ""+PlayerController.gemCount;
    }
}
