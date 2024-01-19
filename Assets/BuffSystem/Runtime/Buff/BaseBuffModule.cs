using UnityEngine;

public abstract class BaseBuffModule : ScriptableObject
{
    public abstract void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null);
}