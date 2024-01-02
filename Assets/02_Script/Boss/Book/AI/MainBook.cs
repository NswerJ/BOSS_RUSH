using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainBook : MonoBehaviour
{
    [Header("AI Value")]
    [SerializeField]
    private bool _stop = false;
    public HitObject hit; // 죽었을 때 이벤트 추가용
    public BackHit Back;

    [Header("Attack Value")]
    [SerializeField]
    private Bullet _bullet;

    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private List<float> _rotateSpeed = new List<float>(5); // 총 5개
    int _index = 0;

    bool _patternStart = false;

    [Header("Move Value")]
    [SerializeField]
    private Transform _mainTransform;


    private void Start()
    {
        Back.HitEvent += HandleHit;
        hit.DieEvent += HandleDie;
    }

    private void HandleDie()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_stop) return;

        if (_patternStart)
        {
            StartCoroutine("Pattern1");
            _patternStart = false;
        }

        RandomMove();
    }

    Vector3 _randPosition;
    bool _randCheck = false;
    float _speed = 3f;
    float _timer = 0f;

    private void RandomMove()
    {
        if (_randCheck == false)
        {
            _randPosition = transform.position +
                (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized) * Random.Range(1f, 2f);

            _randCheck = true;
            _timer = 0f;
        }

        _timer += Time.deltaTime;

        Vector3 moveDir = (_randPosition - transform.position).normalized;
        transform.position += moveDir * _speed * Time.deltaTime;

        if (Vector3.Distance(_randPosition, transform.position) <= 0.2f || _timer >= 1f)
        {
            _randCheck = false;
        }
    }

    private void HandleHit(Book book)
    {
        _stop = false;
        _patternStart = true;
        _index++;
        _speed += 1f;
    }

    IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(0.5f);
        _mainTransform.DOMove(new Vector3(0, 5.5f), 0.5f);

        yield return new WaitForSeconds(1f);

        float speed = _rotateSpeed[_index];
        float moveAngle = 0f;
        float angle = 0;
        int dir = 1;

        while(true)
        {
            for (int i = 0; i <= 360; i += 90)
            {
                Vector2 shootDir = new Vector2(Mathf.Cos((angle+i) * Mathf.Deg2Rad), Mathf.Sin((angle+i) * Mathf.Deg2Rad));
                Bullet bullet = Instantiate(_bullet, _shotPos.position, Quaternion.identity);
                bullet.BulletRotate(angle + i, shootDir);
            }

            angle += speed * 0.1f * dir;
            moveAngle += speed * 0.1f;

            if (moveAngle >= 180)
            {
                moveAngle -= 180;
                dir *= -1;
                speed += 3;
                yield return new WaitForSeconds(5f);
                speed -= 3;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void AIStop()
    {
        _stop = true;
        StopAllCoroutines();
    }
}
