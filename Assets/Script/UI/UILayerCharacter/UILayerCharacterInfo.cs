using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayerCharacterInfo : UI_LayerBase
{
    [SerializeField]
    private UI_LCICommonLayout commonLayout = null;
    [SerializeField]
    private UI_LCICharacterLayout characterLayout = null;

    protected override void Initialize()
    {
        this.layoutList.Add(commonLayout);
        this.layoutList.Add(characterLayout);
    }

    protected override void Refresh()
    {

    }
}
