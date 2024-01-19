using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private static DamageManager instance;
    public static DamageManager Instance => instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }


    public void SubmitDamage(DamageInfo damageInfo)
    {
        BuffHandler creatorHandler = damageInfo.creator.GetComponent<BuffHandler>();
        BuffHandler targetHandler = damageInfo.target.GetComponent<BuffHandler>();

        if(creatorHandler)
        {
            foreach (var buffInfo in creatorHandler.buffList)
            {
                buffInfo.buffData.OnHit?.Apply(buffInfo, damageInfo);
            }
        }

        if (targetHandler)
        {
            foreach (var buffInfo in targetHandler.buffList)
            {
                buffInfo.buffData.OnBehurt?.Apply(buffInfo, damageInfo);
            }

            Actor actor = damageInfo.target.GetComponent<Actor>();
            if(actor != null)
            {
                if(actor.IsCanBekill(damageInfo))
                {
                    foreach (var buffInfo in targetHandler.buffList)
                    {
                        buffInfo.buffData.OnBekill?.Apply(buffInfo, damageInfo);
                    }

                    if (creatorHandler)
                    {
                        foreach (var buffInfo in creatorHandler.buffList)
                        {
                            buffInfo.buffData.OnKill?.Apply(buffInfo, damageInfo);
                        }
                    }

                    //if(actor.IsCanBekill(damageInfo))
                    //{

                    //}

                }
            }

        }
    }

}