using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;

    private void Start()
    {
        Cursor.SetCursor(cursorImage, new Vector2(16, 16), CursorMode.Auto);
    }
}
