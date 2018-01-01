﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCICharacterLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;
    [SerializeField]
    private UIButton characterListButton = null;
    [SerializeField]
    private UIChacterView[] characterViews = null;
    [SerializeField]
    private UIChacterView dragCharacterView = null;

    private CharacterExhibition exhibition;
    private UILayerCharacterInfo parentUILayer = null;

    public bool isDragging = false;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        parentUILayer = layerUI_ as UILayerCharacterInfo;
        exhibition = GameObject.Find("CharacterExhibition").GetComponent<CharacterExhibition>();

        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));
        if (null != characterListButton)
            characterListButton.onClick.Add(new EventDelegate(HandleOnClickCharacterList));

        OnRefresh();
    }

    protected override void OnRefresh()
    {
        this.dragCharacterView.SetActiveViewObj(false);
        SetCharacter();
        if (false == UIManager.Instance.IsActiveUILayer(UIManager.UIType.UI_LAYER_CHARACTER_SCROLL))
            SetCharacterViewInfoPos(new Vector3(0, 0, 0));
    }

    private void HandleOnClickCharacterList()
    {
        UIManager.Instance.OpenUI(UIManager.UIType.UI_LAYER_CHARACTER_SCROLL);

        SetCharacterViewInfoPos(new Vector3(0, 70, 0));
    }
    private void HandleOnClickExitButton()
    {
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_SCROLL);
        UI_LayerLobby lobbyLayer = UIManager.Instance.GetUILayer(UIManager.UIType.UI_LAYER_LOBBY) as UI_LayerLobby;
        if (null != lobbyLayer)
            lobbyLayer.GoToHome();
    }

    private void SetCharacterViewInfoPos(Vector3 pos_)
    {
        for(int i = 0; i < characterViews.Length; i++)
        {
            if (null != characterViews[i])
                characterViews[i].SetInfoPos(pos_);
        }
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
                    characterViews[i].onDragStart = HandleOnDragStart;
                    characterViews[i].onDrop = HandleOnDrop;
                    characterViews[i].onDragEnd = HandleOnDragEnd;
                }
            }

            if (null != exhibition)
                exhibition.SetCharacter(i, characterID);
        }
    }

    private void Update()
    {
        if (false == isDragging)
            return;

        this.dragCharacterView.transform.localPosition = UICamera.lastWorldPosition;
    }

    public void HandleOnClickCharaterSlot(UIViewBase viewBase_)
    {
        exhibition.FocusCharacter(viewBase_.viewIndex);
        
        if(null != parentUILayer)
        {
            parentUILayer.ActiveLayout(UILayerCharacterInfo.LayoutType.SKILL);
        }
    }

    public void HandleOnDragStart(UIViewBase viewBase_)
    {
        this.dragCharacterView.SetActiveViewObj(true);
        isDragging = true;
    }

    public void HandleOnDrop(UIViewBase dropBase_, UIViewBase dragBase_)
    {
        this.dragCharacterView.SetActiveViewObj(false);
        isDragging = false;

        int[] characterIDs = new int[3];

        characterIDs[0] = UserManager.Instance.localUser.FindPartyCharacterByIndex(0);
        characterIDs[1] = UserManager.Instance.localUser.FindPartyCharacterByIndex(1);
        characterIDs[2] = UserManager.Instance.localUser.FindPartyCharacterByIndex(2);

        characterIDs[dropBase_.viewIndex] = dragBase_.dataIndex;

        UserManager.Instance.localUser.RequestSetUserParty(characterIDs, HandleOnResultSetUserParty);
    }

    private void HandleOnResultSetUserParty(int result_)
    {
        if(result_ == 0)
        {
            OnRefresh();
        }
    }

    public void HandleOnDragEnd(UIViewBase viewBase_)
    {
        this.dragCharacterView.SetActiveViewObj(false);
        isDragging = false;
    }
}
