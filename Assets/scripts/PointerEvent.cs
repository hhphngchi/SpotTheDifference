using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color normalColor = Color.white;
    public Color enterColor = Color.white;
    public Color downColor = Color.white;
    public UnityEvent onClick = new UnityEvent();

    private MeshRenderer mesh = null;
    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mesh.material.color = enterColor;
        print("enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mesh.material.color = normalColor;
        print("exit");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mesh.material.color = downColor;
        print("down");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        mesh.material.color = enterColor;
        print("up");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
        print("click");
    }
}
