﻿// Copyright (c) Mira Labs, Inc., 2017. All rights reserved.
// 
// Downloading and/or using this MIRA SDK is under license from MIRA, 
// and subject to all terms and conditions of the Mira Software License,
// found here: www.mirareality.com/sdk-license/
// 
// By downloading this SDK, you agree to the Mira Software License.
//
// This SDK may only be used in connection with the development of
// applications that are exclusively created for, and exclusively available
// for use with, MIRA hardware devices. This SDK may only be commercialized
// in the U.S. and Canada, subject to the terms of the License.
// 
// The MIRA SDK includes software under license from The Apache Software Foundation.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInScene : MonoBehaviour
{
    public bool shouldFadeOut = false;
    public string sceneToLoadNext;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    [SerializeField]
    private Texture2D fadeTexture;

    private float fadeSpeed = 2f;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    [HideInInspector]
    public bool shouldBlackOut = false;

    private void OnGUI()
    {
        if (!shouldFadeOut)
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            Color thisAlpha = GUI.color;
            thisAlpha.a = alpha;
            GUI.color = thisAlpha;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
        else
        {
            alpha -= fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            Color thisAlpha = GUI.color;
            thisAlpha.a = alpha;
            GUI.color = thisAlpha;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }

    public void FadeOut()
    {
        StartCoroutine(StartLogOut(sceneToLoadNext));
    }

    private IEnumerator StartLogOut(string sceneToLoad)
    {
        shouldFadeOut = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
}