using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameController;

// Objetos contenedores (ej: platos, encimeras, sartenes..)
public class Container : Selectable
{
    static float itemSpeed = 5; // velocidad a la que se mueven los objetos
    public GameObject item; // objeto que contiene
    public Vector3 itemPosition; // posición del item respecto al contenedor
    protected Vector3 realItemPosition; // itemPosition + la altura / 2 de item

    // evento
    public delegate void Method(GameObject obj);
    public event Method ItemOn;

    public void Start()
    {
        if(item != null)
        {
            realItemPosition = item.transform.localPosition;
        }
    }

    private void Update()
    {
        if(item != null && item.transform.localPosition != realItemPosition)
        {
            item.transform.localPosition = Vector3.MoveTowards(item.transform.localPosition, realItemPosition, Time.deltaTime * itemSpeed);
            if (item.transform.localPosition == realItemPosition)
                ItemOn?.Invoke(item);
        }
    }

    // return true si el contenedor está vacío
    public bool Empty()
    {
        return item == null;
    }

    // Asigna un objeto al contenedor
    public void SetItem(GameObject newItem)
    {
        if(newItem == gameObject)
        {
            return;
        }
        if (!Empty())
            throw new UnityException("El contenedor no estaba vacío");
        Catchable obj = newItem.GetComponent<Catchable>();
        obj.Done();
        Done();
        obj.GetContainer()?.RemoveItem();
        obj.SetContainer(this);
        item = newItem;
        item.transform.parent = gameObject.transform;
        realItemPosition = new Vector3(itemPosition.x,
                                        itemPosition.y + item.GetComponentInChildren<Renderer>().bounds.size.y / 2,
                                        itemPosition.z);
    }

    // retorna el objeto del contenedor
    public GameObject GetItem()
    {
        return item;
    }

    // borra el objeto del contenedor
    public virtual void RemoveItem()
    {
        item = null;
    }

    override public void OnAction()
    {
        if(!controller.player.Empty() && Empty())
        {
            SetItem(controller.player.GetItem());
        }
    }

}
