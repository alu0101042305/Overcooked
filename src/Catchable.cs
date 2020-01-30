using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameController;

// Objetos que se pueden agarrar (ej: comida, platos...)
public class Catchable : Selectable
{
    Container container; // contenedor donde está el objeto (puede ser null)

    private void Start()
    {
        // ignora collisiones con player
        Physics.IgnoreCollision(GetComponent<Collider>(), controller.player.GetComponentInParent<Collider>(), true);
    }

    // Le asignamos a player el objeto
    // o en el caso de que player contenga un contenedor, a dicho contenedor
    override public void OnAction()
    {
        Container player = controller.player;
        if(!player.Empty())
        {
            Container cont = player.GetItem().GetComponent<Container>();
            if(cont != null && cont.Empty())
            {
                cont.SetItem(gameObject);
            }
        } else
        {
            player.SetItem(gameObject);
        }
    }

    public Container GetContainer()
    {
        return container;
    }

    public void SetContainer(Container cont)
    {
        container = cont;
    }

}
