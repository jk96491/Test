using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalUser : UserBase
{
    Action<GameProtocol.UserInfo, string> LoginCallBack;

    private Dictionary<int/**/, UserCharacter> userCharacterDic = new Dictionary<int, UserCharacter>();

    public void RequestLogin(string ID_, Action<GameProtocol.UserInfo, string> LoginCallBack_ = null)
    {
        LoginCallBack = LoginCallBack_;
        NppLogin.RequestLogin(ID_, HandleOnSuccessLogin, HandleOnFailLogin);
    }

    private void HandleOnSuccessLogin(GameProtocol.Rs_Login Rs_login)
    {
        ResetFromServer(Rs_login.userInfo);
        ResetFromServer(Rs_login.characterInfos);

        if (null != LoginCallBack)
            LoginCallBack(Rs_login.userInfo, "");
    }

    private void HandleOnFailLogin(string error_)
    {
        if (null != LoginCallBack)
            LoginCallBack(null, error_);
    }

    private void ResetFromServer(GameProtocol.CharacterInfo[] characterInfos_)
    {
        GameProtocol.CharacterInfo[] characterInfos = characterInfos_;

        if (null != characterInfos)
        {
            for (int index = 0; index < characterInfos.Length; index++)
            {
                if (null != characterInfos[index])
                {
                    if (true == userCharacterDic.ContainsKey(characterInfos[index].id))
                    {
                        userCharacterDic[characterInfos[index].id].ResetFromServer(characterInfos[index]);
                    }
                    else
                    {
                        UserCharacter userCharacter = new UserCharacter();
                        userCharacter.ResetFromServer(characterInfos[index]);
                        userCharacterDic[characterInfos[index].id] = userCharacter;
                    }
                }
            }
        }
    }
}
