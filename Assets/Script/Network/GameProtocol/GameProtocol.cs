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

    public class Rs_Login
    {
        public CharacterInfo[] characterInfos;
        public UserInfo userInfo;
    }

    public class RS_CreateNickName
    {
        public string nickName = string.Empty;
    }

    public class CharacterInfo
    {
        public int id = GameData.INVALID_ID;
        public int exp = 0;
        public int level = 0;
    }

    public class UserInfo
    {
        public string nickName = string.Empty;
        public int exp = 0;
        public int level = 0;
        public int serverID = 0;
    }



}
