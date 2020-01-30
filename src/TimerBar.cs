using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

// Script para el objeto Bar que funciona como una barra de tiempo.
// Contiene un evento ("End") que se dispara cuando la barra termina de cargarse
public class TimerBar : MonoBehaviour
{
    public delegate void Method();
    public event Method End;
    public float time = 10;
    bool active;
    Image bar;

    void Start()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        gameObject.SetActive(false);
        End += OnEnd;
        active = false;
    }

    void Update()
    {
        if (active)
        {
            bar.fillAmount += Time.deltaTime / time;
            if (bar.fillAmount >= 1)
            {
                gameObject.SetActive(false);
                End.Invoke();
            }
        }
    }

    void OnEnd()
    {
        controller.doneSound.Play();
        active = false;
    }

    public bool IsPlaying()
    {
        return gameObject.activeSelf;
    }

    public void Play()
    {
        active = true;
        gameObject.SetActive(true);
        bar.fillAmount = 0;
    }

    public void Continue()
    {
        active = true;
    }

    public void Stop()
    {
        active = false;
    }

    public void Down()
    {
        active = false;
        gameObject.SetActive(false);
        bar.fillAmount = 0;
    }

    public bool HasStarted()
    {
        if (bar.fillAmount == 0)
            return false;
        else
            return true;
    }

}
