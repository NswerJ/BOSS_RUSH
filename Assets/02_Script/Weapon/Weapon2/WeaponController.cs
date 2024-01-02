using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FD.Dev;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private GameObject perfab;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private WeaponDataSO data;

    private Transform point;
    private Transform flipPoint;
    private SpriteRenderer spriteRenderer;
    private bool isCool;

    private void Start()
    {

        point = transform.Find("Point");
        flipPoint = point.Find("FlipPoint");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    private void Update()
    {

        Rotate();
        Attack();

    }

    private void Rotate()
    {

        var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var dir = (mpos - transform.position).normalized;
        dir.z = 0;

        spriteRenderer.flipY = dir.x > 0;

        if(dir.x > 0)
        {

            flipPoint.localScale = new Vector2(-1, 1);

        }
        else
        {

            flipPoint.localScale = new Vector2(1, 1);

        }

        point.right = -dir;

    }

    private void Attack()
    {

        if(Input.GetMouseButtonDown(0) && !isCool)
        {

            isCool = true;

            point.transform.localScale = new Vector3(1, point.transform.localScale.y * -1, 1);
            Instantiate(perfab, transform.position - point.right, Quaternion.identity).transform.right = point.right;
            spriteRenderer.sortingOrder *= -1;
            point.transform.localPosition = point.transform.localPosition.y == 0 ? new Vector3(0f, -0.5f, 0) : new Vector3(0, 0, 0);

            var hits = Physics2D.OverlapBoxAll(transform.position - point.right * 1.5f, Vector2.one * 2, point.transform.eulerAngles.z, enemyLayer);

            FAED.InvokeDelay(() =>
            {

                isCool = false;

            }, data.AttackCool);

            if(hits.Length > 0)
            {

                foreach( var hit in hits)
                {

                    hit.GetComponent<HitObject>().TakeDamage(data.AttackPower);

                }

            }

        }


    }

}
