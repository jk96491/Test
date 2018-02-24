using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILGMainLayout : UI_LayoutBase
{
    [SerializeField]
    private UIButton attackBtn = null;
    [SerializeField]
    private UIEventListener[] skillBtns = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        if(null != attackBtn)
        {
            this.attackBtn.onClick.Add(new EventDelegate(HandleOnClickAttackButton));
        }

        if(null != skillBtns)
        {
            for(int i = 0; i < this.skillBtns.Length; i++)
            {
                if(null != this.skillBtns[i])
                {
                    this.skillBtns[i].onClick = HandleOnClickSkillButton;
                }
            }
        }
    }

    protected override void OnRefresh()
    {

    }

    private void HandleOnClickAttackButton()
    {
        
    }

    private void HandleOnClickSkillButton(GameObject obj_)
    {

    }
}
