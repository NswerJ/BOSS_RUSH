using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceSpearSkill : MonoBehaviour
{

    private PlayerEventSystem evSys;

    public bool skillEnable;
    public GameObject iceSpear;
    public Transform Player;
    public bool IceSpearCharge = false;
    public float playerSpearSpeed = 40f;
    public float playerSpearCool = 5f;


    void Start()
    {
        playerSpearCool = 5f;
        IceSpearCharge = false;
        evSys = FindObjectOfType<PlayerController>().playerEventSystem;
        if (DataManager.Instance != null)
            skillEnable = PlayerPrefs.GetInt("File" +
            DataManager.Instance.DataIndex + "Boss" + 3) == 1;

        if (skillEnable)
        {
            ConnectEvent();
        }

    }

    public void ConnectEvent()
    {
        evSys.AttackEvent += IceSpearAttack;
    }

    private void IceSpearAttack(float obj)
    {
        Debug.Log("sd");
        if (IceSpearCharge)
        {
            GameObject SpearCharge = Instantiate(iceSpear, Player.position, Quaternion.identity);
            Rigidbody2D SpearChargeRb = SpearCharge.GetComponent<Rigidbody2D>();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x - Player.position.x >= 0)
            {
                SpearCharge.transform.right = Vector2.right;
            }
            else
            {
                SpearCharge.transform.right = Vector2.left;
            }

            SpearChargeRb.velocity = SpearCharge.transform.right * playerSpearSpeed;
            IceSpearCharge = false;
            playerSpearCool = 0;
        }
    }

    void Update()
    {
        playerSpearCool += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && playerSpearCool >= 5f)
        {
            IceSpearCharge = true;
        }
    }
}
