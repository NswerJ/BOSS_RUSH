using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleSpawner : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {

        StartCoroutine(SpawnCo());

    }

    private IEnumerator SpawnCo()
    {

        while (true)
        {

            FAED.TakePool<Icicle>("Icicle").Spawn(spawnPoint);
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));

        }

    }

}
