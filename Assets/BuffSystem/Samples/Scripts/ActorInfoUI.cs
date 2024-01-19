using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActorInfoUI : MonoBehaviour
{
    public Actor actor;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI ack;
    public TextMeshProUGUI speed;



    public void Start()
    {
        actor.actorInfoUI = this;
        Refresh();
    }

    public void Refresh()
    {
        this.hp.text = "Hp: " + actor.property.hp.ToString();
        this.ack.text = "Ack: " + actor.property.ack.ToString();
        this.speed.text = "Speed: " + actor.property.speed.ToString();
    }
}
