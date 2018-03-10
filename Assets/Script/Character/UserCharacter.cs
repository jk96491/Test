using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCharacter 
{
    private int id = GameData.INVALID_ID;
    private int exp = 0;
    private int level = 0;
    private float fatigue = 0;

    public UserSkillMgr skillMgr = new UserSkillMgr();

    public int ID { get { return this.id; } }
    public int Exp { get { return this.exp; } }
    public int Level { get { return this.level; } }
    public float Fatigue { get { return this.fatigue; } }

    public void ResetFromServer(GameProtocol.CharacterInfo characterInfo)
    {
        id = characterInfo.id;
        exp = characterInfo.exp;
        level = characterInfo.level;
        fatigue = characterInfo.fatigue;

        skillMgr.ReserFromServer(characterInfo.userCharSkillInfo, characterInfo.equipUserSkillIDs);
    }
}
