using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBossDie : BossDieCheck
{
    [SerializeField] AudioClip explosionClip;
    public override void DieEvent()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.FadeSound();
            SoundManager.Instance.PlayExplosion(explosionClip);
        }
        FindObjectOfType<BookPower>().SetAbility(true);
        Invoke("EndFun", 3f);
    }
}