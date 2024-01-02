using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponController : MonoBehaviour
{
    private bool onAttack = false;
    public float angle;
    float originAngle;
    Vector3 dir;

    Camera _camera;
    [SerializeField] WeaponDataSO data;
    [SerializeField] float offset = 120;
    [SerializeField] float nextOffset = -60;
    // -90

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (onAttack) return;

        dir = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        originAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (originAngle < 90 && -90 < originAngle)
        {
            angle = originAngle + offset;
        }
        else
        {
            angle = originAngle - offset;
        }

        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (!data.isAttackCoolDown && Input.GetMouseButtonDown(0))
        {
            Attack();
        }

    }

    public void Attack()
    {
        Debug.Log(1);

        onAttack = true;
        Sequence seq = DOTween.Sequence();

        //DOTween.To(() => angle, x => angle = x, angle + nextOffset, 0.3f)
        //.Append(transform.DORotate(new Vector3(0, 0, originAngle + nextOffset), 0.1f).SetEase(Ease.Linear))
        seq.AppendInterval(0.01f)
           .AppendCallback(() =>
           {
               GameObject obj = Instantiate(data.attackPrefab, transform.position + dir.normalized, Quaternion.identity);
               obj.GetComponent<SpriteRenderer>().flipX = true;
               obj.transform.Rotate(Vector3.forward, originAngle);
               float temp = offset;
               offset = nextOffset;
               nextOffset = temp;
               onAttack = false;
           });

        StartCoroutine(SetCoolDownCo());

    }

    private IEnumerator SetCoolDownCo()
    {

        data.isAttackCoolDown = true;

        yield return new WaitForSeconds(data.AttackCool);

        data.isAttackCoolDown = false;

    }

}
