using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_ChangePropertyBM", menuName = "BuffSystem/ChangePropertyBM")]
public class ChangePropertyBM : BaseBuffModule
{
    public Property property;
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        Actor actor = buffInfo.target.GetComponent<Actor>();
        if(actor != null)
        {
            actor.property.hp += property.hp;
            actor.property.ack += property.ack;
            actor.property.speed += property.speed;
        }
    }
}
