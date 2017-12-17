using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayoutMenu : UI_LayoutBase
{
    private UI_LayerLobby lobbyUI;
    [SerializeField]
    private UIToggle homeToggle = null;
    [SerializeField]
    private UIToggle characterToggle = null;
    [SerializeField]
    private UIToggle shopToggle = null;
    [SerializeField]
    private UIToggle optionToggle = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        lobbyUI = layerUI_ as UI_LayerLobby;
        base.Initialize(layerUI_);

        if(null != shopToggle)
        {
            shopToggle.onChange.Add(new EventDelegate(HandleOnClickShop));
        }

        if(null != optionToggle)
        {
            optionToggle.onChange.Add(new EventDelegate(HandleOnClickOption));
        }

        if(null != characterToggle)
        {
            characterToggle.onChange.Add(new EventDelegate(HandleOnClickCharacter));
        }
    }

    public void SetActiveLobbyUICommonLayout()
    {
        if (null != characterToggle)
            this.lobbyUI.SetActiveCommonLayout(!characterToggle.value);
    }

    private void HandleOnClickShop()
    {
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
    }

    private void HandleOnClickOption()
    {
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
    }

    private void HandleOnClickCharacter()
    {
        UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
    }

    public void SetHomeToggleActive()
    {
        if (null != homeToggle)
            homeToggle.value = true;
    }
}
