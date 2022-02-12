using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontShowCursor : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }
}
