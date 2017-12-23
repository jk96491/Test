using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkillInfo
{
    private int id = GameData.INVALID_ID;
    private int level = 0;
    private int requireLevel = 0;

    public void ResetFromServer(GameProtocol.SkillInfo skillInfo_)
    {
        id = skillInfo_.skillID;
        level = skillInfo_.level;

        SkillRecord.SkillInfo skillInfo = GameData.Instance.skillRecord.FindSkillByID(id);

        if(null != skillInfo)
        {
            requireLevel = skillInfo.requireLevel;
        }
    }
}
