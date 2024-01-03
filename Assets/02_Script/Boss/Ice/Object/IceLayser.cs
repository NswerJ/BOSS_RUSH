using Cinemachine;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class IceLayser : MonoBehaviour
{
    
    private LineRenderer lineRenderer;

    private void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();

    }

    public void Show(Vector2 position, Vector2 dir)
    {

        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + dir * 300);

        lineRenderer.widthMultiplier = 0;

        StartCoroutine(ShowCo(position, dir));

    }

    public IEnumerator ShowCo(Vector2 pos, Vector2 dir)
    {

        float per = 0;

        while(per < 1)
        {

            per += Time.deltaTime * 1.15f;

            lineRenderer.widthMultiplier = Mathf.Lerp(0, 0.4f, per);

            yield return null;

        }

        yield return null;

        per = 0;

        while (per <= 1)
        {

            per += Time.deltaTime * 20;

            lineRenderer.widthMultiplier = Mathf.Lerp(0.4f, 1.5f, FAED.Easing(FAED_Easing.InOutBounce, per));

            yield return null;

        }
        yield return null;

        per = 0;

        while (per <= 1)
        {

            per += Time.deltaTime * 80;

            lineRenderer.widthMultiplier = Mathf.Lerp(1.5f, 1f, FAED.Easing(FAED_Easing.InOutBounce, per));

            yield return null;

        }

        yield return null;

        var hit = Physics2D.RaycastAll(pos, dir, 300, LayerMask.GetMask("Player"));

        if(hit.Length != 0)
        {

            foreach(var item in hit)
            {

                item.transform.GetComponent<HitObject>().TakeDamage(15f);

            }

        }

        yield return null;

        per = 0;

        while (per < 1)
        {

            per += Time.deltaTime * 16f;

            lineRenderer.widthMultiplier = Mathf.Lerp(2, 0, FAED.Easing(FAED_Easing.InQuad, per));

            yield return null;

        }

        FAED.InsertPool(gameObject);

    }

}