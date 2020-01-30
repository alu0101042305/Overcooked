
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;
public class CuttingTable : Container
{

    TimerBar bar;

    private Animator anim;
    private GameObject player;

    private const string animBoolName = "prepare";
    private const string animBoolName2 = "cut";

    void Start()
    {
        base.Start();
        bar = transform.Find("Bar").GetComponent<TimerBar>();
        bar.End += OnEnd;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }


    void OnEnd()
    {
        GameObject item = GetItem();
        GameObject newItem;
        RemoveItem();
        newItem = Instantiate(controller.cutApple);


        SetItem(newItem);
        newItem.transform.localPosition = item.transform.localPosition;
        Destroy(item);
    }

    public override void OnAction()
    {
        base.OnAction();

    }
    
    

    void OnTriggerStay(Collider other)
    {
        if (!Empty())
        {

            if (GetItem().CompareTag("Apple"))
            {
                if (other.gameObject == player)
                {
                    bool isOpen = anim.GetBool(animBoolName);
                    anim.enabled = true;
                    anim.SetBool(animBoolName, true);
                }
                if (Input.GetButtonDown("Circle")||Input.GetKeyDown(KeyCode.E))
                {
                    if (!bar.IsPlaying())
                    {
                        bar.Play();
                        GetItem().GetComponent<Catchable>().enabled = false;
                    }
                    else
                        bar.Continue();
                    anim.SetBool(animBoolName2, true);

                }
                if (Input.GetButtonUp("Circle") || Input.GetKeyUp(KeyCode.E))
                {
                    anim.SetBool(animBoolName2, false);
                    if (bar.IsPlaying())
                        bar.Stop();
                }
            }
            else
            {
                anim.SetBool(animBoolName, false);
                anim.SetBool(animBoolName2, false);
                anim.SetBool(animBoolName2, false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player){			
            anim.SetBool(animBoolName, false);
            anim.SetBool(animBoolName2, false);
            if (bar.IsPlaying())
                bar.Stop();
        }
    }
    
}