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
    private UITexture characterTexture = null;
    [SerializeField]
    private UIProgressBar fatigueProgress = null;
    [SerializeField]
    private GameObject InfoObj = null;
    [SerializeField]
    private Transform InfoTrans = null;
    [SerializeField]
    private Vector3 FirstInfoPos = Vector3.zero;
    [SerializeField]
    private UITexture texture = null;

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

    public void SetInfoPos(Vector3 pos_)
    {
        if (null != InfoTrans)
            InfoTrans.localPosition = FirstInfoPos + pos_;
    }

    public void SetTexture(Texture texture_)
    {
        if (null != characterTexture)
            characterTexture.mainTexture = texture_;
    }

    public Texture GetTexture()
    {
        if (null != this.characterTexture)
            return this.characterTexture.mainTexture;
        return null;
    }
}
