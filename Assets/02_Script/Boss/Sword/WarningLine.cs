using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    public float speed;
    [SerializeField] float lifeTime;

    TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            trailRenderer.enabled = false;
            Destroy(gameObject); 
        }
    }
}
