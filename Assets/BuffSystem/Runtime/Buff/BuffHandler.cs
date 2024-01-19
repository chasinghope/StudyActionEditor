using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BuffHandler : MonoBehaviour
{
    public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>();


    private void Update()
    {
        BuffTickAndRemove();
    }

    private void BuffTickAndRemove()
    {
        List<BuffInfo> deleteBuffList = ListPool<BuffInfo>.Get();
        var enumrator = buffList.GetEnumerator();
        while (enumrator.MoveNext())
        {
            BuffInfo buffInfo = enumrator.Current;

            // Tick
            if (buffInfo.buffData.OnTick != null)
            {
                if (buffInfo.tickTimer < 0)
                {
                    buffInfo.buffData.OnTick.Apply(buffInfo);
                    buffInfo.tickTimer = buffInfo.buffData.tickTime;
                }
                else
                {
                    buffInfo.tickTimer -= Time.deltaTime;
                }
            }


            // Remove
            if(!buffInfo.buffData.isForever)
            {
                if (buffInfo.durationTimer < 0)
                {
                    deleteBuffList.Add(buffInfo);
                }
                else
                {
                    buffInfo.durationTimer -= Time.deltaTime;
                }
            }

        }

        foreach (var buffInfo in deleteBuffList)
        {
            RemoveBuff(buffInfo);
        }

        ListPool<BuffInfo>.Release(deleteBuffList);

    }

    public void AddBuff(BuffInfo buffInfo)
    {
        BuffInfo findBuffInfo = FindBuff(buffInfo.buffData.id);
        if (findBuffInfo != null)
        {
            if(findBuffInfo.curStack < findBuffInfo.buffData.maxStack)
            {
                findBuffInfo.curStack++;
                switch (findBuffInfo.buffData.buffUpdateTime)
                {
                    case BuffUpdateTimeEnum.Add:
                        findBuffInfo.durationTimer += findBuffInfo.buffData.duration;
                        break;
                    case BuffUpdateTimeEnum.Replace:
                        findBuffInfo.durationTimer = findBuffInfo.buffData.duration;
                        break;
                    case BuffUpdateTimeEnum.Keep:
                        break;
                    default:
                        break;
                }
                findBuffInfo.buffData?.OnCreate.Apply(findBuffInfo);
            }
            else
            {
                Debug.Log("超出最大层");
            }
        }
        else
        {
            buffInfo.durationTimer = buffInfo.buffData.duration;
            buffInfo.buffData?.OnCreate.Apply(buffInfo);
            buffList.AddLast(buffInfo);

            // 对buffList 排序
            InsertionSort(buffList);
        }
    }


    public void RemoveBuff(BuffInfo buffInfo)
    {
        switch (buffInfo.buffData.buffRemoveStackUpdate)
        {
            case BuffRemoveStackUpdateEnum.Clear:
                buffInfo.buffData?.OnRemove.Apply(buffInfo);
                buffList.Remove(buffInfo);
                break;
            case BuffRemoveStackUpdateEnum.Reduce:
                buffInfo.curStack--;
                buffInfo.buffData?.OnRemove.Apply(buffInfo);
                if (buffInfo.curStack == 0)
                {
                    buffList.Remove(buffInfo);
                }
                else
                {
                    buffInfo.durationTimer = buffInfo.buffData.duration;
                }

                break;
            default:
                break;
        }
    }




    private BuffInfo FindBuff(int buffId)
    {
        foreach (BuffInfo buffInfo in buffList)
        {
            if(buffInfo.buffData.id == buffId)
                return buffInfo;
        }
        return null;
    }



    static void InsertionSort(LinkedList<BuffInfo> list)
    {
        if (list == null || list.First == null)
            return;

        LinkedListNode<BuffInfo> current = list.First.Next;

        while (current != null)
        {
            LinkedListNode<BuffInfo> next = current.Next;
            LinkedListNode<BuffInfo> prev = current.Previous;

            while(prev != null && prev.Value.buffData.priority > current.Value.buffData.priority)
            {
                prev = prev.Previous;
            }

            if(prev == null)
            {
                list.Remove(current);
                list.AddFirst(current);
            }
            else
            {
                list.Remove(current);
                list.AddAfter(prev, current);
            }

            current = next;
        }
    }
}