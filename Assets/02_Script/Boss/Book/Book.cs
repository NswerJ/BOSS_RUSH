using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Transform _root;

    [SerializeField] private GameObject _mainObject;
    [SerializeField] private GameObject _backObject;

    [SerializeField] private AudioClip _flipClip;

    [Header("Hit")]
    [SerializeField] private HitObject _mainHit;
    [SerializeField] private BackHit _backHit;
    public BackHit Back => _backHit;

    private bool _isBack = false;
    private bool _isPlayAnim = false;

    public void ResetChildPos()
    {
        _mainObject.transform.localPosition = Vector3.zero;
    }

    public void Flip(bool isBack)
    {
        if (_isPlayAnim) return;
        _isPlayAnim = true;

        Sequence seq = DOTween.Sequence();

        seq.Append(_root.DORotate(new Vector3(90, 0, 0), 0.1f, RotateMode.Fast))
            .SetEase(Ease.InBack)
            .Append(_root.DORotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast))
            .SetEase(Ease.OutBounce)
            .OnStart(() =>
            {
                _mainObject.SetActive(!isBack);
                _backObject.SetActive(isBack);
            })
            .OnComplete(() =>
            {
                _isPlayAnim = false;
                _isBack = isBack;
            });

        SoundManager.Instance.SFXPlay("Flip", _flipClip);
    }

    public void FlipBack()
    {
        Flip(true);
    }

    public void Fold()
    {
        GameObject currentActiveObj = _isBack ? _backObject : _mainObject;
        Transform activeTrm = currentActiveObj.transform;

        currentActiveObj.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(activeTrm.DOScale(new Vector3(0, 0, 0), 0.6f))
            .Join(activeTrm.DORotate(new Vector3(0, 0, -360 * 2), 0.6f, RotateMode.FastBeyond360))
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                activeTrm.localScale = Vector3.one;
                activeTrm.eulerAngles = Vector3.zero;

                currentActiveObj.SetActive(false);
            });
    }

    public void Open()
    {
        _mainObject.SetActive(false);
        _backObject.SetActive(true);

        Flip(false);
    }

    public void DamageOff()
    {
        _mainHit.SetActivate(false);
        _backHit.SetActivate(false);
    }

    public void DamageOn()
    {
        _mainHit.SetActivate(true);
        _backHit.SetActivate(true);
    }

    public void BackHitOff()
    {
        _backHit.SetActivate(false);
    }
}
