using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LCICharacterLayout : UI_LayoutBase
{
    [SerializeField]
    private UIChacterView[] characterViews = null;

    protected override void Initialize(UI_LayerBase layerUI_)
    {
        OnRefresh();
    }

    protected override void OnRefresh()
    {

    }


}
