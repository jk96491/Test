using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCICharacterLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;
    [SerializeField]
    private UIChacterView[] characterViews = null;
    private CharacterExhibition exhibition;
    private UILayerCharacterInfo parentUILayer = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        parentUILayer = layerUI_ as UILayerCharacterInfo;
        exhibition = GameObject.Find("CharacterExhibition").GetComponent<CharacterExhibition>();
        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));
        OnRefresh();
    }

    protected override void OnRefresh()
    {
        SetCharacter();
    }
    private void HandleOnClickExitButton()
    {
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
        UI_LayerLobby lobbyLayer = UIManager.Instance.GetUILayer(UIManager.UIType.UI_LAYER_LOBBY) as UI_LayerLobby;
        if (null != lobbyLayer)
            lobbyLayer.GoToHome();
    }

    private void SetCharacter()
    {
        for(int i = 0; i < characterViews.Length; i++)
        {
            if (null == characterViews[i])
                continue;

            int characterID = UserManager.Instance.localUser.FindPartyCharacterByIndex(i);
            if ( GameData.INVALID_ID == characterID)
            {
                characterViews[i].SetActiveInfoObj(false);
            }
            else
            {
                UserCharacter userChar = UserManager.Instance.localUser.FindChracterByID(characterID);

                if(null != userChar)
                {
                    characterViews[i].dataIndex = characterID;
                    characterViews[i].slotType = UIViewBase.SLOT_TYPE.CHARACTER_SLOT;
                    characterViews[i].viewIndex = i;
                    characterViews[i].SetActiveInfoObj(true);
                    characterViews[i].SetLevelLabel(userChar.Level);
                    characterViews[i].SetFatigue(userChar.Fatigue / 100f);

                    CharacterRecord.CharacterInfo charInfo = GameData.Instance.characterRecord.FindCharacterInfoByID(characterID);

                    if(null != charInfo)
                    {
                        characterViews[i].SetNameLabel(charInfo.name);
                    }

                    characterViews[i].onClick = HandleOnClickCharaterSlot;
                }
            }

            if (null != exhibition)
                exhibition.SetCharacter(i, characterID);
        }
    }

    public void HandleOnClickCharaterSlot(UIViewBase viewBase_)
    {
        exhibition.FocusCharacter(viewBase_.viewIndex);
        
        if(null != parentUILayer)
        {
            parentUILayer.ActiveLayout(UILayerCharacterInfo.LayoutType.SKILL);
        }
    }
}
