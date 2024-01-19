using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnGOBM", menuName = "BuffSystem/SpawnGOBM")]
public class SpawnGOBM : BaseBuffModule
{
    public GameObject prefab;
    public Vector3 position;

    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.position = position;
    }
}