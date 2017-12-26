﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILCSCharacterListLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;
    [SerializeField]
    private cUIScrollView scrollView = null;

    private List<CharacterRecord.CharacterInfo> allCharacterList = new List<CharacterRecord.CharacterInfo>();

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));

        OnRefresh();
    }

    protected override void OnRefresh()
    {
        SetCharacterScroll();
    }

    private void HandleOnClickExitButton()
    {
        UIManager.Instance.CloseUI(UIManager.UIType.UI_LAYER_CHARACTER_SCROLL);
        UIManager.Instance.RefreshUI(UIManager.UIType.UI_LAYER_CHARACTER_INFO);
    }

    private void SetCharacterScroll()
    {
        allCharacterList.Clear();
        
        var characterEtor = GameData.Instance.characterRecord.GetCharacterDic_Etor();

        while (true == characterEtor.MoveNext())
        {
            CharacterRecord.CharacterInfo charInfo = characterEtor.Current.Value;

            if (null != charInfo)
            {
                CharacterRecord.CharacterInfo character = GameData.Instance.characterRecord.FindCharacterInfoByID(charInfo.id);

                if (null != character)
                {
                    allCharacterList.Add(character);
                }
            }
        }

        if (null != scrollView)
        {
            scrollView.Init(allCharacterList.Count, SetCharacterView);
            scrollView.ResetPosition();
        }
    }

    private void SetCharacterView(UIViewBase viewBase_, int index_)
    {
        UIChacterView characterView = viewBase_ as UIChacterView;

        if (null == characterView)
            return;

        CharacterRecord.CharacterInfo curCharacterInfo = allCharacterList[index_];
        
        if (null == curCharacterInfo)
            return;

        string texturePath = GameData.Instance.resourceRecord.FindResoruceByID(curCharacterInfo.textureID);

        Texture texture = Resources.Load<Texture>(texturePath) as Texture;

        if(null != texture)
        {
            characterView.SetTexture(texture);
        }
    }
}
