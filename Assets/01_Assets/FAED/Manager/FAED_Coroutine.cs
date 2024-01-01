using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAED_Coroutine : MonoBehaviour
{
    
    private HashSet<IEnumerator> defaultCoroutineContainer = new HashSet<IEnumerator>();

    private void Update()
    {
        
        for(IEnumerator i = defaultCoroutineContainer.GetEnumerator(); i.MoveNext();)
        {



        }

    }

}
