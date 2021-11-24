using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private LeanTweenType easeType;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        string infDamageText = (Random.Range(0f, 1f) > .5f) ? "INF" : "POW";
        text.text = damage > 900 ? "INF" : "" + damage;
        LeanTween.moveY(gameObject, transform.position.y + 1f, 1f).setEase(easeType);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.15f);
        LeanTween.moveY(gameObject, transform.position.y + 1.5f, 0.5f).setEase(easeType).setDelay(1.5f);
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0f, .1f).setDelay(1.5f).setDestroyOnComplete(true);
    }
}
