using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCharacter 
{

    private int id = GameData.INVALID_ID;
    private int exp = 0;
    private int level = 0;

    public int ID { get { return this.id; } }
    public int Exp { get { return this.exp; } }
    public int Level { get { return this.level; } }

    public void ResetFromServer(GameProtocol.CharacterInfo characterInfo)
    {
        id = characterInfo.id;
        exp = characterInfo.exp;
        level = characterInfo.level;
    }
}
