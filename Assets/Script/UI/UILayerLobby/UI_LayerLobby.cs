using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayerLobby : UI_LayerBase
{
    [SerializeField]
    UI_LayoutPlayerInfo playerInfo = null;

    protected override void Initialize()
    {
        this.layoutList.Add(playerInfo);
    }

}
