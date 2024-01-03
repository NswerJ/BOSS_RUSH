using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SwordBossController : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] SpriteRenderer visual;
    [SerializeField] ParticleSystem particle;

    [SerializeField] GameObject warningLine;
    [SerializeField] GameObject warningObj;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject bullet;

    [SerializeField] AudioClip move;
    [SerializeField] AudioClip KKANG;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip rot;


    private WarningImg warningImg;
    private GameObject player;
    private Rigidbody2D rigid;
    private Collider2D col;
    private Collider2D dealingCol;

    List<GameObject> portals = new List<GameObject>();

    private void Awake()
    {
        warningImg = warningObj.GetComponent<WarningImg>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        dealingCol = transform.GetChild(0).GetComponent<Collider2D>();
        dealingCol.enabled = false;
        player = GameObject.Find("Player");
    }

    private void Start()
    {

        StartCoroutine(Pattern());

    }

    private IEnumerator Pattern()
    {

        while (true)
        {

            int next = Random.Range(0, 3);

            switch (next)
            {

                case 0:

                    yield return StartCoroutine(MoveTo());
                    break;

                case 1:

                    yield return StartCoroutine(Portal());
                    break;

                case 2:

                    yield return StartCoroutine(RotateAttack());
                    break;

            }

            SetVisible(false);
            yield return new WaitForSeconds(0.8f);

            Vector3 pos = new Vector3(Random.Range(-15f, 15f), Random.Range(-4f, 6f));

            warningImg.gameObject.transform.position = pos;
            warningImg.ResetLifeTime();

            yield return new WaitForSeconds(0.8f);

            transform.rotation = Quaternion.identity;
            transform.position = pos;
            SetVisible(true);

            yield return new WaitForSeconds(3f);

        }

    }

    //�÷��̾� �������� �̵�
    private IEnumerator MoveTo()
    {

        LookAt();

        GameObject obj = Instantiate(warningLine, transform.position, Quaternion.identity);
        obj.transform.up = transform.up;

        yield return new WaitForSeconds(0.5f);

        Destroy(obj);

        SoundManager.Instance.SFXPlay("Move", move);
        rigid.AddForce(transform.up * 40, ForceMode2D.Impulse);
        dealingCol.enabled = true;

        yield return new WaitForSeconds(1f);
        dealingCol.enabled = false;
        rigid.velocity = Vector2.zero;

    }

    // �տ� ������ �տ��� �̵�
    private IEnumerator Portal()
    {

        for (int i = 0; i < 3; i++)
        {

            particle.Play();
            Vector3 pos = center.position + new Vector3(Random.Range(-15f, 15f), Random.Range(-4f, 6f));
            GameObject target = Instantiate(portal, pos, Quaternion.identity);
            var renderer = target.GetComponent<SpriteRenderer>();
            renderer.color = new Color(1, 1, 1, 0);
            renderer.DOFade(1, 0.5f);
            portals.Add(target);

            Vector3 dir = pos - transform.position;

            GameObject obj = Instantiate(warningLine, transform.position, Quaternion.identity);
            transform.up = dir;
            obj.transform.up = dir;

            var line = obj.GetComponent<WarningLine>();
            line.speed = 0;
            obj.transform.DOMove(pos, 0.6f);

            float delay = Vector2.Distance(transform.position, pos) * 0.03f;

            yield return new WaitForSeconds(0.6f);
            Destroy(obj);
            dealingCol.enabled = true;
            transform.DOMove(pos, delay).SetEase(Ease.Linear);
            SoundManager.Instance.SFXPlay("Move", move);

            yield return new WaitForSeconds(delay);
            particle.Stop();
            dealingCol.enabled = false;
            SetVisible(false);


            yield return new WaitForSeconds(2f);

            Vector3 nextPos = new Vector3(Random.Range(-15f, 15f), Random.Range(-4f, 6f));
            warningImg.gameObject.transform.position = nextPos;
            warningImg.ResetLifeTime();

            yield return new WaitForSeconds(0.8f);

            transform.position = nextPos;
            transform.rotation = Quaternion.identity;

            SetVisible(true);
            yield return new WaitForSeconds(1f);

        }



        for (int i = 0; i < 3; i++)
        {
            SoundManager.Instance.SFXPlay("KKANG", KKANG);
            for (int j = 0; j < 10; j++)
            {

                Shoot(36 * j, portals[i].transform.position);

            }

        }

        Destroy(portals[0]);
        Destroy(portals[1]);
        Destroy(portals[2]);
        portals.Clear();

        yield return new WaitForSeconds(0.5f);

    }

    // ȸ��
    private IEnumerator RotateAttack()
    {

        //Debug.Log("rot");

        float originAngle = transform.rotation.eulerAngles.z;
        particle.Play();

        SoundManager.Instance.SFXPlay("Rot", rot);
        dealingCol.enabled = true;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(new Vector3(0, 0, originAngle + 90), 0.1f).SetEase(Ease.Linear)).
            Append(transform.DORotate(new Vector3(0, 0, originAngle + 180), 0.1f).SetEase(Ease.Linear)).
            Append(transform.DORotate(new Vector3(0, 0, originAngle + 270), 0.1f).SetEase(Ease.Linear)).
            Append(transform.DORotate(new Vector3(0, 0, originAngle), 0.1f).SetEase(Ease.Linear));


        for (int i = 0; i < 8; i++)
        {
            Shoot(originAngle + 45 * i, transform.position);
            yield return new WaitForSeconds(0.05f);
        }

        dealingCol.enabled = false;

        particle.Stop();

    }

    // �Ѿ� �߻�
    private void Shoot(float angle, Vector3 pos)
    {

        //Debug.Log(angle);
        GameObject obj = Instantiate(bullet, pos, Quaternion.identity);
        obj.transform.rotation = Quaternion.AngleAxis(angle, transform.forward);

    }

    // �÷��̾� �ٶ󺸱�
    private void LookAt()
    {

        Vector2 dir = player.transform.position - transform.position;
        transform.up = dir;

    }

    private void SetVisible(bool value)
    {

        if (value)
        {
            Debug.Log(1);
            visual.DOFade(1, 0.5f);

        }
        else
        {

            visual.DOFade(0, 0.5f);

        }

        col.enabled = value;

    }

    //private void ChangeIdle()
    //{

    //    Vector3 pos = new Vector3(Random.Range(-15f, 15f), Random.Range(-4f, 6f));
    //    transform.position = pos;
    //    Debug.Log(warningImg);
    //    warningImg.gameObject.transform.position = pos;
    //    warningImg.ResetLifeTime();
    //    transform.rotation = Quaternion.identity;
    //    SetVisible(true);

    //}


    private void OnDestroy()
    {
        foreach (var item in portals)
        {
            Destroy(item);
        }
        portals.Clear();
    }
}
