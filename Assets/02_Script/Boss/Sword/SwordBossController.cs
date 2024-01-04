using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

    private bool onDash = false;
    private bool prevHit = false;

    List<GameObject> portals = new List<GameObject>();

    private void Awake()
    {
        warningImg = warningObj.GetComponent<WarningImg>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        particle.Stop();
        Invoke("StartBoss", 4f);

    }

    private void StartBoss()
    {

        //StartCoroutine(PortalCombo());
        StartCoroutine(Pattern());

    }

    private IEnumerator Pattern()
    {

        particle.Play();
        while (true)
        {

            int next = Random.Range(0, 4);

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

                case 3:
                    yield return StartCoroutine(PortalCombo());
                    break;
            }

            SetVisible(false);
            yield return new WaitForSeconds(0.8f);

            Vector3 pos = new Vector3(Random.Range(-15f, 15f), Random.Range(-4f, 6f));

            warningImg.gameObject.transform.position = pos;
            warningImg.ResetLifeTime();
            warningImg.transform.localScale = Vector2.zero;
            warningImg.transform.DOScale(Vector2.one, 0.8f);

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
        onDash = true;
        SoundManager.Instance.SFXPlay("Move", move);
        rigid.AddForce(transform.up * 40, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);
        onDash = false;
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
            onDash = true;
            transform.DOMove(pos, delay).SetEase(Ease.Linear);
            SoundManager.Instance.SFXPlay("Move", move);

            yield return new WaitForSeconds(delay);
            onDash = false;
            particle.Stop();
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

        warningImg.gameObject.transform.position = transform.position;
        warningImg.ResetLifeTime();
        warningImg.transform.localScale = Vector2.zero;
        warningImg.transform.DOScale(Vector2.one, 0.8f).SetEase(Ease.OutElastic);

        yield return new WaitForSeconds(0.8f);

        float originAngle = transform.rotation.eulerAngles.z;
        particle.Play();

        SoundManager.Instance.SFXPlay("Rot", rot);
        onDash = true;
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

        onDash = false;
        particle.Stop();

    }

    private IEnumerator RotateAttackCombo()
    {


        float originAngle = transform.rotation.eulerAngles.z;
        particle.Play();

        SoundManager.Instance.SFXPlay("Rot", rot);
        onDash = true;
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

        onDash = false;
        particle.Stop();

    }

    private IEnumerator PortalCombo()
    {

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = center.position + new Vector3(Random.Range(-15f, 15f), Random.Range(-4f, 6f));
            GameObject target = Instantiate(portal, pos, Quaternion.identity);
            var renderer = target.GetComponent<SpriteRenderer>();
            renderer.color = new Color(1, 1, 1, 0);
            renderer.DOFade(1, 0.5f);
            portals.Add(target);
            yield return new WaitForSeconds(0.1f);
        }

        #region 경고1
        Vector3 targetPos1 = portals[0].transform.position;
        Vector3 dir1 = targetPos1 - transform.position;

        GameObject obj1 = Instantiate(warningLine, transform.position, Quaternion.identity);
        obj1.transform.up = dir1;

        WarningLine line1 = obj1.GetComponent<WarningLine>();
        line1.speed = 0;
        float delay1 = Vector2.Distance(transform.position, targetPos1) * 0.02f;
        obj1.transform.DOMove(targetPos1, delay1);

        yield return new WaitForSeconds(delay1);
        #endregion

        #region 경고2
        Vector3 targetPos2 = portals[1].transform.position;
        Vector3 dir2 = targetPos2 - transform.position;

        GameObject obj2 = Instantiate(warningLine, targetPos1, Quaternion.identity);
        obj2.transform.up = dir2;

        WarningLine line2 = obj2.GetComponent<WarningLine>();
        line2.speed = 0;
        float delay2 = Vector2.Distance(transform.position, targetPos2) * 0.02f;
        obj2.transform.DOMove(targetPos2, delay2);
        yield return new WaitForSeconds(delay2);

        #endregion

        #region 경고3
        Vector3 targetPos3 = portals[2].transform.position;
        Vector3 dir3 = targetPos3 - transform.position;

        GameObject obj3 = Instantiate(warningLine, targetPos2, Quaternion.identity);
        obj3.transform.up = dir3;

        WarningLine line3 = obj3.GetComponent<WarningLine>();
        line3.speed = 0;
        float delay3 = Vector2.Distance(transform.position, targetPos3) * 0.02f;
        obj3.transform.DOMove(targetPos3, delay3);
        yield return new WaitForSeconds(delay3);
        #endregion

        yield return new WaitForSeconds(0.2f);
        Destroy(obj3);
        Destroy(obj2);
        Destroy(obj1);

        particle.Play();

        onDash = true;
        for (int i = 0; i < 3; i++)
        {
            transform.up = portals[i].transform.position - transform.position;
            float delay = Vector2.Distance(transform.position, portals[i].transform.position) * 0.03f;

            transform.DOMove(portals[i].transform.position, delay).SetEase(Ease.Linear);
            SoundManager.Instance.SFXPlay("Move", move);

            yield return new WaitForSeconds(delay);

            StartCoroutine(RotateAttackCombo());

            yield return new WaitForSeconds(0.5f);
            SoundManager.Instance.SFXPlay("KKANG", KKANG);
            for (int j = 0; j < 10; j++)
            {

                Shoot(36 * j, transform.position);

            }
            Destroy(portals[i]);
        }
        onDash = false;
        particle.Stop();
        portals.Clear();

        yield return new WaitForSeconds(0.5f);
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


    private void OnDestroy()
    {
        foreach (var item in portals)
        {
            Destroy(item);
        }
        portals.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onDash)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.TryGetComponent<HitObject>(out HitObject ho))
                {
                    ho.TakeDamage(10);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onDash)
        {
            if (prevHit == false)
            {
                if (collision.CompareTag("Player"))
                {
                    if (collision.TryGetComponent<HitObject>(out HitObject ho))
                    {
                        ho.TakeDamage(10);
                        prevHit = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prevHit = false;
    }
}
