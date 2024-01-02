using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStand : MonoBehaviour
{
    [SerializeField] WeaponDataSO data;
    WeaponController player;
    Animator animator;

    Transform text;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponentInChildren<WeaponController>();
        animator = GetComponent<Animator>();
        text = transform.GetChild(0);
    }

    private void Update()
    {
        bool check = Physics2D.OverlapCircle(transform.position, 1, 1 << 7);
        text.gameObject.SetActive(check);

        animator.SetBool("IsOpen", check);

        if (check == false) return;

        if (Input.GetKey(KeyCode.F))
        {
            var temp = player.Data;
            player.Data = data;
            data = temp;
        }
    }
}
