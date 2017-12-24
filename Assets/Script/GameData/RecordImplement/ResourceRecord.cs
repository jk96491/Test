using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRecord : IRecord
{
    public const int UI_LOGIN = 101;
    public const int UI_LOBBY = 201;
    public const int UI_CREATE_NICKNAME = 301;
    public const int UI_WATING = 401;
    public const int UI_CHARACTER_INFO = 501;
    public const int UI_CHARACTER_SCROLL = 601;

    public class ResourcreInfo
    {
        public int ID = GameData.INVALID_ID;
        public string path = string.Empty;
    }

    public ResourcreInfo[] Info = null;

    private Dictionary<int /*ID*/, string/*path*/> _dataDic = new Dictionary<int, string>();

    public override void DeSerialize(SimpleJSON.JSONNode root)
    {
        var rootInfo = root["Info"];
        this.Info = new ResourcreInfo[rootInfo.Count];

        for (int index = 0; index < this.Info.Length; index++)
        {
            if (null == this.Info[index])
                this.Info[index] = new ResourcreInfo();
            Info[index].ID = rootInfo[index]["ID"];
            Info[index].path = rootInfo[index]["Path"];
        }

        DeSerialize();
    }

    public void DeSerialize()
    {
        for(int index = 0; index < this.Info.Length; index++)
        {
            if(null != this.Info[index])
            {
                if(false == this._dataDic.ContainsKey(this.Info[index].ID))
                    this._dataDic[this.Info[index].ID] = this.Info[index].path;
            }
        }
    }

    public string FindResoruceByID(int ID_)
    {
        string path = null;

        if(true == this._dataDic.ContainsKey(ID_))
        {
            path = this._dataDic[ID_];
        }

        return path;
    }
}
