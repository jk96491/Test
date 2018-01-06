using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayerGame : UI_LayerBase
{
    [SerializeField]
    private UILGMainLayout mainLayout = null;

    protected override void Initialize()
    {
        this.layoutList.Add(mainLayout);
    }

    protected override void Refresh()
    {

    }
}
