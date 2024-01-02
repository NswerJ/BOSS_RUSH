using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{

    private TMP_Text text;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
        text = GetComponentInChildren<TMP_Text>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void SetSize(float value)
    {

        spriteRenderer.size = new Vector2(value, 2);

    }

    public void SetText(string text)
    {

        this.text.text = text;

    }

}
