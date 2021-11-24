using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    public static int waveNum;
    [SerializeField] private LeanTweenType easeType;
    

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = $"WAVE {waveNum} COMPLETED";
        LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector2(0, 0), .75f).setEase(easeType);
        LeanTween.scale(gameObject, new Vector2(0, 0), .15f).setDelay(4f).setDestroyOnComplete(true);
    }
}
