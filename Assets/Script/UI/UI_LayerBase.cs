using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayerBase : MonoBehaviour
{
    protected List<UI_LayoutBase> layoutList = new List<UI_LayoutBase>();

    public void Acivate()
    {
        this.gameObject.SetActive(true);
        InitAllLayout();
    }

    public void DeActivate()
    {
        this.gameObject.SetActive(false);
    }

    public void InitUI()
    {
        Initialize();
        Acivate();
    }

    private void InitAllLayout()
    {
        for(int i = 0; i < layoutList.Count; i++)
        {
            if(null != layoutList[i])
            {
                layoutList[i].InitLayout(this);
            }
        }
    }

    private void RefreshAllLayout()
    {
        for (int i = 0; i < layoutList.Count; i++)
        {
            if (null != layoutList[i])
            {
                layoutList[i].RefreshLayout();
            }
        }
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void Refresh()
    {

    }

    public void RefreshUI()
    {
        Refresh();
    }

}
