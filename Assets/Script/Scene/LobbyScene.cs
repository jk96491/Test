using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AllSceneManager.Instance.Initialized(AllSceneManager.SceneType.LOBBY);
        UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_LOBBY);
    }
	
}
