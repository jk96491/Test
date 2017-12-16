using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUtil
{
    public static GameObject RoadPrefab(int ResourceID_)
    {
        GameObject obj = null;

        string path = GameData.Instance.resourceRecord.FindResoruceByID(ResourceID_);

        obj = Resources.Load(path) as GameObject;

        return obj;
    }
}
