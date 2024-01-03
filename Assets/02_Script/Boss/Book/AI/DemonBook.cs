using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DemonBook : MonoBehaviour
{
    [Header("AI Value")]
    [SerializeField]
    private bool _stop = false;
    public HitObject hit;
    public BackHit Back;

    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip _backHitClip;
    [SerializeField]
    private AudioClip _hitClip;

    [SerializeField]
    private AudioClip _bulletClip;
    [SerializeField]
    private AudioClip _fireballClip;

    [Header("Attack Value")]
    [SerializeField]
    private Bullet _sharpBullet;
    [SerializeField]
    private Bullet _fireballPrefab;
    [SerializeField]
    private Transform _shotPos;

    private List<Bullet> _fireball = new List<Bullet>();

    [Header("Move Value")]
    [SerializeField]
    private Transform _mainBook;
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _minX;
    [SerializeField]
    private float _maxX;
    [SerializeField]
    private float _minY;
    [SerializeField]
    private float _maxY;

    [SerializeField]
    private BoxCollider2D _boxCollider;

    [SerializeField]
    private LineRenderer _dangerLine;

    private bool _randCheck = false;
    private Vector3 _randPosition;

    private Transform _player;
    private Transform _point;

    private float _timer = 0;
    private float _fireballDelayTime = 3f;
    private int _count = 0;
    private int _maxCount = 3;

    private int _rageCount = 0;

    [Header("Sprite")]
    [SerializeField]
    private SpriteRenderer _mainSprite;
    [SerializeField]
    private Material _rageMat;
    [SerializeField]
    private Material _superRageMat;

    void Start()
    {
        _minY += _boxCollider.size.y;
        _maxY -= _boxCollider.size.y;
        _minX += _boxCollider.size.x;
        _maxX -= _boxCollider.size.x;

        _player = GameObject.Find("Player").transform;
        _point = GameObject.Find("BookPoint").transform;

        hit.DieEvent += HandleDie;
        hit.HitEvent += HandleHitSound;
        Back.BackHitEvent += HandleHit;
    }

    private void HandleHitSound()
    {
        SoundManager.Instance.SFXPlay("Hit", _hitClip);
    }

    void Update()
    {
        if (_stop) return;

        _timer += Time.deltaTime; 
        if (_timer >= _fireballDelayTime)
        {
            if (_count < _maxCount)
                _count++;

            _timer -= _fireballDelayTime;
            ShotFireball();
            
            StartCoroutine(ShotBullet());
        }

        MoveBook();
    }

    private void MoveBook()
    {
        if (Vector3.Distance(_player.position, _mainBook.position) <= 3f)
        {
            RunAway();
        }
        else
        {
            RandomMove();
        }
    }

    private void HandleDie()
    {
        _stop = true;
        StopAllCoroutines();
        _dangerLine.enabled = false;

        if (_rageCount == 0)
        {
            hit.SetHP(hit.maxHP + 200);
            
            _mainSprite.material = _rageMat;
            _speed = 5f;
            _maxCount = 4;
            _count = 1;

            _rageCount++;
        }
        else if (_rageCount == 1)
        {
            hit.SetHP(hit.maxHP + 300);
            
            _mainSprite.material = _superRageMat;
            _speed = 8f;
            _maxCount = 5;
            _count = 2;
        }


        

        for (int i = 0; i < _fireball.Count; i++)
            Destroy(_fireball[i].gameObject);
        _fireball.Clear();
    }

    private void HandleHit(Book book)
    {
        _stop = false;
        _timer = 0;

        SoundManager.Instance.SFXPlay("failHit", _backHitClip);
    }

    private void RunAway()
    {
        Vector3 dir = (_point.position - _player.position).normalized;
        Vector3 movePos = _player.position + dir * 8f;

        _randCheck = false;

        if (Vector3.Distance(movePos, _mainBook.position) <= 0.2f)
            return;

        Vector3 moveDir = (movePos - _mainBook.position).normalized;
        _mainBook.position += moveDir * _speed * Time.deltaTime;
    }

    private void RandomMove()
    {
        if(_randCheck == false)
        {
            _randPosition = transform.position + 
                (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized) * Random.Range(1f, 2f);

            if (_randPosition.x < _minX || _randPosition.x > _maxX
                || _randPosition.y < _minY || _randPosition.y > _maxY)
            {
                Vector3 dir = (_point.position - transform.position).normalized;
                _randPosition = transform.position + dir * Random.Range(0.5f, 1f);
            }

            _randCheck = true;
        }

        Vector3 moveDir = (_randPosition - transform.position).normalized;
        transform.position += moveDir * _speed * Time.deltaTime;

        if (Vector3.Distance(_randPosition, transform.position) <= 0.2f)
        {
            _randCheck = false;
        }
    }

    private void ShotFireball()
    {
        for(int i = 0; i < _count; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(-9f, 9f), 16, 0);

            Vector3 dir = Vector3.down;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Bullet fire = Instantiate(_fireballPrefab, pos, Quaternion.identity);
            fire.BulletRotate(angle, dir);

            _fireball.Add(fire);
        }

        SoundManager.Instance.SFXPlay("Fireball", _fireballClip);
    }

    IEnumerator ShotBullet()
    {
        _dangerLine.enabled = true;
        Vector3 dir = (_player.position - _mainBook.position).normalized;

        float currentTime = 0;
        float endTime = 0.5f;

        while(currentTime <= endTime)
        {
            currentTime += Time.deltaTime;
            _dangerLine.SetPosition(0, _mainBook.position);
            _dangerLine.SetPosition(1, _mainBook.position + dir * 25f);

            _dangerLine.startWidth = 0.1f;
            _dangerLine.endWidth = Mathf.Lerp(0.1f, 3f, currentTime / endTime);

            yield return new WaitForEndOfFrame();
        }
        
        _dangerLine.enabled = false;
        for(int i = 0; i < _count * 3; ++i)
        {
            SoundManager.Instance.SFXPlay("BookBullet", _bulletClip);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Bullet bullet = Instantiate(_sharpBullet, _shotPos.position, Quaternion.identity);
            bullet.BulletRotate(angle, dir);

            Destroy(bullet, 5f);

            yield return new WaitForSeconds(0.1f);
        }

    }
}
