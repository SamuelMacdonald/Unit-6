using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public float scrollerX = 0.5f;
    public float scrollerY = 0.5f;
    void Update()
    {
        float OffsetX = Time.time * scrollerX;
        float OffsetY = Time.time * scrollerY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);    
    }
}
