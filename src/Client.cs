using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

// script que define el comportamiento de los clientes
public class Client : MonoBehaviour
{
    Image img; // referencia a la imagen del bocadillo
    Recipe food; // comida asignada actualmente al cliente
    Recipe[] foods = null; // referencia al array con las recetas
    private Animator anim;

    [SerializeField]
    Container cont;

    private void Awake()
    {
        img = transform.Find("Canvas").GetComponent<Image>();
        foods = GameObject.Find("GameController").GetComponent<Recipes>().foods;
        cont.ItemOn += OnItemOn;
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SetRecipe(foods[Random.Range(0, foods.Length)]);
    }

    void OnItemOn(GameObject obj)
    {
        if(obj.CompareTag("Plate"))
        {
            Container plate = obj.GetComponent<Container>();
            if(!plate.Empty())
            {
                GameObject foodOnPlate = plate.GetItem();
                if(foodOnPlate.CompareTag(food.prefab.tag))
                {
                    if(foodOnPlate.tag == "Pie")
                    {
                        cont.RemoveItem();
                        Destroy(plate.gameObject);
                        OrderCompleted(2);
                    } else
                    {
                        cont.RemoveItem();
                        Destroy(plate.gameObject);
                        OrderCompleted(1);
                    }
                } else
                {
                    // TODO no es la comida que esperaba el cliente (o no estaba en un plato)
                }
            }
        }
    }

    // Le asigna una receta al cliente
    void SetRecipe(Recipe food)
    {
        this.food = food;
        img.sprite = food.img;
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }
    //When a order is completed
    void OrderCompleted(int points) {
        controller.AddPoints(points);
        anim.SetBool("finish", true);
    }
}
