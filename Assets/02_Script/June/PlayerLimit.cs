using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimit : MonoBehaviour
{
    
    void Update()
    {
        transform.position = new Vector2
            (Mathf.Clamp(transform.position.x,-30.5f,29.3f), transform.position.y);
    }
}
