using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRecord : IRecord
{
    public class SkillInfo
    {
        public int id = GameData.INVALID_ID;
        public int requireLevel = 0;
    }

    public SkillInfo[] Info = null;

    private Dictionary<int /*ID*/, SkillInfo> _dataDic = new Dictionary<int, SkillInfo>();

    public override void DeSerialize(SimpleJSON.JSONNode root)
    {
        var rootInfo = root["Info"];
        this.Info = new SkillInfo[rootInfo.Count];

        for (int index = 0; index < this.Info.Length; index++)
        {
            if (null == this.Info[index])
                this.Info[index] = new SkillInfo();
            Info[index].id = rootInfo[index]["ID"];
            Info[index].requireLevel = rootInfo[index]["RequireLevel"];
        }

        DeSerialize();
    }

    private void DeSerialize()
    {
        for (int index = 0; index < this.Info.Length; index++)
        {
            if (null != this.Info[index])
            {
                if (false == this._dataDic.ContainsKey(this.Info[index].id))
                    this._dataDic[this.Info[index].id] = this.Info[index];
            }
        }
    }

    public SkillInfo FindSkillByID(int skillID_)
    {
        return this._dataDic[skillID_];
    }
}
