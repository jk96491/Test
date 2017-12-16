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

    [Header("재화 정보")]
    [SerializeField]
    private UILabel goldLabel = null;
    [SerializeField]
    private UILabel gemLabel = null;

    protected override void Initialize(UI_LayerBase layerUI_)
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

        int gold = UserManager.Instance.localUser.Gold;
        SetGoldLabel(gold);

        int gem = UserManager.Instance.localUser.Gem;
        SetGemLabel(gem);
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

    public void SetGoldLabel(int gold_)
    {
        if (null != goldLabel)
            goldLabel.text = gold_.ToString();
    }

    public void SetGemLabel(int gem_)
    {
        if (null != gemLabel)
            gemLabel.text = gem_.ToString();
    }
}
