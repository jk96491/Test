using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkillMgr
{
    private Dictionary<int /*skill ID*/, UserSkillInfo> equipedSkillDic = new Dictionary<int, UserSkillInfo>();

    public void ReserFromServer(GameProtocol.SkillInfo[] equipedSkillInfo_)
    {
        equipedSkillDic.Clear();
        
        if(null != equipedSkillInfo_)
        {
            for (int i = 0; i < equipedSkillInfo_.Length; i++)
            {
                if (null == equipedSkillInfo_[i])
                    continue;

                UserSkillInfo skillInfo = new UserSkillInfo();
                skillInfo.ResetFromServer(equipedSkillInfo_[i]);

                equipedSkillDic.Add(equipedSkillInfo_[i].skillID, skillInfo);
            }
        }
    }
}
