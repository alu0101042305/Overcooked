using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameController;

// Clase que implementa 3 eventos de ratón y el evento OnAction
abstract public class Selectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public static Selectable selected { get; private set; }
    Renderer[] rds; // renderers
    Material[] originalMaterials; // materiales original del gameobject
    public bool isDone { get;  private set; } // si el evento ha tenido éxito
    Selectable[] scripts; // scripts Selectable del objeto

    private void Awake()
    {
        rds = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[rds.Length];
        for(int i = 0; i < rds.Length; ++i)
        {
            originalMaterials[i] = rds[i].material;
        }
        scripts = GetComponents<Selectable>();
    }

    // Deselecciona el objeto (cambia la textura)
    void Deselect()
    {
        for (int i = 0; i < rds.Length; ++i)
        {
            rds[i].material = originalMaterials[i];
        }
    }

    // Selecciona el objeto (cambia la textura)
    void Select()
    {
        for (int i = 0; i < rds.Length; ++i)
        {
            rds[i].material = controller.selectableMaterial;
        }
    }

    // Selecciona obj y deselecciona la selección actual (con null check)
    static void Select(GameObject obj)
    {
        if(obj != selected)
        {
            selected?.Deselect();
            selected = obj.GetComponent<Selectable>();
            selected?.Select();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Select(eventData.pointerCurrentRaycast.gameObject);
    }

    // Marca el último evento OnAction como exitoso
    public void Done()
    {
        isDone = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        isDone = false;
        if(!scripts[0].isDone)
            OnAction();
        if (scripts.Length > 1) // este if es para los platos...
        {
            if (!scripts[0].isDone && !scripts[1].isDone)
            {
                controller.failSound.Play();
            }
            if (scripts[1] == this)
            {
                scripts[0].isDone = true;
                scripts[1].isDone = true;
            }
        }
        else if (!isDone)
        {
            controller.failSound.Play();
        }
    }

    // Equivalente a la acción que ocurre cuando el usuario clicka el objeto
    public void TriggerClick()
    {
        foreach(Selectable script in scripts)
        {
            script.OnClick();
        }
    }

    abstract public void OnAction();

    private void OnDestroy()
    {
        if(selected == this)
        {
            selected = null;
        }
    }

}
