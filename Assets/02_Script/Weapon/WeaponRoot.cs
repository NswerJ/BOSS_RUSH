using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponRoot : MonoBehaviour
{
    protected Vector3 MousePoint, target;
    protected float angle;

    [field: SerializeField] public WeaponDataSO Data { get; protected set; }

    protected virtual void Awake()
    {
        target = transform.position;
        Data = Instantiate(Data);
        Data.Init(this);
    }

    public abstract void DoAttack(Vector3 mousePoint);

    protected virtual void Update() {

        MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(MousePoint.y - target.y, MousePoint.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (!Data.isAttackCoolDown && MousePoint != null && Input.GetMouseButton(0))
        {

            Data.SetCoolDown();
            DoAttack(MousePoint);

        }
    }

}
