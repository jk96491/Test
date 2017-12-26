using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecord : IRecord
{
    public class CharacterInfo
    {
        public int id = GameData.INVALID_ID;
        public int textureID = GameData.INVALID_ID;
        public int modelID = GameData.INVALID_ID;
        public int maxHP = 0;
        public string name = string.Empty;
        public string desc = string.Empty;
    }

    public CharacterInfo[] Info = null;

    private Dictionary<int /*ID*/, CharacterInfo> _dataDic = new Dictionary<int, CharacterInfo>();

    public override void DeSerialize(SimpleJSON.JSONNode root)
    {
        var rootInfo = root["Info"];
        this.Info = new CharacterInfo[rootInfo.Count];

        for (int index = 0; index < this.Info.Length; index++)
        {
            if (null == this.Info[index])
                this.Info[index] = new CharacterInfo();
            Info[index].id = rootInfo[index]["ID"];
            Info[index].textureID = rootInfo[index]["TextureID"];
            Info[index].modelID = rootInfo[index]["ModelID"];
            Info[index].maxHP = rootInfo[index]["MAXHP"];
            Info[index].name = rootInfo[index]["Name"];
            Info[index].desc = rootInfo[index]["Desc"];
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

    public CharacterInfo FindCharacterInfoByID(int id_)
    {
        return this._dataDic[id_];
    }

    public Dictionary<int, CharacterInfo>.Enumerator GetCharacterDic_Etor()
    {
        return this._dataDic.GetEnumerator();
    }
}
