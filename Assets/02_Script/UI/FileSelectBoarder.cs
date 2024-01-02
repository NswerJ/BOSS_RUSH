using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileSelectBoarder : MonoBehaviour
{
    Image image;
    CustomButton customButton;

    private void Awake()
    {
        image = GetComponent<Image>();
        customButton = GetComponent<CustomButton>();
    }

    private void Update()
    {
        if(customButton.IsHover)
        {
            image.color = customButton.HoverColor;
        }
        else
        {
            image.color= Color.white;
        }
    }
}
