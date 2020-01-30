using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameController;


/*
/ Controller: controller from the menu scene
*/

public class Controller : MonoBehaviour
{

    public Text menuText;     //Text from menu
    Button selected = null; // botón seleccionado actualmente (puede ser null)

    // Start is called before the first frame update
    void Start()
    {
        if(controller == null)
        {
            menuText.text = "Pulsa en Nueva Partida para jugar";
        } else if(controller.PlayerWinned())
        {
            menuText.text = "¡Has Ganado!";
        } else
        {
            menuText.text = "Has Perdido";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Exit();
        if(Input.GetButtonDown("X") || Input.GetKeyDown(KeyCode.X))
        {
            selected?.onClick.Invoke();
        }
    }

    public void ButtonEnter(Button button)
    {
        selected = button;
    }

    public void ButtonExited(Button button)
    {
        selected = null;
    }

    //Load a Scene
    public void LoadAScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    //EXIT APP
    public void Exit()
	{
        Application.Quit();      
	}

}
