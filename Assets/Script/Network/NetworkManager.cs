using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager> {

    public enum ProtocolType
    {
        NONE = -1,
        LOGIN,
    }


    private string IP = "anylee.iptime.org";
    private string Port = "3000";

    protected string RequestURL = null;
    public bool IsNetWorkMode = false;
    
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ConnectServer(Action<WWW> RsFuntion ,string subUrl_)
    {
        RequestURL = string.Format("{0}:{1}/{2}", IP, Port, subUrl_);
        StartCoroutine(Connect(RsFuntion));
    }

    protected virtual IEnumerator Connect(Action<WWW> RsFuntion)
    {
        WWW www = new WWW(RequestURL);
        yield return www;

        if (null != RsFuntion)
            RsFuntion(www);
    }
}
