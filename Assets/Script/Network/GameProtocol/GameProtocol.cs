using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProtocol : MonoBehaviour
{
    public class Rq_Login
    {
        public string ID = string.Empty;
    }
    public class Rq_CreateNickName
    {
        public int ServerID = -1;
        public string nickName = string.Empty;
    }
    public class Rq_SetUserParty
    {
        public int userID;
        public int[] characterIDs;
    }

    public class Rs_Login
    {
        public CharacterInfo[] characterInfos;
        public UserInfo userInfo;
        public PartyInfo partyInfo;
    }

    public class RS_CreateNickName
    {
        public string nickName = string.Empty;
    }

    public class Rs_SetUserParty
    {
        public string Result = string.Empty;
        public PartyInfo partyInfo;
    }

    public class CharacterInfo
    {
        public int serverID = GameData.INVALID_ID;
        public int id = GameData.INVALID_ID;
        public int exp = 0;
        public int level = 0;
        public float fatigue = 0f;
        public SkillInfo[] equipSkillInfos;
    }

    public class SkillInfo
    {
        public int skillID;
        public int level;
    }


    public class UserInfo
    {
        public string nickName = string.Empty;
        public int exp = 0;
        public int level = 0;
        public int serverID = 0;
        public UserMoney userMoney;
    }

    public class UserMoney
    {
        public int gold = 0;
        public int gem = 0;
    }


    public class PartyInfo
    {
        public int[] partyArray;
    }
}
