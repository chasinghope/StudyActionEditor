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
            BuffInfo buffInfo = BuffManager.instance.GetBuffByName("SpeedDown", gameObject, gameObject);
            buffHandler.AddBuff(buffInfo);
            //buffHandler.AddBuff(new BuffInfo() { buffData = buffData, target = gameObject });
        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("press A space");
            BuffInfo buffInfo = BuffManager.instance.GetBuffByName("SpeedUp", gameObject, gameObject);
            buffHandler.AddBuff(buffInfo);
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("press B space");
            BuffInfo buffInfo = BuffManager.instance.GetBuffByName("CreateCube", gameObject, gameObject);
            buffHandler.AddBuff(buffInfo);
        }
    }
}