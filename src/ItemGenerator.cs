using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contenedor que genera continuamente un objeto cada vez que el jugador lo coje
public class ItemGenerator : Container
{

    public GameObject obj;

    new void Start()
    {
        base.Start();
        SetObject();
    }

    override public void RemoveItem()
    {
        base.RemoveItem();
        SetObject();
    }

    void SetObject()
    {
        GameObject item = Instantiate(obj);
        SetItem(item);
        item.transform.localPosition = realItemPosition;
    }
    
}
