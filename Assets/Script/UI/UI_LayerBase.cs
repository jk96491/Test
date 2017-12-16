using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayerBase : MonoBehaviour
{
    public void Acivate()
    {
        this.gameObject.SetActive(true);
        Initialize();
    }

    public void DeActivate()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void Initialize()
    {

    }

}
