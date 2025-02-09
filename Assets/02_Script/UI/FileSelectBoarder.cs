using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileSelectBoarder : MonoBehaviour
{
    Image image;
    CustomButton customButton;

    [SerializeField] float x = 160f;
    [SerializeField] float y = 160f;

    [SerializeField] Sprite fillImage;
    [SerializeField] Sprite emptyImage;
    [SerializeField] Image setImage;
    [SerializeField] TextMeshProUGUI tmpro;

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

    public void SetFill(int num)
    {
        setImage.rectTransform.sizeDelta = new Vector2(160f, 160f);
        setImage.sprite = fillImage;
        tmpro.text = "사망 횟수 : " + num;
    }

    public void SetEmpty()
    {
        setImage.rectTransform.sizeDelta = new Vector2(x, y);
        setImage.sprite= emptyImage;
    }
}
