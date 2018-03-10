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

    private int characterID = GameData.INVALID_ID;


    protected override void Initialize(UI_LayerBase layerUI_)
    {
        parentUILayer = layerUI_ as UILayerCharacterInfo;
        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));
    }

    protected override void OnRefresh()
    {
        SetSkillSlots();
    }

    private void SetSkillSlots()
    {
        UserCharacter userChar = UserManager.Instance.localUser.FindChracterByID(characterID);
        List<UserSkillInfo> userSkillList = new List<UserSkillInfo>();

        if(null != userChar)
        {
            userSkillList.AddRange(userChar.skillMgr.GetEquipList());
            userSkillList.AddRange(userChar.skillMgr.GetNonEquipList());
        }

        for(int i = 0; i < skillSlotView.Length; i++)
        {
            if (null == skillSlotView[i])
                continue;

            UserSkillInfo info = userSkillList[i];

            if(null != info)
            {

            }
        }
    }

    private void HandleOnClickExitButton()
    {
        if (null != parentUILayer)
        {
            parentUILayer.ActiveLayout(UILayerCharacterInfo.LayoutType.CHRACTER);
        }
    }

    public void SetCharacterID(int characterID_)
    {
        this.characterID = characterID_;
    }
}
