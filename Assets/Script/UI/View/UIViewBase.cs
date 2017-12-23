using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public enum SLOT_TYPE
    {
        NONE = -1,
        CHARACTER_SLOT,
    }

    public int dataIndex = -1;
    public SLOT_TYPE slotType = SLOT_TYPE.NONE;
    public int viewIndex = -1;
    
    public delegate void OnClickDelegate(UIViewBase viewBase_);
    public OnClickDelegate onClick;

    public void OnClick()
    {
        onClick(this);
    }
}
