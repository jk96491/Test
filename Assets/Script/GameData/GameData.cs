using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

    // 각 Record들
    public TableRecord tableRecord = null;
    public ResourceRecord resourceRecord = null;
    public CharacterRecord characterRecord = null;
    public SkillRecord skillRecord = null;

    public enum DataType : sbyte
    {
        NONE = -1,
        TABLE,
        RESOURCE,
        CHARACTER,
        SKILL
    }

    private string tablePath = "Table/TableRecord";
    public const int INVALID_ID = -1;

    static private GameData instance = new GameData();
    static public GameData Instance { get { return instance; } }

    private bool IsLoadedData = false;

    private Dictionary<DataType, IRecord> _recordDic = new Dictionary<DataType, IRecord>();     // 각 레코드 저장소

    public void LoadTableData()
    {
        if (true == this.IsLoadedData)
            return;

        if(null == this.tableRecord)
        {
            this.tableRecord = new TableRecord();
        }

        TextAsset textAsset = Resources.Load(this.tablePath) as TextAsset;

        if(null == textAsset)
        {
            Debug.LogError("textAsset is null");
            return;
        }

        JSONNode root = JSON.Parse(textAsset.text);
        this.tableRecord.DeSerialize(root);
        LoadAllData();
    }                                                   

    private void LoadAllData()
    {
        TableRecord.TableInfo[] tableInfo = this.tableRecord.Info;

        if(null != tableInfo)
        {
            for(int index = 0; index < tableInfo.Length; index++)
            {
                TextAsset textAsset = Resources.Load(tableInfo[index].path) as TextAsset;

                if (null == textAsset)
                {
                    Debug.LogError("textAsset is null");
                    continue;
                }

                JSONNode root = JSON.Parse(textAsset.text);

                if(true == this._recordDic.ContainsKey(tableInfo[index].type))
                {
                    GetRecord(tableInfo[index].type).DeSerialize(root);
                }
                else
                {
                    IRecord record = GetTypeToRecord(tableInfo[index].type);
                    record.DeSerialize(root);
                    InsertData(record, tableInfo[index].type);
                    this._recordDic.Add(tableInfo[index].type, record);
                }
            }

            this.IsLoadedData = true;
        }
    }

    private IRecord GetTypeToRecord(DataType type_)
    {
        switch(type_)
        {
            case DataType.TABLE: { return new TableRecord(); }
            case DataType.RESOURCE: { return new ResourceRecord(); }
            case DataType.CHARACTER: { return new CharacterRecord(); }
            case DataType.SKILL: { return new SkillRecord(); }
        }
        return null;
    }

    private IRecord GetRecord(DataType type_)
    {
        switch (type_)
        {
            case DataType.TABLE: { return this.tableRecord; }
            case DataType.RESOURCE: { return this.resourceRecord; }
            case DataType.CHARACTER: { return this.characterRecord; }
            case DataType.SKILL: { return this.skillRecord; }
        }
        return null;
    }

    private void InsertData(IRecord record_, DataType type_)
    {
        switch (type_)
        {
            case DataType.TABLE: { this.tableRecord = record_ as TableRecord; } break;
            case DataType.RESOURCE: { this.resourceRecord = record_ as ResourceRecord; } break;
            case DataType.CHARACTER: { this.characterRecord = record_ as CharacterRecord; } break;
            case DataType.SKILL: { this.skillRecord = record_ as SkillRecord; } break;
        }
    }
}
