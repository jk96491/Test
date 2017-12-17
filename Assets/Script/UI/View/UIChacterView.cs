using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChacterView : UIViewBase
{
    [SerializeField]
    private UILabel overallLabel = null;
    [SerializeField]
    private UILabel levelLabel = null;
    [SerializeField]
    private UILabel nameLabel = null;
    [SerializeField]
    private UIProgressBar fatigueProgress = null;
    [SerializeField]
    private GameObject InfoObj = null;

    public void SetOverallLabel(int overall_)
    {
        if (null != overallLabel)
            overallLabel.text = overall_.ToString();
    }

    public void SetLevelLabel(int level_)
    {
        if (null != levelLabel)
            levelLabel.text = string.Format("Lv.{0}", level_);
    }

    public void SetNameLabel(string name_)
    {
        if (null != nameLabel)
            nameLabel.text = name_;
    }

    public void SetFatigue(float value_)
    {
        if (null != fatigueProgress)
            fatigueProgress.value = value_;
    }

    public void SetActiveInfoObj(bool active_)
    {
        if (null != InfoObj)
            InfoObj.SetActive(active_);
    }
}
