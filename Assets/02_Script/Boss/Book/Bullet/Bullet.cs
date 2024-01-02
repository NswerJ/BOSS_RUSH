using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Hit Value")]
    [SerializeField]
    private List<string> _hitObjectTagNameList = new List<string>();
    [SerializeField]
    private float _damage;
    [SerializeField]
    private bool _hitAndDelete = false;

    [Header("Move Value")]
    [SerializeField]
    private float _offsetZ;
    [SerializeField]
    private float _moveSpeed;

    private Vector3 _dir;

    private void Update()
    {
        transform.position += _dir * _moveSpeed * Time.deltaTime;
    }

    public void BulletRotate(float z, Vector3 dir)
    {
        transform.localEulerAngles = new Vector3(0, 0, z + _offsetZ);
        _dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool check = false;
        for(int i = 0; i < _hitObjectTagNameList.Count; ++i)
        {
            if (col.CompareTag(_hitObjectTagNameList[i]))
            {
                Debug.Log(_hitObjectTagNameList[i]);
                check = true;
                break;
            }
        }

        if (check == false)
            return;

        if(col.transform.TryGetComponent<HitObject>(out HitObject hitObject))
            hitObject.TakeDamage(_damage);

        if (_hitAndDelete)
            Destroy(gameObject);
    }
}
