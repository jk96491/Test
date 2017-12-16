using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayerLobby : UI_LayerBase
{
    [SerializeField]
    UI_LayoutLobbyCommon commonLayOut = null;
    [SerializeField]
    UI_LayoutPlayerInfo playerInfo = null;
    [SerializeField]
    UI_LayoutMenu menu = null;

    protected override void Initialize()
    {
        this.layoutList.Add(playerInfo);
        this.layoutList.Add(menu);
    }

    public void SetActiveCommonLayout(bool active_)
    {
        if(null != commonLayOut)
            commonLayOut.gameObject.SetActive(active_);
    }

}
