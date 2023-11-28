using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColorBand
{
    public class MenuInicio : MonoBehaviour
    {
        public GameObject botonSalir, menuSalir;
        public string principal;
        public static int Nivel = 1;

        public void Cambia_nivel(int i)
        {
            SceneManager.LoadScene(principal);
            Nivel = i;
        }

        //private void OnMouseDown()
        //{
        //    Cambia_nivel(string);
        //}

        public void Salir()         //Esto es para el botón con una X
        {
            Time.timeScale = 0f;
            botonSalir.SetActive(false);
            menuSalir.SetActive(true);
        }

        public void ConfirmaSalir()     //Esto es para el botón de sí en el menu de salir
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
            botonSalir.SetActive(true);
            //Application.Quit();
            SceneManager.LoadScene("Menu Juegos");
        }

        public void Reanudar()
        {
            Time.timeScale = 1f;
            botonSalir.SetActive(true);
            menuSalir.SetActive(false);
        }

        public void GoesTutorial()
        {
            SceneManager.LoadScene("CB_Tutorial");
        }
    }
}
