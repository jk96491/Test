using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserBase {
	private string nickName = string.Empty;
    private string tokenID = string.Empty;
    private int serverID;
    private int level = 0;
    private int exp = 0;
    
    private int gold = 0;
    private int gem = 0;

    public int Gold { get { return this.gold; } }
    public int Gem { get { return this.gem; } }

    public string NickName { get { return this.nickName; } }
    public int Level { get { return this.level; } }
    public int Exp { get { return this.exp; } }

    Action<GameProtocol.RS_CreateNickName, string> CreateNickNameCallBack;

    public void ResetFromServer(GameProtocol.UserInfo userInfo_)
    {
        nickName = userInfo_.nickName;
        level = userInfo_.level;
        exp = userInfo_.exp;
        serverID = userInfo_.serverID;
        gold = userInfo_.gold;
        gem = userInfo_.gem;
    }

    public void RequestCreateNickName(string nickName_, Action<GameProtocol.RS_CreateNickName, string> CreateNickNameCallBack_ = null)
    {
        CreateNickNameCallBack = CreateNickNameCallBack_;
        NppCreateNickName.RequestCreateNickName(serverID, nickName_, HandleOnSuccessCreateNickName, HandleOnFailCreateNickName);
    }

    public void HandleOnSuccessCreateNickName(GameProtocol.RS_CreateNickName protocol)
    {
        ReserFromServer(protocol);

        if( null != CreateNickNameCallBack)
        {
            CreateNickNameCallBack(protocol,"");
            CreateNickNameCallBack = null;
        }
    }

    public void HandleOnFailCreateNickName(string error_)
    {
        if (null != CreateNickNameCallBack)
        {
            CreateNickNameCallBack(null, error_);
            CreateNickNameCallBack = null;
        }
    }

    private void ReserFromServer(GameProtocol.RS_CreateNickName protocol)
    {
        nickName = protocol.nickName;
    }
}
