using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkillMgr
{
    private Dictionary<int /*skill ID*/, UserSkillInfo> userSkillDic = new Dictionary<int, UserSkillInfo>();
    private int[] equipSkillIDs = null;

    public void ReserFromServer(GameProtocol.SkillInfo[] userCharSkillInfo_, int[] equipSkillIDs_)
    {
        userSkillDic.Clear();
        
        if(null != userCharSkillInfo_)
        {
            for (int i = 0; i < userCharSkillInfo_.Length; i++)
            {
                if (null == userCharSkillInfo_[i])
                    continue;

                UserSkillInfo skillInfo = new UserSkillInfo();
                skillInfo.ResetFromServer(userCharSkillInfo_[i]);

                userSkillDic.Add(userCharSkillInfo_[i].skillID, skillInfo);
            }
        }

        equipSkillIDs = new int[equipSkillIDs_.Length];

        for(int i = 0; i < equipSkillIDs_.Length; i++)
        {
            equipSkillIDs[i] = equipSkillIDs_[i];
        }
    }

    public Dictionary<int /*skill ID*/, UserSkillInfo>.Enumerator GetUserSkillDic_Etor()
    {
        return this.userSkillDic.GetEnumerator();
    }

    public List<UserSkillInfo> GetEquipList()
    {
        List<UserSkillInfo> skillList = new List<UserSkillInfo>();

        for (int i = 0; i < equipSkillIDs.Length; i++)
        {
            skillList.Add(userSkillDic[equipSkillIDs[i]]);
        }

        return skillList;
    }

    public List<UserSkillInfo> GetNonEquipList()
    {
        List<UserSkillInfo> allSkillList = new List<UserSkillInfo>();
        List<UserSkillInfo> equipSkillList;
        List<UserSkillInfo> nonEquipSkillList;

        var userSkillDicEtor = GetUserSkillDic_Etor();

        while(true == userSkillDicEtor.MoveNext())
        {
            allSkillList.Add(userSkillDicEtor.Current.Value);
        }

        equipSkillList = GetEquipList();

        for(int i = 0; i < equipSkillList.Count; i++)
        {
            if(allSkillList.Contains(equipSkillList[i]))
            {
                allSkillList.Remove(equipSkillList[i]);
            }
        }
        nonEquipSkillList = allSkillList;

        return nonEquipSkillList;
    }
}
