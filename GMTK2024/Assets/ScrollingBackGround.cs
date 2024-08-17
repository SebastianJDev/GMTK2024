using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackGround : MonoBehaviour
{
    public RawImage rawImage;
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.1f;

    void Update()
    {
        Rect uvRect = rawImage.uvRect;
        uvRect.x += scrollSpeedX * Time.deltaTime;
        uvRect.y += scrollSpeedY * Time.deltaTime;
        rawImage.uvRect = uvRect;
    }
}
