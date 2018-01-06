using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class NppSetUserParty
{
    private static int userID = GameData.INVALID_ID;
    private static int[] characterIDs = new int[3];

    private static string subURL = string.Empty;
    static Action<GameProtocol.Rs_SetUserParty> sucessCallback;
    static Action<string> failCallback;

    public static void RequestSetUserParty(int userID_, int[] characterIDs, Action<GameProtocol.Rs_SetUserParty> SuccessCallback_, Action<string> FailCallback_)
    {
        NppSetUserParty.userID = userID_;
        NppSetUserParty.characterIDs[0] = characterIDs[0];
        NppSetUserParty.characterIDs[1] = characterIDs[1];
        NppSetUserParty.characterIDs[2] = characterIDs[2];

        sucessCallback = SuccessCallback_;
        failCallback = FailCallback_;

        GameProtocol.Rq_SetUserParty protocol = new GameProtocol.Rq_SetUserParty();
        protocol.userID = userID_;
        protocol.characterIDs = new int[3];
        protocol.characterIDs[0] = characterIDs[0];
        protocol.characterIDs[1] = characterIDs[1];
        protocol.characterIDs[2] = characterIDs[2];

        //subURL = string.Format("users_nickname/?pid={0}&nick={1}", protocol.userID, protocol.nickName);

        if (true == NetworkManager.Instance.IsNetWorkMode)
        {
            NetworkManager.Instance.ConnectServerByGet(Rs_SetUserParty, subURL);
        }
        else
        {
            Rs_SetUserParty();
        }

    }

    private static void Rs_SetUserParty(WWW Data)
    {
        if (true == string.IsNullOrEmpty(Data.error))
        {
            GameProtocol.Rs_SetUserParty Protocol = new GameProtocol.Rs_SetUserParty();
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

    private static void Rs_SetUserParty()
    {
        GameProtocol.Rs_SetUserParty protocol = new GameProtocol.Rs_SetUserParty();

        protocol.partyInfo = new GameProtocol.PartyInfo();
        protocol.partyInfo.partyArray = new int[3];
        protocol.partyInfo.partyArray[0] = characterIDs[0];
        protocol.partyInfo.partyArray[1] = characterIDs[1];
        protocol.partyInfo.partyArray[2] = characterIDs[2];

        if (null != sucessCallback)
            sucessCallback(protocol);
    }
}
