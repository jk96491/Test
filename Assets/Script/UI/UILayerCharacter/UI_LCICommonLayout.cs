using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCICommonLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));
    }

    protected override void OnRefresh()
    {

    }

    private void HandleOnClickExitButton()
    {
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
        UI_LayerLobby lobbyLayer = UIManager.Instance.GetUILayer(UIManager.UIType.UI_LAYER_LOBBY) as UI_LayerLobby;
        if (null != lobbyLayer)
            lobbyLayer.GoToHome();
    }
}
