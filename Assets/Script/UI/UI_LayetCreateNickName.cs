using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayetCreateNickName : UI_LayerBase {

    [SerializeField]
    private UIInput input = null;
    [SerializeField]
    private UIButton button = null;

    protected override void Initialize()
    {
        if(null != button)
        {
            button.onClick.Add(new EventDelegate(HandleOnClickButton));
        }
    }

    private void HandleOnClickButton()
    {
        if(null != input)
        {
            if(false == string.IsNullOrEmpty(input.value))
            {
                UserManager.Instance.localUser.RequestCreateNickName(input.value, CreateNickNameCallBack);
            }
        }
    }

    private void CreateNickNameCallBack(GameProtocol.RS_CreateNickName userInfo_ ,string error_)
    {
        if(null != userInfo_)
        {
            AllSceneManager.Instance.ChangeScene(AllSceneManager.SceneType.LOBBY);
        }
    }

}
