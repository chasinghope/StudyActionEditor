using UnityEngine;

public class Test : MonoBehaviour
{
    public BuffHandler buffHandler;
    public BuffData buffData;
    public BuffData buffData2;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("press down space");
            buffHandler.AddBuff(new BuffInfo() { buffData = buffData, target = gameObject });
        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("press A space");
            buffHandler.AddBuff(new BuffInfo() { buffData = buffData2, target = gameObject, creator = gameObject });
        }

    }
}