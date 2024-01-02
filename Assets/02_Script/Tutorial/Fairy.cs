using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{

    private readonly int HASH_FADE = Shader.PropertyToID("_FullDistortionFade");

    [SerializeField] private Transform fairyPos; 

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void Show()
    {

        StartCoroutine(ShowCo(true));

    }


    private void Update()
    {

        transform.position = fairyPos.position;

    }

    public void UnShow()
    {

        StartCoroutine(ShowCo(false));

    }

    private IEnumerator ShowCo(bool fade)
    {

        float per = 0;


        while(per < 1)
        {

            per += Time.deltaTime * 2;

            if (fade)
            {

                spriteRenderer.material.SetFloat(HASH_FADE, per);

            }
            else
            {

                spriteRenderer.material.SetFloat(HASH_FADE, 1 - per);

            }

            yield return null;

        }



    }

}
