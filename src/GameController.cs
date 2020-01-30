using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public Container player { get; private set; }
    public AudioSource failSound { get; private set; }
    public AudioSource doneSound { get; private set; }
    public AudioSource frieSound { get; private set; }

    public Text timerText;                              // Text of countdown timer
    public Text pointText;                              // Text points of player

    public Material selectableMaterial;
    public GameObject meat;
    public GameObject friedEgg;
    public GameObject pie;
    public GameObject cutApple;

    int countDownTime;                                  // Seconds to finish the game

    public int points;                                         // player points

    GameObject client1;                                 // Client 1
    GameObject client2;                                 // Client 2
    GameObject client3;                                 // Client 3
    GameObject client4;                                 // Client 4
    private Queue<GameObject> cola = new Queue<GameObject>();

    private void Awake()
    {
        if (controller != null)
            Destroy(controller.gameObject);
        controller = this;
        DontDestroyOnLoad(this);
        controller = this;
        player = GameObject.Find("Player").GetComponent<Container>();
        failSound = GetComponent<AudioSource>();
        doneSound = transform.Find("DoneSound").GetComponent<AudioSource>();
        frieSound = transform.Find("FrieSound").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        countDownTime = 90;


        CDTimer();

        client1 = GameObject.FindWithTag("Client1");
        client2 = GameObject.FindWithTag("Client2");
        client3 = GameObject.FindWithTag("Client3");
        client4 = GameObject.FindWithTag("Client4");

        ClientBegin();
        Invoke("RandomlyClients", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("X"))
        {
            Selectable.selected?.TriggerClick();
        }
    }

    // Calculate time to finish the game
    void CDTimer() {
        if (countDownTime > 0) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(countDownTime);
        if (countDownTime < 70 && countDownTime > 59)
          timerText.text = "0" + timeSpan.Minutes + ":0" + timeSpan.Seconds;
        else if (countDownTime >= 10)
            timerText.text = "0" + timeSpan.Minutes + ":" + timeSpan.Seconds;
        else
            timerText.text = "0" + timeSpan.Minutes + ":0" + timeSpan.Seconds;
        countDownTime--;
        Invoke("CDTimer", 1.0f);
        }
        else {
        timerText.text = "00:00";
        LoadAScene("menu");
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "menu")
        {
            enabled = false;
            CancelInvoke();
        }
    }

    //Load a Scene
    public void LoadAScene(string scene) {
      SceneManager.LoadScene(scene);
    }

    //Client activate at begin
    void ClientBegin()
    {
      client2.SetActive(false);
      client3.SetActive(false);
      client4.SetActive(false);
        cola.Enqueue(client2);
        cola.Enqueue(client3);
        cola.Enqueue(client4);
        cola.Enqueue(client1);
    }

    // Control of activating clients
    void ClientsActivate() {
      if (countDownTime >0 || points < 5) {
            for(int i = 0; i < cola.Count; i++)
            {
                if (!cola.Peek().activeSelf)
                {
                    cola.Peek().SetActive(true);
                    cola.Enqueue(cola.Dequeue());
                    return;
                }
                else
                {
                    cola.Enqueue(cola.Dequeue());
                }

            }
      }
    }

    // Invoke randomly Clients function
    void RandomlyClients()
    {
       float randomTime = UnityEngine.Random.Range(8.0f, 15.0f);
       Invoke("ClientsActivate", randomTime);
       Invoke("RandomlyClients", randomTime);
    }

    public void AddPoints(int p)
    {
      points += p;
      pointText.text = points.ToString() + "/5";
      if (PlayerWinned())
      {
        LoadAScene("menu");
      }
    }

    public bool PlayerWinned()
    {
        return points >= 5;
    }

}
