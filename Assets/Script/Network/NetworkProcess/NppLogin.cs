using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class NppLogin 
{
    static Action<GameProtocol.Rs_Login> sucessCallback;
    static Action<string> failCallback;

    private static string subURL = string.Empty;

    public static void RequestLogin(string ID_, Action<GameProtocol.Rs_Login> sucessCallback_, Action<string> failCallback_)
    {
        GameProtocol.Rq_Login protocol = new GameProtocol.Rq_Login();
        protocol.ID = ID_;

        sucessCallback = sucessCallback_;
        failCallback = failCallback_;

        subURL = string.Format("users/{0}", protocol.ID);

        if(true == NetworkManager.Instance.IsNetWorkMode)
        {
            NetworkManager.Instance.ConnectServer(Rs_Login, subURL);
        }
        else
        {
            Rs_Login_Dev();
        }
    }

    public static void Rs_Login(WWW Data)
    {
        if (true == string.IsNullOrEmpty(Data.error))
        {
            GameProtocol.Rs_Login rsLoginProtocol = new GameProtocol.Rs_Login();

            SimpleJSON.JSONNode root = SimpleJSON.JSON.Parse(Data.text);

            ParseUserInfo(root["user"][0], ref rsLoginProtocol.userInfo);
            ParseChracterInfo(root["characters"], ref rsLoginProtocol.characterInfos);

            if (null != sucessCallback)
                sucessCallback(rsLoginProtocol);
        }
        else
        {
            if (null != failCallback)
                failCallback(Data.error);
        }

        sucessCallback = null;
        failCallback = null;
    }

    public static void Rs_Login_Dev()
    {
        GameProtocol.Rs_Login rsLoginProtocol = new GameProtocol.Rs_Login();

        rsLoginProtocol.userInfo = new GameProtocol.UserInfo();
        rsLoginProtocol.characterInfos = new GameProtocol.CharacterInfo[3];

        rsLoginProtocol.userInfo.nickName = "Guest";
        rsLoginProtocol.userInfo.level = 1;
        rsLoginProtocol.userInfo.exp = 0;
        rsLoginProtocol.userInfo.gold = 999999;
        rsLoginProtocol.userInfo.gem = 999999;

        rsLoginProtocol.characterInfos[0] = new GameProtocol.CharacterInfo();
        rsLoginProtocol.characterInfos[0].id = 10101;
        rsLoginProtocol.characterInfos[0].level = 1;
        rsLoginProtocol.characterInfos[0].exp = 0;

        rsLoginProtocol.characterInfos[1] = new GameProtocol.CharacterInfo();
        rsLoginProtocol.characterInfos[1].id = 10201;
        rsLoginProtocol.characterInfos[1].level = 1;
        rsLoginProtocol.characterInfos[1].exp = 0;

        rsLoginProtocol.characterInfos[2] = new GameProtocol.CharacterInfo();
        rsLoginProtocol.characterInfos[2].id = 10301;
        rsLoginProtocol.characterInfos[2].level = 1;
        rsLoginProtocol.characterInfos[2].exp = 0;

        if (null != sucessCallback)
            sucessCallback(rsLoginProtocol);
    }

    private static void ParseUserInfo(SimpleJSON.JSONNode root_, ref GameProtocol.UserInfo userInfo)
    {
        userInfo = new GameProtocol.UserInfo();
        userInfo.serverID = root_["pid"];
        userInfo.level = root_["level"];
        userInfo.exp = root_["exp"];
        userInfo.nickName = root_["nickname"];
    }

    private static void ParseChracterInfo(SimpleJSON.JSONNode root_, ref GameProtocol.CharacterInfo[] characterInfo_)
    {
        characterInfo_ = new GameProtocol.CharacterInfo[root_.Count];

        for (int index = 0; index < characterInfo_.Length; index++)
        {
            characterInfo_[index] = new GameProtocol.CharacterInfo();
            characterInfo_[index].id = root_[index]["pid"];
            characterInfo_[index].level = root_[index]["level"];
            characterInfo_[index].exp = root_[index]["exp"];
        }
    }
}

