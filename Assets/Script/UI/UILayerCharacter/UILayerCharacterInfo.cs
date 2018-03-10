using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayerCharacterInfo : UI_LayerBase
{
    public enum LayoutType
    {
        NONE = -1,
        CHRACTER,
        SKILL
    }
    
    [SerializeField]
    private UI_LCICharacterLayout characterLayout = null;
    [SerializeField]
    private UI_LCISkillLayout skillLayout = null;
    
    protected override void Initialize()
    {
        this.layoutList.Add(characterLayout);
        this.layoutList.Add(skillLayout);
        Refresh();
    }

    protected override void Refresh()
    {
        ActiveLayout(LayoutType.CHRACTER);
    }

    public void ActiveLayout(LayoutType type_, int value = -1)
    {
        for(int i = 0; i < this.layoutList.Count; i++)
        {
            if(null != this.layoutList[i])
            {
                this.layoutList[i].DeActivate();
            }
        }

        switch (type_)
        {
            case LayoutType.CHRACTER: characterLayout.Activate(); break;
            case LayoutType.SKILL:
                skillLayout.SetCharacterID(value);
                skillLayout.Activate();
                break;
        }
    }
}
