using DG.Tweening;
using FD.Dev;
using System.Collections;
using UnityEngine;

public class IceClone : MonoBehaviour
{

    private Vector3 pos;
    private float per;
    private bool isMove;

    private void OnDisable()
    {

        isMove = false;

    }

    public void Spawn(Transform target)
    {

        per = 0;
        isMove = true;
        pos = transform.position;
        StartCoroutine(ShardAttack(target));

    }
    private IEnumerator ShardAttack(Transform target)
    {

        float time = Time.time;

        while (Time.time - time < 13)
        {

            if(target == null) yield break;

            FAED.TakePool<IceShard>("IceShard", transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity).Spawn(target, 0.3f);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));

        }

        yield return new WaitForSeconds(2);

        FAED.InsertPool(gameObject);

    }



    private void Update()
    {
        if (isMove)
        {

            transform.position = pos + MoveToInf(per * 2);

        }

    }

    private Vector3 MoveToInf(float t)
    {

        float x = Mathf.Cos(t) * 2;
        float y = Mathf.Sin(t) * Mathf.Cos(t) * 2;

        per += Time.deltaTime;

        return new Vector2(x, y);

    }

}
