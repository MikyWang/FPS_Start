using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamera : MonoBehaviour
{
    private void Start()
    {
        float radio = (float)Screen.width / (float)Screen.height;
        GetComponent<Camera>().rect = new Rect(0.8f, (1 - 0.2f * radio), 0.2f, 0.2f * radio);
    }
}
