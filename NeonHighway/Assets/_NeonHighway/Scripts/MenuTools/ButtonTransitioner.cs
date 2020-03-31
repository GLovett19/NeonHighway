using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 normalColor = Color.white;
    public Color32 hoverColor = Color.grey;
    public Color32 downColor = Color.white;

    private Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("enter");
        image.color = hoverColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //print("exit");
        image.color = normalColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       // print("down");

        image.color = downColor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
       // print("up");

    }
    public void OnPointerClick(PointerEventData eventData)
    {
       //print("click");

        image.color = hoverColor;
    }
}
