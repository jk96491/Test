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
            NetworkManager.Instance.ConnectServerByGet(Rs_Login, subURL);
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

            ParseUserInfo(root[0], ref rsLoginProtocol.userInfo);
            ParseUserPartyInfo(root[0], ref rsLoginProtocol.partyInfo);
            ParseChracterInfo(root["Characters"], ref rsLoginProtocol.characterInfos);

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

        rsLoginProtocol.userInfo.userMoney = new GameProtocol.UserMoney();

        rsLoginProtocol.userInfo.userMoney.gold = 999999;
        rsLoginProtocol.userInfo.userMoney.gem = 999999;

        for(int i = 0; i < rsLoginProtocol.characterInfos.Length; i++)
        {
            rsLoginProtocol.characterInfos[i] = new GameProtocol.CharacterInfo();
            rsLoginProtocol.characterInfos[i].id = 10101 + (i + 0) * 100;
            rsLoginProtocol.characterInfos[i].level = 1;
            rsLoginProtocol.characterInfos[i].exp = 0;
            rsLoginProtocol.characterInfos[i].fatigue = UnityEngine.Random.Range(25f, 85f);

            rsLoginProtocol.characterInfos[i].equipSkillInfos = new GameProtocol.SkillInfo[3];

            GameProtocol.SkillInfo[] equipedSkillInfo = rsLoginProtocol.characterInfos[i].equipSkillInfos;

            for (int j = 0; j < equipedSkillInfo.Length; j++)
            {
                equipedSkillInfo[j] = new GameProtocol.SkillInfo();
                equipedSkillInfo[j].skillID = 101010101 + (i + 0) * 1000000 + j;
            }
        }

        rsLoginProtocol.partyInfo = new GameProtocol.PartyInfo();
        rsLoginProtocol.partyInfo.partyArray = new int[3];
        rsLoginProtocol.partyInfo.partyArray[0] = 10101;
        rsLoginProtocol.partyInfo.partyArray[1] = 10201;
        rsLoginProtocol.partyInfo.partyArray[2] = 10301; 

        if (null != sucessCallback)
            sucessCallback(rsLoginProtocol);
    }

    private static void ParseUserInfo(SimpleJSON.JSONNode root_, ref GameProtocol.UserInfo userInfo)
    {
        userInfo = new GameProtocol.UserInfo();
        userInfo.serverID = root_["user_pk"];
        userInfo.level = root_["Level"];
        userInfo.exp = root_["Exp"];
        userInfo.nickName = root_["Nickname"];

        userInfo.userMoney = new GameProtocol.UserMoney();
        userInfo.userMoney.gold = root_["Gold"];
        userInfo.userMoney.gem = root_["Gem"];
    }

    private static void ParseUserPartyInfo(SimpleJSON.JSONNode root_, ref GameProtocol.PartyInfo partyInfo)
    {
        partyInfo = new GameProtocol.PartyInfo();

        partyInfo.partyArray = new int[root_["UserParty"].Count];
        
        for(int i = 0; i < partyInfo.partyArray.Length; i++)
        {
            partyInfo.partyArray[i] = 10000 + 100 * root_["UserParty"][i] + 1;
        }

    }

    private static void ParseChracterInfo(SimpleJSON.JSONNode root_, ref GameProtocol.CharacterInfo[] characterInfo_)
    {
        characterInfo_ = new GameProtocol.CharacterInfo[root_.Count];

        for (int index = 0; index < characterInfo_.Length; index++)
        {
            characterInfo_[index] = new GameProtocol.CharacterInfo();
            characterInfo_[index].serverID = root_[index]["char_pk"];
            int JobCode = int.Parse(root_[index]["Job"]);
            int equipedSkin = int.Parse(root_[index]["EquippedSkin"]);

            int charID = 10000 + JobCode * 100 + equipedSkin;

            characterInfo_[index].id = charID;

            characterInfo_[index].level = root_[index]["Level"];
            characterInfo_[index].exp = root_[index]["Exp"];
            characterInfo_[index].fatigue = root_[index]["Fatigue"];

            SimpleJSON.JSONNode skillRoot = root_[index]["Skills"];

            characterInfo_[index].equipSkillInfos = new GameProtocol.SkillInfo[skillRoot.Count];

            for(int skillIndex = 0; skillIndex < characterInfo_[index].equipSkillInfos.Length; skillIndex++)
            {
                characterInfo_[index].equipSkillInfos[skillIndex] = new GameProtocol.SkillInfo();
                characterInfo_[index].equipSkillInfos[skillIndex].skillID = skillRoot[skillIndex]["skill_pk"];
                characterInfo_[index].equipSkillInfos[skillIndex].level = skillRoot[skillIndex]["level"];
            }
        }
    }
}

