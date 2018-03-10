using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCISkillLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;
    [SerializeField]
    private UISkillView[] skillSlotView = null;
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

    private void SetSkillSlots()
    {
        for(int i = 0; i < skillSlotView.Length; i++)
        {
            if (null == skillSlotView[i])
                continue;
        }
    }

    private void HandleOnClickExitButton()
    {
        if (null != parentUILayer)
        {
            parentUILayer.ActiveLayout(UILayerCharacterInfo.LayoutType.CHRACTER);
        }
    }
}
