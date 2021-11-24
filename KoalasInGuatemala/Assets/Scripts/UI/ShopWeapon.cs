using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] RectTransform priceGUI;
    [SerializeField] int weapon;
    [SerializeField] int price;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private float y;
    [SerializeField] LeanTweenType easeType;
    private bool owned;

    void Start()
    {
        text.text = "" + price;
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        LeanTween.move(priceGUI, new Vector2(6f, y), .1f).setEase(easeType);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        LeanTween.move(priceGUI, new Vector2(35f, y), .1f);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!owned)
        {
            if (PlayerController.gemCount >= price)
            {
                PlayerController.gemCount -= price;
                Shooting.ownedGuns[weapon] = true;
                owned = true;
                priceGUI.GetComponent<Image>().enabled = false;
                text.text = "OWNED";
            }
            else
            {
                LeanTween.move(priceGUI, new Vector2(8f, y), .1f).setEase(LeanTweenType.easeShake);
            }
        }
    }
}
