using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalUser : UserBase
{
    Action<GameProtocol.UserInfo, string> LoginCallBack;
    Action<int> SetUserPartyCallBack;

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
        ResetFromServer(Rs_login.partyInfo);

        if (null != LoginCallBack)
            LoginCallBack(Rs_login.userInfo, "");
    }

    private void HandleOnFailLogin(string error_)
    {
        if (null != LoginCallBack)
            LoginCallBack(null, error_);
    }
    
    public void RequestSetUserParty(int[] characterIDs, Action<int> SetUserPartyCallBack_)
    {
        SetUserPartyCallBack = SetUserPartyCallBack_;

        for(int i = 0; i < characterIDs.Length; i++)
        {
            int ID = characterIDs[i];

            if(null == FindChracterByID(ID))
            {
                return;
            }
        }

        NppSetUserParty.RequestSetUserParty(this.ServerID, characterIDs, HandleOnSuccessSetUserParty, HandleOnFailSetUserParty);
    }

    private void HandleOnSuccessSetUserParty(GameProtocol.Rs_SetUserParty Protocol_)
    {
        ResetFromServer(Protocol_.partyInfo);

        if (null != SetUserPartyCallBack)
            SetUserPartyCallBack(0);
    }

    private void HandleOnFailSetUserParty(string error_)
    {
        if (null != SetUserPartyCallBack)
            SetUserPartyCallBack(1);
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

    private void ResetFromServer(GameProtocol.PartyInfo partyInfo_)
    {
        int[] partyArra = partyInfo_.partyArray;

        if (null == userParty)
            userParty = new int[partyArra.Length];
        for(int i = 0; i < partyArra.Length; i++)
        {
            userParty[i] = partyArra[i];
        }
    }
    public UserCharacter FindChracterByID(int id_)
    {
        if (false == this.userCharacterDic.ContainsKey(id_))
            return null;

        return this.userCharacterDic[id_];
    }
}
