using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayerCharacterScroll : UI_LayerBase
{
    [SerializeField]
    private UILCSCharacterListLayout characterListLayout = null;

    protected override void Initialize()
    {
        this.layoutList.Add(characterListLayout);
    }

    protected override void Refresh()
    {

    }
}
