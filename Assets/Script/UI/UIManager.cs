using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public enum UIType
    {
        UI_LAYER_LOGIN,
        UI_LAYER_LOBBY,
        UI_LAYER_CREATE_NICKNAME,
        UI_LAYER_CHARACTER_INFO,
        UI_WATING = 50,
    }

    [SerializeField]
    private Transform UI_ParentTrans = null;
    [SerializeField]
    private UIRoot root = null;

    private Dictionary<UIType, UI_LayerBase> _loadedUI_Dic = new Dictionary<UIType, UI_LayerBase>();
    
    public void OpenUI(UIType type_)
    {
        if(true == _loadedUI_Dic.ContainsKey(type_))
        {
            //_loadedUI_Dic[type_].Acivate();
            _loadedUI_Dic[type_].gameObject.SetActive(true);
            _loadedUI_Dic[type_].RefreshUI();
        }
        else
        {
            GameObject uiObj = null;
            UI_LayerBase uiLayer = null;

            switch (type_)
            {
                case UIType.UI_LAYER_LOGIN:
                    {
                        uiObj = Instantiate(ResourceUtil.RoadPrefab(ResourceRecord.UI_LOGIN));
                        uiLayer = uiObj.GetComponent<UI_LayerBase>();
                    }
                    break;
                case UIType.UI_LAYER_LOBBY:
                    {
                        uiObj = Instantiate(ResourceUtil.RoadPrefab(ResourceRecord.UI_LOBBY));
                        uiLayer = uiObj.GetComponent<UI_LayerBase>();
                    }
                    break;
                case UIType.UI_LAYER_CREATE_NICKNAME:
                    {
                        uiObj = Instantiate(ResourceUtil.RoadPrefab(ResourceRecord.UI_CREATE_NICKNAME));
                        uiLayer = uiObj.GetComponent<UI_LayerBase>();
                    }
                    break;
                case UIType.UI_LAYER_CHARACTER_INFO:
                    {
                        uiObj = Instantiate(ResourceUtil.RoadPrefab(ResourceRecord.UI_CHARACTER_INFO));
                        uiLayer = uiObj.GetComponent<UI_LayerBase>();
                    }
                    break;
            }

            if(null != uiObj && null != uiLayer)
            {
                _loadedUI_Dic.Add(type_, uiLayer);
                uiObj.SetActive(true);
                uiObj.transform.SetParent(UI_ParentTrans);
                uiObj.transform.localPosition = Vector3.zero;
                uiObj.transform.localScale = Vector3.one;
                uiLayer.InitUI();
            }
            else
            {
                Debug.LogError(string.Format("Do Not Open UI : {0}", type_));
            }
        }
    }

    public void CloseUI(UIType type_)
    {
        if(true == _loadedUI_Dic.ContainsKey(type_))
        {
            _loadedUI_Dic[type_].DeActivate();
        }
    }

    public void CloseAllUI()
    {
        var UI_DicEtor = this._loadedUI_Dic.GetEnumerator();

        while(true == UI_DicEtor.MoveNext())
        {
            UI_LayerBase uiLayer = UI_DicEtor.Current.Value;

            if(null != uiLayer)
            {
                uiLayer.DeActivate();
            }
        }
    }

    public void OpenWatingUI()
    {
        if (true == _loadedUI_Dic.ContainsKey(UIType.UI_WATING))
        {
            _loadedUI_Dic[UIType.UI_WATING].Acivate();
        }
        else
        {
            GameObject uiObj = null;
            UI_LayerBase uiLayer = null;

            uiObj = Instantiate(ResourceUtil.RoadPrefab(ResourceRecord.UI_WATING));
            uiLayer = uiObj.GetComponent<UI_LayerBase>();

            if (null != uiObj && null != uiLayer)
            {
                _loadedUI_Dic.Add(UIType.UI_WATING, uiLayer);
                uiObj.SetActive(true);
                uiObj.transform.SetParent(UI_ParentTrans);
                uiObj.transform.localPosition = Vector3.zero;
                uiObj.transform.localScale = Vector3.one;
                uiLayer.Acivate();
                uiObj.GetComponent<UIPanel>().depth = 10;
            }
            else
            {
                Debug.LogError(string.Format("Do Not Open UI : {0}", UIType.UI_WATING));
            }
        }
    }

    public void CloseWatingUI()
    {
        if (true == _loadedUI_Dic.ContainsKey(UIType.UI_WATING))
        {
            _loadedUI_Dic[UIType.UI_WATING].DeActivate();
        }
    }

    public UI_LayerBase GetUILayer(UIType type_)
    {
        UI_LayerBase uiLayer = null;

        uiLayer = _loadedUI_Dic[type_];

        return uiLayer;
    }
}
