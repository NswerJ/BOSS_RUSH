using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingAnimeParticle : MonoBehaviour
{
    
    public void AnimeEnd()
    {

        FAED.InsertPool(gameObject);

    }

}
