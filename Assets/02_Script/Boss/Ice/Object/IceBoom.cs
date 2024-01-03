using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IceBoom : MonoBehaviour
{

    [SerializeField] private ParticleSystem movePtc;

    public void Spawn(Vector3 position)
    {

        movePtc.Play();

        transform.position = position + Vector3.up * 10;

        transform.DOMove(position, 0.7f).SetEase(Ease.OutQuad).OnComplete(() =>
        {

            StartCoroutine(Boom());

            movePtc.Stop();

        });

    }

    private IEnumerator Boom()
    {

        yield return new WaitForSeconds(0.3f);

        FAED.TakePool("BoomPTC", transform.position);

        int cnt = Random.Range(3, 8);

        var ang = 360f / cnt;

        for(int i = 0; i < cnt; i++)
        {

            var shard = FAED.TakePool<IceShard>("IceShard", transform.position, Quaternion.Euler(0, 0, i * ang));
            shard.ImmediatelySpawn(shard.transform.up);

        }

        Destroy(gameObject);

    }



}
