using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllSceneManager : Singleton<AllSceneManager>
{

    public void Initialized(SceneType type_)
    {
        if(currentScene == SceneType.NONE)
        {
            switch (type_)
            {
                case SceneType.LOGIN:
                    {
                        GameData.Instance.LoadTableData();
                        NetworkManager.Instance.IsNetWorkMode = true;

                        UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_LOGIN);
                    }
                    break;
                case SceneType.LOBBY:
                    {
                        GameData.Instance.LoadTableData();
                        NetworkManager.Instance.IsNetWorkMode = false;
                        UserManager.Instance.localUser.RequestLogin("Temp");
                        UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_LOBBY);
                    }
                    break;
                case SceneType.GAME:
                    {
                        GameData.Instance.LoadTableData();

                        if(NetworkManager.Instance.IsNetWorkMode != true)
                            NetworkManager.Instance.IsNetWorkMode = false;
                        UserManager.Instance.localUser.RequestLogin("Temp");
                        UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_GAME);
                    }
                    break;
            }
        }
        currentScene = type_;
    }

    public enum SceneType
    {
        NONE = -1,
        LOGIN,
        LOBBY,
        GAME,
    }

    private SceneType currentScene = SceneType.NONE;

    public SceneType CurrentScene { get { return this.currentScene; } }

    public void ChangeScene(SceneType type_)
    {
        if (type_ == currentScene)
            return;

        UIManager.Instance.CloseAllUI();
        currentScene = type_;
        SceneManager.LoadScene(SceneTypeConverter(type_));
    }

    private string SceneTypeConverter(SceneType type_)
    {
        string sceneName = string.Empty;

        switch (type_)
        {
            case SceneType.LOBBY: sceneName = "LobbyScene"; break;
            case SceneType.LOGIN: sceneName = "LoginScene"; break;
        }

        return sceneName;
    }
}
