using System;
using System.Collections.Generic;

public class TableRecord : IRecord
{
    public class TableInfo
    {
        public GameData.DataType type = GameData.DataType.NONE;
        public string path = string.Empty;
    }

    public TableInfo[] Info = null;

    private Dictionary<GameData.DataType, string/*path*/> _dataDic = new Dictionary<GameData.DataType, string>();

    public override void DeSerialize(SimpleJSON.JSONNode root)
    {
        var rootInfo = root["Info"];
        this.Info = new TableInfo[rootInfo.Count];

        for(int index = 0; index < this.Info.Length; index++)
        {   if (null == this.Info[index])
                this.Info[index] = new TableInfo();
            Info[index].path = rootInfo[index]["Path"].Value;
            Info[index].type = (GameData.DataType)Enum.Parse(typeof(GameData.DataType),rootInfo[index]["Type"].Value);
        }
    }
}
