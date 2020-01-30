using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

// Script que controla el comportamiento de la sartén.
// Permite freir huevos y carne cruda.
public class Sartén : Container
{

    TimerBar bar;

    ParticleSystem ps;

    void Start()
    {
        base.Start();
        bar = transform.Find("Bar").GetComponent<TimerBar>();
        bar.End += OnEnd;
        ps = GameObject.FindWithTag("smoke").GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;
    }

    void OnEnd()
    {
        controller.frieSound.Stop();
        var em = ps.emission;
        em.enabled = false;
        //return;
        GameObject item = GetItem();
        GameObject newItem;
        RemoveItem();
        if(item.CompareTag("Egg"))
        {
            newItem = Instantiate(controller.friedEgg);
            
        } 
        else
        {
            newItem = Instantiate(controller.meat);
        }
        
        SetItem(newItem);
        newItem.transform.localPosition = item.transform.localPosition;
        Destroy(item);
    }

    public override void OnAction()
    {
        GameObject obj = controller.player.GetItem();
        if(obj ? (obj.CompareTag("Egg") || obj.CompareTag("RawMeat")) : false)
        {
            base.OnAction();
            if(GetItem() == obj)
            {
                obj.GetComponent<Catchable>().enabled = false;
                bar.Play();
                controller.frieSound.Play();
                var em = ps.emission;
                em.enabled = true;
            }
        }
    }
}
