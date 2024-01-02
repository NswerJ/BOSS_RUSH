using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderSencer : MonoBehaviour
{

    [SerializeField] private UnityEvent triggerdEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        triggerdEvent?.Invoke();
        gameObject.SetActive(false);

    }

}
