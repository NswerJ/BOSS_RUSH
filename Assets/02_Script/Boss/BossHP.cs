using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private Slider _hpBar;

    [SerializeField]
    private HitObject _mainBoss;

    private void Update()
    {
        _hpBar.value = _mainBoss.hp / _mainBoss.maxHP;
    }
}
