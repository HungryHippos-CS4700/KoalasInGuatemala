using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowUnderline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject underlineImage;
    public void OnPointerEnter(PointerEventData eventData) {
        underlineImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        underlineImage.SetActive(false);
    }
}
