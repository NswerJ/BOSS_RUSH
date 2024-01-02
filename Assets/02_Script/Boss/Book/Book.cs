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

    private bool _isBack = false;
    private bool _isPlayAnim = false;

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
    }

    public void FlipBack()
    {
        Flip(true);
    }
}
