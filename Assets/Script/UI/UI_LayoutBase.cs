using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayoutBase : MonoBehaviour
{
    public void InitLayout(UI_LayerBase layerUI_)
    {
        Initialize(layerUI_);
    }
    
    public void RefreshLayout()
    {
        OnRefresh();
    }

    protected virtual void Initialize(UI_LayerBase layerUI_)
    {

    }

    protected virtual void OnRefresh()
    {

    }
}
