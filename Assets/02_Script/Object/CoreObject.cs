using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreObject : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    private bool isIn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        isIn = true;
        text.gameObject.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (!collision.CompareTag("Player")) return;
        isIn = false;
        text.gameObject.SetActive(false);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && isIn)
        {

            SceneManager.LoadScene("Main");

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        isIn = true;


    }

}
