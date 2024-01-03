using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FD.Dev;
using System;
using UnityEngine.AI;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private GameObject perfab;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private WeaponDataSO data;

    public event Action<Vector3> attackEvent;

    public WeaponDataSO Data
    {
        get { return data; }
        set
        {
            data = value;
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            spriteRenderer.sprite = data.weaponImage;
            perfab = data.attackPrefab;
        }
    }

    private PlayerEventSystem playerEventSystem;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    private Transform flipPoint;
    private Transform point;
    private bool isCool;

    private Vector3 dir;

    private void Start()
    {

        point = transform.Find("Point");
        flipPoint = point.Find("FlipPoint");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        playerEventSystem = playerController.playerEventSystem;
        spriteRenderer.sprite = data.weaponImage;

    }

    private void Update()
    {

        if (Time.timeScale == 0) return;
        if (playerController.CurrentState != EnumPlayerState.Move) return;

        Rotate();
        Attack();

    }


    private void Rotate()
    {

        var mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dir = (mpos - transform.position).normalized;
        dir.z = 0;

        spriteRenderer.flipY = dir.x > 0;

        if (dir.x > 0)
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

        if (Input.GetMouseButtonDown(0) && !isCool)
        {
            attackEvent?.Invoke(dir.normalized);
            isCool = true;

            point.transform.localScale = new Vector3(1, point.transform.localScale.y * -1, 1);
            Instantiate(perfab, transform.position - point.right, Quaternion.identity).transform.right = point.right;
            spriteRenderer.sortingOrder *= -1;
            point.transform.localPosition = point.transform.localPosition.y == 0 ? new Vector3(0f, -0.5f, 0) : new Vector3(0, 0, 0);

            var hits = Physics2D.OverlapBoxAll(transform.position - point.right * data.AttackRange, Vector2.one * data.AttackSize, point.transform.eulerAngles.z, enemyLayer);

            playerEventSystem.AttackEventExecute(data.AttackPower);

            FAED.InvokeDelay(() =>
            {

                isCool = false;

            }, data.AttackCool);

            if (hits.Length > 0)
            {

                foreach (var hit in hits)
                {

                    if (hit.TryGetComponent<HitObject>(out HitObject ho))
                        ho.TakeDamage(data.AttackPower);

                }

            }

        }


    }

}
