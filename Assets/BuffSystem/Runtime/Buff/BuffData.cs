using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_BuffData", menuName = "BuffSystem/BuffData", order = 1)]
public class BuffData : ScriptableObject
{
    // ������Ϣ
    public int id;
    public string buffName;
    public string description;
    public Sprite icon;
    public int priority;
    public int maxStack;
    public string[] tags;

    //ʱ����Ϣ
    public bool isForever;
	public float duration;
	public float tickTime;

	//���·�ʽ
	public BuffUpdateTimeEnum buffUpdateTime;
	public BuffRemoveStackUpdateEnum buffRemoveStackUpdate;

    //�ص���
    public BaseBuffModule OnCreate;
    public BaseBuffModule OnRemove;
    public BaseBuffModule OnTick;

    // -- �˺��ص���
    public BaseBuffModule OnHit;
    public BaseBuffModule OnBehurt;
    public BaseBuffModule OnKill;
    public BaseBuffModule OnBekill;
}
