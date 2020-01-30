using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

//Clase que establece la papelera, que se usa para eliminar objetos cogidos

public class Bin : Container
{

    // Cuando se hace click procede a eliminar el objeto
    public override void OnAction()
    {
        base.OnAction();
        if (!Empty())
        {
            Done();
            StartCoroutine(Eliminate());
        }

    }

    // Elimina el objeto que se le pasa a la papelera
    IEnumerator Eliminate()
    {
        yield return new WaitUntil(() => GetItem().transform.localPosition == realItemPosition);
        Destroy(GetItem());
    }
}
