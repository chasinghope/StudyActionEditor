using UnityEngine;

public class Actor : MonoBehaviour
{
    public Property property;


    public bool IsCanBekill(DamageInfo damageInfo)
    {
        property.hp -= damageInfo.damage;
        if(property.hp <= 0)
        {
            return true;
        }
        return false;
    }
}