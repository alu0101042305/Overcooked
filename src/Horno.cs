using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

// Scripts que controla el comportamiento del horno
// Cuando contenga un huevo y una manzana cortada inicia la barra de carga
// y crea un objeto tarta al finalizar
public class Horno : Container
{

    TimerBar bar;
    bool ended = false;
    bool hasEgg = false;
    bool hasApple = false;
    private Light ovenLight;

    
    void Start()
    {
        base.Start();
        bar = transform.Find("Bar").GetComponent<TimerBar>();
        bar.End += OnEnd;
        ovenLight = transform.Find("Light").GetComponent<Light>();
        ItemOn += OnItemOn;
    }

    void OnEnd()
    {
        GameObject pie = Instantiate(controller.pie);
        SetItem(pie);
        //Physics.IgnoreCollision(GetComponent<Collider>(), pie.GetComponentInChildren<Collider>(), true);
        pie.transform.localPosition = realItemPosition;
        ended = true;
        ovenLight.enabled = false;
    }

    void OnItemOn(GameObject obj)
    {
        print("eo");
        if(!obj.CompareTag("Pie"))
        {
            RemoveItem();
            Destroy(obj);
        }
    }

    public override void OnAction()
    {
        GameObject obj = controller.player.GetItem();
        if(obj != null)
        {
            if (obj.CompareTag("Egg") && !hasEgg)
            {
                hasEgg = true;
                SetItem(obj);
            }
            else if (obj.CompareTag("CutApple") && !hasApple)
            {
                hasApple = true;
                SetItem(obj);
            }
        }

        if (ended)
        {
            Done();
            Selectable pie = GetItem().GetComponent<Selectable>();
            pie.TriggerClick();
            if (pie.isDone)
            {
                ended = false;
                hasEgg = false;
                hasApple = false;
            }
        } else
        {
            if (hasApple && hasEgg && !bar.IsPlaying())
            {
                bar.Play();
                ovenLight.enabled = true;
            }
        }

    }
    
}
