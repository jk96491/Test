using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayoutMenu : UI_LayoutBase
{
    private UI_LayerLobby lobbyUI;
    [SerializeField]
    private UIToggle characterToggle = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        lobbyUI = layerUI_ as UI_LayerLobby;
        base.Initialize(layerUI_);
    }

    public void SetActiveLobbyUICommonLayout()
    {
        if (null != characterToggle)
            this.lobbyUI.SetActiveCommonLayout(!characterToggle.value);
    }
}
