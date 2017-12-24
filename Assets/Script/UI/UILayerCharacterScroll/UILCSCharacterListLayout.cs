using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILCSCharacterListLayout : UI_LayoutBase
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
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_SCROLL);
        UIManager.Instance.RefreshUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
    }
}
