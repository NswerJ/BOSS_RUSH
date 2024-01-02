using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DemonBook : MonoBehaviour
{
    [Header("AI Value")]
    [SerializeField]
    private bool _stop = false;

    [Header("Attack Value")]
    [SerializeField]
    private GameObject _fireballPrefab;

    [Header("Move Value")]
    [SerializeField]
    private Transform _point;
    [SerializeField]
    private float _speed = 3f;

    private bool _randCheck = false;
    private Vector3 _randPosition;

    private Transform _player;

    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _point = GameObject.Find("Point").transform;
    }

    void Update()
    {
        if (_stop) return;

        MoveBook();
    }

    private void MoveBook()
    {
        if (Vector3.Distance(_player.position, transform.position) <= 3f)
        {
            RunAway();
        }
        else
        {
            RandomMove();
        }
    }

    private void RunAway()
    {
        Vector3 dir = (_point.position - _player.position).normalized;
        Vector3 movePos = _player.position + dir * 8f;

        _randCheck = false;

        if (Vector3.Distance(movePos, transform.position) <= 0.2f)
            return;

        Vector3 moveDir = (movePos - transform.position).normalized;
        transform.position += moveDir * _speed * Time.deltaTime;
    }

    private void RandomMove()
    {
        if(_randCheck == false)
        {
            _randPosition = transform.position + 
                (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized) * Random.Range(1f, 2f);

            _randCheck = true;
        }

        Vector3 moveDir = (_randPosition - transform.position).normalized;
        transform.position += moveDir * _speed * Time.deltaTime;

        if (Vector3.Distance(_randPosition, transform.position) <= 0.2f)
        {
            _randCheck = false;
        }
    }
}
