﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : MonoBehaviour
{
    private void Start()
    {
        AllSceneManager.Instance.Initialized(AllSceneManager.SceneType.LOGIN);
       
    }
}
