using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : Singleton<NetworkManager> {

    public enum ProtocolType
    {
        NONE = -1,
        LOGIN,
    }

    public enum RequestType
    {
        GET,
        PUT,
        POST
    }



    private string IP = "anylee.iptime.org";
    private string Port = "3000";

    protected string RequestURL = null;
    public bool IsNetWorkMode = false;
    
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ConnectServerByGet(Action<WWW> RsFuntion ,string subUrl_)
    {
        RequestURL = string.Format("{0}:{1}/{2}", IP, Port, subUrl_);
        StartCoroutine(ConnectByGet(RsFuntion));
    }

    public void ConnectServerByPut(Action<UnityWebRequest> RsFuntion, string subUrl_)
    {
        RequestURL = string.Format("{0}:{1}/{2}", IP, Port, subUrl_);
        StartCoroutine(ConnectByPut(RsFuntion));
    }

    protected virtual IEnumerator ConnectByGet(Action<WWW> RsFuntion)
    {
        UIManager.Instance.OpenWatingUI();
        WWW www = new WWW(RequestURL);
        yield return www;

        UIManager.Instance.CloseWatingUI();
        if (null != RsFuntion)
            RsFuntion(www);
    }

    protected virtual IEnumerator ConnectByPut(Action<UnityWebRequest> RsFuntion)
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("Temp");
        using (UnityWebRequest www = UnityWebRequest.Put(RequestURL, myData))
        {
            yield return www.Send();

            if (null != RsFuntion)
                RsFuntion(www);
        }
    }
}
