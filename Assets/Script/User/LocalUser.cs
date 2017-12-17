using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalUser : UserBase
{
    Action<GameProtocol.UserInfo, string> LoginCallBack;

    private Dictionary<int/**/, UserCharacter> userCharacterDic = new Dictionary<int, UserCharacter>();
    private int[] userParty = new int[3];

    public void RequestLogin(string ID_, Action<GameProtocol.UserInfo, string> LoginCallBack_ = null)
    {
        LoginCallBack = LoginCallBack_;
        NppLogin.RequestLogin(ID_, HandleOnSuccessLogin, HandleOnFailLogin);
    }

    private void HandleOnSuccessLogin(GameProtocol.Rs_Login Rs_login)
    {
        ResetFromServer(Rs_login.userInfo);
        ResetFromServer(Rs_login.characterInfos);

        //파티 임시 세팅
        userParty[0] = -1;
        userParty[1] = -1;
        userParty[2] = -1;

        if (null != LoginCallBack)
            LoginCallBack(Rs_login.userInfo, "");
    }

    private void HandleOnFailLogin(string error_)
    {
        if (null != LoginCallBack)
            LoginCallBack(null, error_);
    }

    public int FindPartyCharacterByIndex(int index_)
    {
        return userParty[index_];
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

    public UserCharacter FindChracterByID(int id_)
    {
        return this.userCharacterDic[id_];
    }
}
