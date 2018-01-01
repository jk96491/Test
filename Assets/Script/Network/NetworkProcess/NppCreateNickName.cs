using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class NppCreateNickName
{
    static Action<GameProtocol.RS_CreateNickName> sucessCallback;
    static Action<string> failCallback;

    private static string subURL = string.Empty;
    private static int ServerID = -1;
    private static string nickName = string.Empty;

    public static void RequestCreateNickName(int ID_, string nickName_ ,Action<GameProtocol.RS_CreateNickName> sucessCallback_, Action<string> failCallback_)
    {
        NppCreateNickName.ServerID = ID_;
        NppCreateNickName.nickName = nickName_;

        GameProtocol.Rq_CreateNickName protocol = new GameProtocol.Rq_CreateNickName();
        protocol.ServerID = ID_;
        protocol.nickName = nickName_;

        sucessCallback = sucessCallback_;
        failCallback = failCallback_;

        subURL = string.Format("users_nickname/?pid={0}&nick={1}", protocol.ServerID, protocol.nickName);

        if (true == NetworkManager.Instance.IsNetWorkMode)
        {
            NetworkManager.Instance.ConnectServer(Rs_CreateNickName, subURL);
        }
        else
        {
            Rs_CreateNickNameDev();
        }
    }

    private static void Rs_CreateNickName(WWW Data)
    {
        if (true == string.IsNullOrEmpty(Data.error))
        {
            GameProtocol.RS_CreateNickName Protocol = new GameProtocol.RS_CreateNickName();

            Protocol.nickName = NppCreateNickName.nickName;

            if (null != sucessCallback)
                sucessCallback(Protocol);
        }
        else
        {
            if (null != failCallback)
                failCallback(Data.error);
        }

        sucessCallback = null;
        failCallback = null;
    }

    private static void Rs_CreateNickNameDev()
    {
        GameProtocol.RS_CreateNickName protocol = new GameProtocol.RS_CreateNickName();

        protocol.nickName = NppCreateNickName.nickName;

        if (null != sucessCallback)
            sucessCallback(protocol);
    }
}
