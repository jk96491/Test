using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayoutBase : MonoBehaviour
{
    public void InitLayout()
    {
        Initialize();
    }
    
    public void RefreshLayout()
    {
        OnRefresh();
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void OnRefresh()
    {

    }
}
