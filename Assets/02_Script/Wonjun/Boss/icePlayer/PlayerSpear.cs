using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpear : MonoBehaviour
{
    [Header("Hit Value")]
    [SerializeField]
    private List<string> _hitObjectTagNameList = new List<string>();
    [SerializeField]
    private bool _hitAndDelete = false;

    public float Damage = 150f;


    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool check = false;
        for (int i = 0; i < _hitObjectTagNameList.Count; ++i)
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

        if (col.transform.TryGetComponent<HitObject>(out HitObject hitObject))
            hitObject.TakeDamage(Damage);

        if (_hitAndDelete)
            Destroy(gameObject);
    }
}
