using System;
using System.Collections.Generic;
using UnityEngine;

public enum BuffUpdateTimeEnum
{
    Add,
    Replace,
    Keep,
}


public enum BuffRemoveStackUpdateEnum
{
    Clear,
    Reduce,
}

public class BuffInfo
{
    public BuffData buffData;
    public GameObject creator;
    public GameObject target;
    public float durationTimer;
    public float tickTimer;
    public int curStack = 1;
}

public class DamageInfo
{
    public GameObject creator;
    public GameObject target;
    public float damage;
}

[System.Serializable]
public class Property
{
    public float hp;
    public float speed;
    public float ack;
}



public class BuffInfoPool
{
    public static Stack<BuffInfo> pool = new Stack<BuffInfo>();
    public static BuffInfo Get(BuffData buffData)
    {
        if(pool.Count > 0)
        {
            BuffInfo buffInfo = pool.Pop();
            buffInfo.buffData = buffData;
            buffInfo.creator = null;
            buffInfo.target = null;
            buffInfo.durationTimer = 0;
            buffInfo.tickTimer = 0;
            buffInfo.curStack = 1;
            return buffInfo;
        }
        else
        {
            return new BuffInfo() { buffData = buffData };
        }
    }

    public static void Release(BuffInfo buffInfo)
    {
        buffInfo.buffData = null;
        pool.Push(buffInfo);
    }

}