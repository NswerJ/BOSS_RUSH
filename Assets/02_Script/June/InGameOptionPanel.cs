using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameOptionPanel : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            _panel.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        }
    }

    public void LobbyBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void ContinueBtn()
    {
        Time.timeScale = 1f;
        _panel.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
    }
}