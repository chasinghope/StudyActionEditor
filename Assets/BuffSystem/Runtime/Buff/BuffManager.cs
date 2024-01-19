using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;

    public Dictionary<string, BuffData> buffDict = new Dictionary<string, BuffData>();

    public void Awake()
    {
        if(instance == null)
            instance = this;
        LoadBuffData();

    }


    public void LoadBuffData()
    {
#if UNITY_EDITOR
        string[] objGuids = AssetDatabase.FindAssets("t:BuffData", new string[] { "Assets/BuffSystem/Runtime/Data/BuffDataSO" });
        
        foreach(string guid in objGuids)
        {
            BuffData data = AssetDatabase.LoadAssetAtPath<BuffData>(AssetDatabase.GUIDToAssetPath(guid));
            if (!buffDict.TryAdd(data.buffName, data))
            {
                Debug.Log("buff Ãû³ÆÖØ¸´");
            }
        }

#endif
    }

    public BuffInfo GetBuffByName(string name, GameObject creator, GameObject target)
    {
        BuffInfo buffInfo = BuffInfoPool.Get(buffDict[name]);
        buffInfo.creator = creator;
        buffInfo.target = target;
        return buffInfo;
    }
}
