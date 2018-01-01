using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILCSCharacterListLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton exitButton = null;
    [SerializeField]
    private cUIScrollView scrollView = null;
    [SerializeField]
    private UIChacterView DrageCharacterView = null;

    private List<CharacterRecord.CharacterInfo> allCharacterList = new List<CharacterRecord.CharacterInfo>();

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        if (null != exitButton)
            exitButton.onClick.Add(new EventDelegate(HandleOnClickExitButton));

        OnRefresh();
    }

    protected override void OnRefresh()
    {
        if (null != DrageCharacterView)
            DrageCharacterView.gameObject.SetActive(false);

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

        characterView.dataIndex = curCharacterInfo.id;
        characterView.slotType = UIViewBase.SLOT_TYPE.CHARACTER_LIST;
        characterView.viewIndex = index_;

        string texturePath = GameData.Instance.resourceRecord.FindResoruceByID(curCharacterInfo.textureID);

        Texture texture = Resources.Load<Texture>(texturePath) as Texture;

        if(null != texture)
        {
            characterView.SetTexture(texture);
        }

        if(null != UserManager.Instance.localUser.FindChracterByID(characterView.dataIndex))
        {
            characterView.onDragStart = HandleOnDragStart;
            characterView.onDrop = HandleOnDrop;
            characterView.onDragEnd = HandleOnDragEnd;
            characterView.SetActive(true);
        }
        else
        {
            characterView.onDragStart = null;
            characterView.onDrop = null;
            characterView.onDragEnd = null;
            characterView.SetActive(false);
        }
    }

    public void HandleOnDragStart(UIViewBase viewBase_)
    {
        if (null == DrageCharacterView)
            return;

        DrageCharacterView.gameObject.SetActive(true);

        UIChacterView charView = viewBase_ as UIChacterView;

        if(null != charView)
        {
            DrageCharacterView.SetTexture(charView.GetTexture());
        }

    }

    public void HandleOnDrop(UIViewBase dropBase_, UIViewBase dragBase_)
    {
       
    }

    public void HandleOnDragEnd(UIViewBase viewBase_)
    {
        if (null != DrageCharacterView)
            DrageCharacterView.gameObject.SetActive(false);
    }
}
