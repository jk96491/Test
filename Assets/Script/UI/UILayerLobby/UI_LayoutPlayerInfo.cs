using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LayoutPlayerInfo : UI_LayoutBase
{
    [Header("유저 정보")]
    [SerializeField]
    private UITexture profileTexture = null;
    [SerializeField]
    private UILabel nickNameLabel = null;
    [SerializeField]
    private UILabel levelLabel = null;
    [SerializeField]
    private UILabel progressExpLabel = null;
    [SerializeField]
    private UIProgressBar expProgress = null;

    protected override void Initialize()
    {
        OnRefresh();
    }

    protected override void OnRefresh()
    {
        SerUserInfo();
    }

    private void SerUserInfo()
    {
        string nickName = UserManager.Instance.localUser.NickName;
        SetNickNameLabel(nickName);

        int userLevel = UserManager.Instance.localUser.Level;
        SetLevelLabel(userLevel);

        int maxExp = 1000;
        int userExp = UserManager.Instance.localUser.Exp + 265;

        float expValue = (float)userExp / maxExp;
        SetExpProgress(expValue);
        SetProgressExpLabel((int)(expValue * 100));
    }

    private void SetUserTexture(Texture tex_)
    {
        if (null != profileTexture)
            profileTexture.mainTexture = tex_;
    }

    private void SetNickNameLabel(string nickName_)
    {
        if (null != nickNameLabel)
            nickNameLabel.text = nickName_;
    }

    private void SetProgressExpLabel(int percent_)
    {
        if (null != progressExpLabel)
            progressExpLabel.text = string.Format("{0} %", percent_);
    }

    private void SetLevelLabel(int level_)
    {
        if (null != nickNameLabel)
            levelLabel.text = string.Format("Lv.{0}", level_);
    }

    public void SetExpProgress(float value_)
    {
        if (null != expProgress)
            expProgress.value = value_;
    }
}
