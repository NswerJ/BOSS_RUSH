using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningText : MonoBehaviour
{

    private TMP_Text text;

    private void Awake()
    {

        Debug.Log(gameObject.name);
        text = GetComponent<TMP_Text>();
        text.text = "";

    }

    public void SetText(string text, float duration)
    {

        this.text.text = text;

        StartCoroutine(TextSettingCo(duration));

    }

    private IEnumerator TextSettingCo(float duration)
    {

        yield return new WaitForSeconds(duration);
        text.text = "";

    }

}
