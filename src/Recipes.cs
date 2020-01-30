using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
{

    /*
      INGREDIENTES
        -Manzana
        -Huevo
        -Carne cruda

      PLATOS
        -Pastel de manzana
          -Manzana
          -Huevo
        -Bistec
          -Carne cruda
        -Huevo frito
          -Huevo
    */

    public Recipe[] foods;

}

[System.Serializable]
public class Recipe
{
    public Sprite img; // imagen
    public GameObject prefab; // prefab de la comida
}
