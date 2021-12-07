using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScaleControl : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    public Camera mainCamera;

    private int oldWidth;
    private int oldHeight;

    void Start()
    {
        oldWidth = Screen.width;
        oldHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != oldWidth || Screen.height != oldHeight)
        {
            OnScreenResolutionChanged(Screen.width, Screen.height);
            oldWidth = Screen.width;
            oldHeight = Screen.height;
        }
    }

    private void OnScreenResolutionChanged(int width, int height)
    {
        //Debug.LogFormat("Screen resolution changed. old resolution ({0}, {1}), new resolution ({2}, {3}).",
        //    oldWidth, oldHeight,
        //    width, height);
        int referenceWidth = (int)canvasScaler.referenceResolution.x;
        int referenceHeight = (int)canvasScaler.referenceResolution.y;
        float referenceRatio = (float)referenceWidth / referenceHeight;
        float newRatio = (float)width / height;
        if (referenceWidth * height == width * referenceHeight)
        {
            mainCamera.rect = new Rect(0, 0, 1, 1);
        }
        else if (newRatio > referenceRatio)
        {
            float h = height;
            float w = h * referenceRatio;
            float x = (width - w) / 2;
            mainCamera.pixelRect = new Rect(x, 0, w, h);
        }
        else
        {
            float w = width;
            float h = w / referenceRatio;
            float y = (height - h) / 2;
            mainCamera.pixelRect = new Rect(0, y, w, h);
        }
    }
}
