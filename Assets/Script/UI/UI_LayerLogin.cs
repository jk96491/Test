using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LayerLogin : UI_LayerBase
{
    [SerializeField]
    private UIInput userInput = null;
    [SerializeField]
    private UIButton loginButton = null;

    protected override void Initialize()
    {
        if(null != loginButton)
        {
            loginButton.onClick.Add(new EventDelegate( HandleOnClickLoginButton));
        }
    }

    private void HandleOnClickLoginButton()
    {
        if (false == string.IsNullOrEmpty(userInput.value))
        {
            UserManager.Instance.localUser.RequestLogin(userInput.value, LoginCallBack);
        }
    }

    private void LoginCallBack(GameProtocol.UserInfo userinfo_, string error_)
    {
        if(true == string.IsNullOrEmpty(error_))
        {
            if(null != userinfo_)
            {
                if(true == string.IsNullOrEmpty(userinfo_.nickName))
                {
                    UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_CREATE_NICKNAME);
                    UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_LOGIN);
                }
                else
                {
                    AllSceneManager.Instance.ChangeScene(AllSceneManager.SceneType.LOBBY);
                }
            }
        }
    }
}
