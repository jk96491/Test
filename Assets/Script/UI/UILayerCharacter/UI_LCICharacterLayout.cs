using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCICharacterLayout : UI_LayoutBase
{
    [SerializeField]
    private UIChacterView[] characterViews = null;
    private CharacterExhibition exhibition;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        exhibition = GameObject.Find("CharacterExhibition").GetComponent<CharacterExhibition>();
        OnRefresh();
    }

    protected override void OnRefresh()
    {
        SetCharacter();
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
                    characterViews[i].SetActiveInfoObj(true);
                    characterViews[i].SetLevelLabel(userChar.Level);
                    characterViews[i].SetFatigue(userChar.Fatigue / 100f);

                    CharacterRecord.CharacterInfo charInfo = GameData.Instance.characterRecord.FindCharacterInfoByID(characterID);

                    if(null != charInfo)
                    {
                        characterViews[i].SetNameLabel(charInfo.name);
                    }
                }
            }

            if (null != exhibition)
                exhibition.SetCharacter(i, characterID);
        }
    }
}
