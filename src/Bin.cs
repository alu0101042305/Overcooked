using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class Bin : Container
{

    public override void OnAction()
    {
        base.OnAction();
        if (!Empty())
        {
            Done();
            StartCoroutine(Eliminate());
        }

    }

    IEnumerator Eliminate()
    {
        yield return new WaitUntil(() => GetItem().transform.localPosition == realItemPosition);
        Destroy(GetItem());
    }
}
