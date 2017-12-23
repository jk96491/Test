using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCISkillLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;
    private UILayerCharacterInfo parentUILayer = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        parentUILayer = layerUI_ as UILayerCharacterInfo;
        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));
    }

    protected override void OnRefresh()
    {

    }

    private void HandleOnClickExitButton()
    {
        if (null != parentUILayer)
        {
            parentUILayer.ActiveLayout(UILayerCharacterInfo.LayoutType.CHRACTER);
        }
    }
}
