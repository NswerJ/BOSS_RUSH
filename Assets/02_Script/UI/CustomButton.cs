using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private UnityEvent clickEvent;
    [SerializeField] private Color originColor = Color.white;
    [SerializeField] private Color hoverColor = Color.white;
    public Color HoverColor => hoverColor;
    [SerializeField] private float hoverFadingSpeed = 2f;

    private TMP_Text text;
    private float per;
    private bool isHover;
    public bool IsHover => isHover;

    public bool useImage = false;
    private Image image;

    private void Awake()
    {

        text = GetComponentInChildren<TMP_Text>();
        image = GetComponentInChildren<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        clickEvent?.Invoke();

    }

    private void Update()
    {
        
        if(isHover)
        {

            per += hoverFadingSpeed * Time.deltaTime;

        }
        else
        {

            per -= hoverFadingSpeed * Time.deltaTime;

        }

        per = Mathf.Clamp01(per);

        text.color = Color.Lerp(originColor, hoverColor, per);
        if(useImage)
            image.color = Color.Lerp(originColor, hoverColor, per);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        isHover = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        isHover = false;

    }

}