using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIceSpearSkill : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private Image _SpearImage;
    [SerializeField] private TextMeshProUGUI _coolText;

    private PlayerEventSystem evSys;

    public bool skillEnable;
    public GameObject iceSpear;
    public Transform Player;
    public bool IceSpearCharge = false;
    public float playerSpearSpeed = 40f;
    public float playerSpearCool = 5f;
    private bool isCoolingDown = false;


    void Start()
    {

        Image cooldownImage = Image.FindAnyObjectByType<Image>();
        playerSpearCool = 5f;
        IceSpearCharge = false;
        evSys = FindObjectOfType<PlayerController>().playerEventSystem;
        if (DataManager.Instance != null)
            skillEnable = PlayerPrefs.GetInt("File" +
            DataManager.Instance.DataIndex + "Boss" + 3) == 1;
        else if (FindObjectOfType<Data>() != null)
            skillEnable = PlayerPrefs.GetInt("File" +
            FindObjectOfType<Data>().name + "Boss" + 3) == 1;

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
        if (IceSpearCharge)
        {
            StartCooldown();
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
        }
    }

    void Update()
    {
        playerSpearCool += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && playerSpearCool >= 5f)
        {
            IceSpearCharge = true;
        }

        if (isCoolingDown)
        {
            Debug.Log("sd");
            float fillValue = Mathf.Clamp01(1 - (playerSpearCool / 5f));

            if(_cooldownImage == null) return;
            _cooldownImage.fillAmount = fillValue;
            string str = (fillValue * 5).ToString();
            
            if (str.Length > 2)
                _coolText.text = str.Substring(0, 2);
            if (playerSpearCool >= 5f)
            {
                isCoolingDown = false;
                _cooldownImage.fillAmount = 0f;
                _coolText.text = string.Empty;
            }

        }

    }

    private void StartCooldown()
    {
        playerSpearCool = 0;
        isCoolingDown = true;
    }
}
