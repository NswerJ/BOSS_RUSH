using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBook : MonoBehaviour
{
    [Header("AI Value")]
    [SerializeField]
    private bool _stop = false;
    public HitObject hit; // 죽었을 때 이벤트 추가용
    public BackHit Back;


    private void Start()
    {
        Back.HitEvent += HandleHit;
    }

    private void Update()
    {
        if (_stop) return;
    }

    private void HandleHit(Book book)
    {
        _stop = false;
        
    }

    public void AIStop()
    {
        _stop = true;
    }
}
