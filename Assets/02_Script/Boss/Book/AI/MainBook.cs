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

    [SerializeField] private BossDieCutScene _dieCutScene;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _backHitClip;
    [SerializeField]
    private AudioClip _hitClip;

    [SerializeField]
    private AudioClip _bulletClip;
    [SerializeField]
    private AudioClip _laserClip;
    //

    [Header("Attack Value")]
    [SerializeField]
    private Bullet _bullet;
    [SerializeField]
    private Bullet _sharpBullet;

    [SerializeField]
    private GameObject _danger;
    [SerializeField]
    private LineRenderer _dangerLine;
    [SerializeField]
    private LineRenderer _laserLine;

    [SerializeField]
    private Transform _vDG;
    [SerializeField]
    private Transform _hDG;

    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private List<float> _rotateSpeed = new List<float>(5); // 총 5개
    [SerializeField]
    private List<float> _waitTime = new List<float>(5);
    int _index = 0;

    bool _patternStart = false;
    Transform _player;

    [SerializeField]
    private LayerMask _playerLayer;

    private bool _isPattern;

    [Header("Move Value")]
    [SerializeField]
    private Transform _mainTransform;
    private bool _move = true;


    private void Start()
    {
        _player = GameObject.Find("Player").transform;

        Back.BackHitEvent += HandleHit;
        Back.HitEvent += HandleAnsHit;
        hit.DieEvent += HandleDie;
        hit.HitEvent += HandleHitSound;
    }

    private void HandleAnsHit()
    {
        SoundManager.Instance.SFXPlay("AnsHit", _backHitClip);
    }

    private void HandleHitSound()
    {
        SoundManager.Instance.SFXPlay("Hit", _hitClip);
    }

    private void HandleDie()
    {
        _dieCutScene.Play();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_stop) return;

        if (_patternStart || _isPattern == false)
        {
            StartCoroutine($"Pattern{Random.Range(1, 3)}");
            _patternStart = false;
            
        }

        
        if (_move)
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
        _move = true;
        _isPattern = true;

        yield return new WaitForSeconds(0.5f);
        _mainTransform.DOMove(new Vector3(0, 5.5f), 0.5f);

        yield return new WaitForSeconds(1f);

        float speed = _rotateSpeed[_index];
        float moveAngle = 0f;
        float angle = 0;
        int dir = 1;

        _danger.SetActive(true);
        Sequence seq = DOTween.Sequence();

        _vDG.localScale = new Vector3(0, 50, 1);
        _hDG.localScale = new Vector3(50, 0, 1);
        seq.Append(_vDG.DOScaleX(0.5f, 0.25f))
            .Join(_hDG.DOScaleY(0.5f, 0.25f));

        yield return new WaitForSeconds(0.26f);

        _danger.SetActive(false);

        int cnt = 0;
        while (true)
        {
            SoundManager.Instance.SFXPlay("BookBullet", _bulletClip);

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
                yield return new WaitForSeconds(_waitTime[_index]);
                speed -= 3;

                // 레이저
                ++cnt;
                if (cnt >= 2)
                {
                    _isPattern = false;
                    break;
                }

                Vector2 lasetShotDir = _player.position - _shotPos.position;
                float currentTime = 0f, endTime = 0.5f;
                _dangerLine.enabled = true;

                while(currentTime <= endTime)
                {
                    currentTime += Time.deltaTime;
                    float val = currentTime / endTime;
                    _dangerLine.startWidth = Mathf.Lerp(0f, 0.8f, val);
                    _dangerLine.endWidth = Mathf.Lerp(0f, 0.8f, val);

                    _dangerLine.SetPosition(0, _shotPos.position);
                    _dangerLine.SetPosition(1, _shotPos.position + (Vector3)lasetShotDir * 100f);

                    yield return new WaitForEndOfFrame();
                }

                _dangerLine.enabled = false;

                currentTime = 0f;
                _laserLine.enabled = true;
                float soundInterval = 0f;
                
                SoundManager.Instance.SFXPlay("Laser", _laserClip);
                while (currentTime <= endTime)
                {
                    currentTime += 0.01f;
                    soundInterval += 0.01f;
                    float val = currentTime / endTime;
                    _laserLine.startWidth = Mathf.Lerp(1f, 0f, val);
                    _laserLine.endWidth = Mathf.Lerp(1f, 0f, val);

                    if(soundInterval <= endTime / 5f)
                    {
                        SoundManager.Instance.SFXPlay("Laser", _laserClip);
                        soundInterval = 0f;
                    }

                    RaycastHit2D hit = Physics2D.Linecast(_shotPos.position, _shotPos.position + (Vector3)lasetShotDir * 100f, _playerLayer);
                    if(hit.collider)
                    {
                        if(hit.transform.TryGetComponent<HitObject>(out HitObject HitObj))
                        {
                            HitObj.TakeDamage(1f);
                        }
                    }

                    _laserLine.SetPosition(0, _shotPos.position);
                    _laserLine.SetPosition(1, _shotPos.position + (Vector3)lasetShotDir * 100f);

                    yield return new WaitForSeconds(0.01f);
                }

                _laserLine.enabled = false;


                _danger.SetActive(true);
                seq.Kill();
                seq = DOTween.Sequence();

                _vDG.localScale = new Vector3(0, 50, 1);
                _hDG.localScale = new Vector3(50, 0, 1);
                seq.Append(_vDG.DOScaleX(0.5f, 0.25f))
                    .Join(_hDG.DOScaleY(0.5f, 0.25f));

                yield return new WaitForSeconds(0.26f);

                _danger.SetActive(false);
                
            }

            yield return new WaitForSeconds(0.1f);
            

        }
    }

    IEnumerator Pattern2()
    {
        _move = false;
        _isPattern = true;

        yield return new WaitForSeconds(0.5f);
        _mainTransform.DOMove(new Vector3(0, 5.5f), 0.5f);

        yield return new WaitForSeconds(1f);

        float speed = _rotateSpeed[_index];
        float moveAngle = 0f;
        float angle = 0;
        int dir = 1;
        if (Random.Range(0, 2) < 1)
            dir = -1;

        _danger.SetActive(true);
        Sequence seq = DOTween.Sequence();

        _vDG.localScale = new Vector3(0, 50, 1);
        _hDG.localScale = new Vector3(50, 0, 1);
        seq.Append(_vDG.DOScaleX(0.5f, 0.25f))
            .Join(_hDG.DOScaleY(0.5f, 0.25f));

        yield return new WaitForSeconds(0.26f);

        _danger.SetActive(false);

        while (true)
        {
            SoundManager.Instance.SFXPlay("BookBullet", _bulletClip);

            for (int i = 0; i <= 360; i += 90)
            {
                Vector2 shootDir = new Vector2(Mathf.Cos((angle + i) * Mathf.Deg2Rad), Mathf.Sin((angle + i) * Mathf.Deg2Rad));
                Bullet bullet = Instantiate(_sharpBullet, _shotPos.position, Quaternion.identity);
                bullet.BulletRotate(angle + i, shootDir);

                Destroy(bullet, 2f);
            }

            angle += speed * 0.05f * dir;
            moveAngle += speed * 0.05f;

            if (moveAngle >= 90)
            {
                yield return new WaitForSeconds(_waitTime[_index]);
                _isPattern = false;
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void AIStop()
    {
        _stop = true;
        _dangerLine.enabled = false;
        _laserLine.enabled = false;
        _danger.SetActive(false);
        StopAllCoroutines();
    }
}
