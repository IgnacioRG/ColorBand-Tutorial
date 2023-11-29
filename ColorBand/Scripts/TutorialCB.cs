using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialCB : MonoBehaviour
{
    //Gameobjects de tipo ui
    public GameObject paso;
    public GameObject personaje;
    public GameObject explicacion;

    //Gameobject de tipo boton
    public GameObject boton;

    //Sprites de explicacion y recursos visuales.
    public Sprite mecanica1_sp;
    public Sprite mecanica2_sp;
    public Sprite derrota_sp;
    public Sprite victoria_sp;

    private bool _sig;

    //Inicializamos parametros.
    private void Start()
    {
        _sig = false;
        boton.SetActive(true);
        boton.transform.GetChild(0).GetComponent<Text>().text = "Siguiente";
        boton.GetComponent<Button>().onClick.RemoveAllListeners();
        boton.GetComponent<Button>().onClick.AddListener(SiguientePaso);

        StartCoroutine(TutorialFlujo());
    }

    /**
     * SiguientePaso se llama cada vez que el jugador termina de leer la instruccion
     * actual (boton siguiente).
     */
    public void SiguientePaso()
    {
        _sig = true;
    }

    /**
     * Metodo para volver al menu.
     */
    public void VolverPrincipal()
    {
        SceneManager.LoadScene("CB_Menu_Inicio");
    }

    /**
     * Flujo normal del tutorial en el que se explica en 4 pasos las mecanicas del juego.
     * 1. Mecanica principal de memorizacion.
     * 2. Mecanica de segunda fase a contra tiempo.
     * 3. Condiciones de Victoria.
     * 4. Condiciones de Derrota.
     */
    IEnumerator TutorialFlujo()
    {
        explicacion.GetComponent<Text>().text = "¡Escucha y memoriza  los sonidos que cada color aporta a la Color Band!";
        paso.GetComponent<Image>().sprite = mecanica1_sp;
        while(!_sig)
        {
            yield return null;
        }
        _sig = false;

        explicacion.GetComponent<Text>().text = "Escucha el sonido y selecciona a qué color corresponde antes de que se acabe el tiempo.";
        paso.GetComponent<Image>().sprite = mecanica2_sp;
        while (!_sig)
        {
            yield return null;
        }
        _sig = false;

        explicacion.GetComponent<Text>().text = "¡Logra un buen puntaje \n(5 aciertos)  y sube de nivel!";
        paso.GetComponent<Image>().sprite = victoria_sp;
        while (!_sig)
        {
            yield return null;
        }
        _sig = false;

        explicacion.GetComponent<Text>().text = "Si te equivocas, bajaras de nivel \n(4 errores).";
        paso.GetComponent<Image>().sprite = derrota_sp;

        boton.transform.GetChild(0).GetComponent<Text>().text = "Comenzar";
        boton.GetComponent<Button>().onClick.RemoveAllListeners();
        boton.GetComponent<Button>().onClick.AddListener(VolverPrincipal);
    }
}
