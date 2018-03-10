using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillView : UIViewBase
{
    [SerializeField]
    private UITexture skillTexture = null;
    [SerializeField]
    private UILabel skillNameLabel = null;
    [SerializeField]
    private UILabel levelLabel = null;
    [SerializeField]
    private UILabel descLabel = null;

    public void SetSkillTexture(Texture2D texture_)
    {
        if (null != skillTexture)
            skillTexture.mainTexture = texture_;
    }
	
    public void SetLevelLabel(int level_)
    {
        if (null != levelLabel)
            levelLabel.text = string.Format("Lv.{0}", level_);
    }

    public void SetSkillLevelLabel(string name_)
    {
        if(null != this.skillNameLabel)
        {
            this.skillNameLabel.text = name_;
        }
    }

    public void SetDescLabel(string desc_)
    {
        if (null != descLabel)
            descLabel.text = desc_;
    }
}
