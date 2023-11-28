using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ColorBand
{
    public class AdminColorBand : MonoBehaviour
    {
        public GameObject PanelFaseMemorizar, PanelIntermedio, PanelFaseJugar;
        //[SerializeField] 
        //Variables de objetos para controlar que el boton de pausa desaparezca cuando salga el menu y viceversa
        public GameObject botonPausa, menuPausa, menuSalir, botonSalir, mensajeIncorrecto, mensajeCorrecto1, mensajeCorrecto2, mensajeSubeNivel1, mensajeSubeNivel2, mensajeBajaNivel, mensajeMismoNivel;
        public AudioClip[] sonidos;                                                         //Arreglo para guardar las canciones
        public AudioSource emisorSonido;
        public AudioClip[] contador;
        private AudioClip _clipQueSeEstaRepitiendo;
        private int _numRepeticionesDelClip = 0;
        public enum Colores { Rojo, Azul, Amarillo, Verde, Morado, Naranja }
        public List<AudioClip> sonidosAMemorizar = new List<AudioClip>();
        public int nivel;
        public Text nivel_text;
        public Text aciertos_text;
        public Text fallos_text;

        public Dictionary<string, AudioClip> diccionarioBotones = new Dictionary<string, AudioClip>();
        public Button boton1, boton2, boton3, boton4, boton5, boton6;
        public Image jack;
        //public Image _saxofonista, _baterista, _rapero, _cantante, _guitarrista;            //Hacerlo en un arreglo y que de acuerdo al numero aleatorio poner un setactive para uno de los monitos
        public GameObject[] miembrosBanda;
        public Image franjaLimite;
        private Animator franjaLimite_anim;

        [Tooltip("Se usa para emitir los sonidos de aciertos, fallos, subes de nivel o bajas de nivel")]
        public AudioSource bocina;
        public AudioClip victoria_audioClip;
        public AudioClip derrota_audioClip;
        public AudioClip subesNivel_audioClip;
        public AudioClip bajasNivel_audioClip;

        public GameObject botonesFase1Nivel1;
        public GameObject botonesFase1Nivel2A;
        public GameObject botonesFase1Nivel2B;
        public GameObject botonesFase1Nivel2C;
        public GameObject botonesFase1Nivel3;
        public GameObject botonesFase1Nivel4A;
        public GameObject botonesFase1Nivel4B;
        public GameObject botonesFase1Nivel4C;
        public GameObject botonesFase1Nivel4D;
        public GameObject botonesFase1Nivel5;
        public GameObject botonesFase1Nivel6A;
        public GameObject botonesFase1Nivel6B;
        public GameObject botonesFase1Nivel6C;
        public GameObject botonesFase1Nivel6D;
        public GameObject botonesFase1Nivel6E;
        public GameObject botonesFase1Nivel7A;
        public GameObject botonesFase1Nivel7B;
        public GameObject botonesFase1Nivel7C;
        public GameObject botonesFase1Nivel7D;
        public GameObject botonesFase1Nivel7E;
        public GameObject botonesFase1Nivel7F;
        public GameObject botonesFase1Nivel8;
        public GameObject botonesFase1Nivel9A;
        public GameObject botonesFase1Nivel9B;
        public GameObject botonesFase1Nivel9C;
        public GameObject botonesFase1Nivel9D;
        public GameObject botonesFase1Nivel9E;
        public GameObject botonesFase1Nivel9F;
        public GameObject botonesFase1Nivel10A;
        public GameObject botonesFase1Nivel10B;
        public GameObject botonesFase1Nivel10C;
        public GameObject botonesFase1Nivel10D;
        public GameObject botonesFase1Nivel10E;
        public GameObject botonesFase1Nivel10F;

        public GameObject botonesFase2Nivel1;
        public GameObject botonesFase2Nivel2;
        public GameObject botonesFase2Nivel3;
        public GameObject botonesFase2Nivel4;
        public GameObject botonesFase2Nivel5;
        public GameObject botonesFase2Nivel6;
        public GameObject botonesFase2Nivel7;
        public GameObject botonesFase2Nivel8;
        public GameObject botonesFase2Nivel9;
        public GameObject botonesFase2Nivel10;

        public GameObject bloqueaClicks_panel;

        [SerializeField]
        int _NeurocoinsRecibidasAlGanar = 5;
        [SerializeField]
        int _NeurocoinsRecibidasAlSubirDeNivel = 20;

        public GameObject mascota;
        public GameObject coins_panel;
        public Text coins_text;

        private AudioClip sonidoFaseJugar;                                                //Se declaro como variable privada para que la función activafase2 pueda acceder a ella tambien

        private int _numRespuestasDadas = 0;
        //private int _numMaxAciertosConsecutivos = 3;
        //private int _numMaxFallosConsecutivos = 3;
        private int _numMaxRespuestas = 6;
        private int _numAciertos = 0;
        private int _numFallos = 0;
        public string menuInicio;

        public static bool seAgotoTiempoDeRespuesta = false;

        Coroutine co;

        void Awake()
        {
            PanelFaseMemorizar.SetActive(true);
            PanelFaseJugar.SetActive(false);
            PanelIntermedio.SetActive(false);
            aciertos_text.text = "Aciertos: 0/ 6";
            fallos_text.text = "Fallos: 0";
            franjaLimite_anim = franjaLimite.GetComponent<Animator>();
            franjaLimite_anim.SetBool("Reproduce", false);
//#if UNITY_STANDALONE_WIN
            nivel = MenuInicio.Nivel;
//#endif
            nivel_text.text = "Nivel: " + nivel;
            PreparaNivel();
        }

        void ActivaMiembroBanda()
        {
            int dado = Random.Range(0, 5);
            for (int i = 0; i < miembrosBanda.Length; i++)
            {
                miembrosBanda[i].SetActive(false);
            }
            miembrosBanda[dado].SetActive(true);
            jack = miembrosBanda[dado].GetComponent<Image>();
        }

        void PreparaNivel()
        {
            ActivaMiembroBanda();
            diccionarioBotones.Clear();
            DesactivaTodosLosBotonesFase1();
            DesactivaTodosLosBotonesFase2();
            ReseteaMarcador();
            menuSalir.SetActive(false);
            coins_panel.SetActive(false);
            mensajeBajaNivel.SetActive(false);
            mensajeSubeNivel1.SetActive(false);
            mensajeSubeNivel2.SetActive(false);
            mensajeMismoNivel.SetActive(false);
            PanelFaseMemorizar.SetActive(true);
            PanelIntermedio.SetActive(false);
            PanelFaseJugar.SetActive(false);
            franjaLimite.gameObject.SetActive(false);
            bloqueaClicks_panel.SetActive(false);
            switch (nivel)
            {
                case 1:
                    DefineSonidosDelNivel(2);
                    botonesFase1Nivel1.SetActive(true);
                    botonesFase2Nivel1.SetActive(true);
                    diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                    diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                    break;
                case 2:
                    DefineSonidosDelNivel(2);
                    int dadoColores = Random.Range(0, 3);               //Recordemos que como hay un distractor se hicieron diferente combinaciones de botones en memorizar, el if hace que de acuerdo
                    if (dadoColores == 0)                               //al random se active uno de los paneles con las combinaciones
                    {
                        botonesFase1Nivel2A.SetActive(true);
                        botonesFase2Nivel2.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                    }
                    else if (dadoColores == 1)
                    {
                        botonesFase1Nivel2B.SetActive(true);
                        botonesFase2Nivel2.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                    }
                    else
                    {
                        botonesFase1Nivel2C.SetActive(true);
                        botonesFase2Nivel2.SetActive(true);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                    }
                    break;
                case 3:
                    DefineSonidosDelNivel(3);
                    botonesFase1Nivel3.SetActive(true);
                    botonesFase2Nivel3.SetActive(true);
                    diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                    diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                    diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                    break;
                case 4:
                    DefineSonidosDelNivel(3);
                    dadoColores = Random.Range(0, 4);
                    if (dadoColores == 0)
                    {
                        botonesFase1Nivel4A.SetActive(true);
                        botonesFase2Nivel4.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                    }
                    else if (dadoColores == 1)
                    {
                        botonesFase1Nivel4B.SetActive(true);
                        botonesFase2Nivel4.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                    }
                    else if (dadoColores == 2)
                    {
                        botonesFase1Nivel4C.SetActive(true);
                        botonesFase2Nivel4.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                    }
                    else
                    {
                        botonesFase1Nivel4D.SetActive(true);
                        botonesFase2Nivel4.SetActive(true);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                    }
                    break;
                case 5:
                    DefineSonidosDelNivel(4);
                    botonesFase1Nivel5.SetActive(true);
                    botonesFase2Nivel5.SetActive(true);
                    diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                    diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                    diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                    diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                    break;
                case 6:
                    DefineSonidosDelNivel(4);
                    dadoColores = Random.Range(0, 5);
                    if (dadoColores == 0)
                    {
                        botonesFase1Nivel6A.SetActive(true);
                        botonesFase2Nivel6.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 1)
                    {
                        botonesFase1Nivel6B.SetActive(true);
                        botonesFase2Nivel6.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 2)
                    {
                        botonesFase1Nivel6C.SetActive(true);
                        botonesFase2Nivel6.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 3)
                    {
                        botonesFase1Nivel6D.SetActive(true);
                        botonesFase2Nivel6.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                    }
                    else
                    {
                        botonesFase1Nivel6E.SetActive(true);
                        botonesFase2Nivel6.SetActive(true);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                    }
                    break;
                case 7:
                    DefineSonidosDelNivel(4);
                    dadoColores = Random.Range(0, 6);
                    if (dadoColores == 0)
                    {
                        botonesFase1Nivel7A.SetActive(true);
                        botonesFase2Nivel7.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 1)
                    {
                        botonesFase1Nivel7B.SetActive(true);
                        botonesFase2Nivel7.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 2)
                    {
                        botonesFase1Nivel7C.SetActive(true);
                        botonesFase2Nivel7.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 3)
                    {
                        botonesFase1Nivel7D.SetActive(true);
                        botonesFase2Nivel7.SetActive(true);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[3]);
                    }
                    else if (dadoColores == 4)
                    {
                        botonesFase1Nivel7E.SetActive(true);
                        botonesFase2Nivel7.SetActive(true);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[3]);
                    }
                    else
                    {
                        botonesFase1Nivel7F.SetActive(true);
                        botonesFase2Nivel7.SetActive(true);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[3]);
                    }
                    break;
                case 8:
                    DefineSonidosDelNivel(5);
                    botonesFase1Nivel8.SetActive(true);
                    botonesFase2Nivel8.SetActive(true);
                    diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                    diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                    diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                    diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                    diccionarioBotones.Add("Azul", sonidosAMemorizar[4]);
                    break;
                case 9:
                    DefineSonidosDelNivel(5);
                    dadoColores = Random.Range(0, 6);
                    if (dadoColores == 0)
                    {
                        botonesFase1Nivel9A.SetActive(true);
                        botonesFase2Nivel9.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 1)
                    {
                        botonesFase1Nivel9B.SetActive(true);
                        botonesFase2Nivel9.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 2)
                    {
                        botonesFase1Nivel9C.SetActive(true);
                        botonesFase2Nivel9.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 3)
                    {
                        botonesFase1Nivel9D.SetActive(true);
                        botonesFase2Nivel9.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 4)
                    {
                        botonesFase1Nivel9E.SetActive(true);
                        botonesFase2Nivel9.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else
                    {
                        botonesFase1Nivel9F.SetActive(true);
                        botonesFase2Nivel9.SetActive(true);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    break;
                case 10:
                    DefineSonidosDelNivel(5);
                    dadoColores = Random.Range(0, 6);
                    if (dadoColores == 0)
                    {
                        botonesFase1Nivel10A.SetActive(true);
                        botonesFase2Nivel10.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 1)
                    {
                        botonesFase1Nivel10B.SetActive(true);
                        botonesFase2Nivel10.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 2)
                    {
                        botonesFase1Nivel10C.SetActive(true);
                        botonesFase2Nivel10.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 3)
                    {
                        botonesFase1Nivel10D.SetActive(true);
                        botonesFase2Nivel10.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else if (dadoColores == 4)
                    {
                        botonesFase1Nivel10E.SetActive(true);
                        botonesFase2Nivel10.SetActive(true);
                        diccionarioBotones.Add("Rojo", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    else
                    {
                        botonesFase1Nivel10F.SetActive(true);
                        botonesFase2Nivel10.SetActive(true);
                        diccionarioBotones.Add("Naranja", sonidosAMemorizar[0]);
                        diccionarioBotones.Add("Amarillo", sonidosAMemorizar[1]);
                        diccionarioBotones.Add("Verde", sonidosAMemorizar[2]);
                        diccionarioBotones.Add("Azul", sonidosAMemorizar[3]);
                        diccionarioBotones.Add("Morado", sonidosAMemorizar[4]);
                    }
                    break;
                default:
                    break;
            }
        }

        public void AvanzaPanelIntermedio()
        {
            StartCoroutine(ActivaPanelIntermedio());

        }

        IEnumerator ActivaPanelIntermedio()             //Corrutina para que aparezca el panel2 durante 3 segundos
        {
            emisorSonido.Stop();            //Asi no se quedará ningun sonido reproduciendose de la etapa 1
            PanelFaseMemorizar.SetActive(false);
            PanelIntermedio.SetActive(true);
            yield return new WaitForSeconds(3f);
            ActivaFase2();
        }

        void ActivaFase2()
        {
            EligeSonidoAleatorio();
            bloqueaClicks_panel.SetActive(false);
            PanelIntermedio.SetActive(false);
            PanelFaseJugar.SetActive(true);
            franjaLimite.gameObject.SetActive(true);
            emisorSonido.clip = sonidoFaseJugar;
            if (_clipQueSeEstaRepitiendo == sonidoFaseJugar)
            {
                _numRepeticionesDelClip += 1;
            }
            else
            {
                _clipQueSeEstaRepitiendo = sonidoFaseJugar;
                _numRepeticionesDelClip = 1;
            }
            if (_numRepeticionesDelClip >= 3)
            { //ya se repitió 2 veces el mismo sonido, evitaremos una tercera
                ActivaFase2();
                return;
            }
            co = StartCoroutine(MueveJack());
            emisorSonido.loop = true;
            franjaLimite_anim.SetBool("Reproduce", false);
            emisorSonido.Play();
        }

        public void EvaluaRespuesta(string respuesta)
        {
            bloqueaClicks_panel.SetActive(true);
            franjaLimite.gameObject.SetActive(false);
            if (diccionarioBotones.ContainsKey(respuesta) == false)
            {  //le dio clic a un distractor
               //   Debug.Log("Respuesta incorrecta");
                StartCoroutine(Incorrecto());
                return;
            }
            if (diccionarioBotones[respuesta] == sonidoFaseJugar)
            {
                //   Debug.Log("Respuesta correcta");
                StartCoroutine(Correcto());
            }
            else
            {
                //   Debug.Log("Respuesta incorrecta");                      //Aqui hay que meter una corrutina o parecido para que aparezca el mensaje de error y se resete etc         
                StartCoroutine(Incorrecto());
            }

        }

        IEnumerator Incorrecto()                                       //Precisamente la corrutina para mostrar los mensajes de respuesta incorrecta
        {
            StopCoroutine(co);
            emisorSonido.Stop();
            _numFallos++;
            // Debug.Log("Un error mas: " + _numFallos);
            _numRespuestasDadas++;
            // Debug.Log("Una respuesta mas: " + _numRespuestasDadas);
            //_numAciertos = 0;
            if ((_numFallos >= 4) && _numRespuestasDadas == 6)
            {
                bocina.clip = bajasNivel_audioClip;
                bocina.Play();
                GuardaPartida(false, seAgotoTiempoDeRespuesta);
                mascota.GetComponent<Animator>().Rebind();
                mascota.GetComponent<Animator>().SetInteger("Estado", 2); //triste
                coins_panel.SetActive(true);
                coins_text.text = "¡No te rindas!";
                fallos_text.text = "Fallos: " + _numFallos;
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                _numFallos = 0;
                nivel = (nivel == 1) ? 1 : nivel - 1;
                nivel_text.text = "Nivel " + nivel;
                mensajeBajaNivel.SetActive(true);
                yield return new WaitForSeconds(4f);
                //mensajeBajaNivel.SetActive(false);
                fallos_text.text = "Fallos: " + _numFallos;
                //  Debug.Log("Mensaje:Baja nivel");
                PreparaNivel();
            }
            else if ((_numFallos == 3 || _numFallos == 2) && _numRespuestasDadas == 6)
            {
                bocina.clip = derrota_audioClip;
                bocina.Play();
                GuardaPartida(false, seAgotoTiempoDeRespuesta);                
                fallos_text.text = "Fallos: " + _numFallos;
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                _numFallos = 0;
                nivel = (nivel == 1) ? 1 : nivel;
                nivel_text.text = "Nivel " + nivel;
                mensajeMismoNivel.SetActive(true);
                yield return new WaitForSeconds(3f);
                //mensajeBajaNivel.SetActive(false);
                fallos_text.text = "Fallos: " + _numFallos;
                //   Debug.Log("Mensaje:Permanece nivel");
                PreparaNivel();
            }
            else if (_numFallos <= 1 && _numRespuestasDadas == 6)
            {
                bocina.clip = subesNivel_audioClip;
                bocina.Play();
                GuardaPartida(false, seAgotoTiempoDeRespuesta);
                mascota.GetComponent<Animator>().Rebind();
                mascota.GetComponent<Animator>().SetInteger("Estado", 0); //celebrando
                coins_panel.SetActive(true);
                coins_text.text = "¡+" + _NeurocoinsRecibidasAlSubirDeNivel + " Neurocoins!";
                ActualizaNeurocoins(_NeurocoinsRecibidasAlSubirDeNivel);
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                fallos_text.text = "Fallos: " + _numFallos;
                _numAciertos = 0;
                nivel = (nivel == 10) ? 10 : nivel + 1;
                nivel_text.text = "Nivel " + nivel;
                StartCoroutine(ActivaMensajeSubeNivel());
                yield return new WaitForSeconds(4f);
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                PreparaNivel();
            }
            else
            {
                bocina.clip = derrota_audioClip;
                bocina.Play();
                mensajeIncorrecto.SetActive(true);
                yield return new WaitForSeconds(3f);
                mensajeIncorrecto.SetActive(false);
                fallos_text.text = "Fallos: " + _numFallos;
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                ActivaFase2();
            }
        }

        IEnumerator Correcto()
        {
            StopCoroutine(co);
            emisorSonido.Stop();
            int i = Random.Range(1, 11);                                 //Se agregó para que aparezca uno u otro de los mensajes de respuesta correcta gracias al if "i % 2 == 0"
                                                                         //Debug.Log(i);
            _numAciertos++;
            //  Debug.Log("Un acierto mas: " + _numAciertos);
            _numRespuestasDadas++;
            //  Debug.Log("Una respuesta mas: " + _numRespuestasDadas);
            ///_numFallos = 0;
            if ((_numAciertos >= 5) && _numRespuestasDadas == 6)
            {
                bocina.clip = subesNivel_audioClip;
                bocina.Play();
                mascota.GetComponent<Animator>().Rebind();
                mascota.GetComponent<Animator>().SetInteger("Estado", 0); //celebrando
                coins_panel.SetActive(true);
                coins_text.text = "¡+" + _NeurocoinsRecibidasAlSubirDeNivel + " Neurocoins!";
                ActualizaNeurocoins(_NeurocoinsRecibidasAlSubirDeNivel);
                GuardaPartida(true, false);
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                fallos_text.text = "Fallos: " + _numFallos;
                _numAciertos = 0;
                nivel = (nivel == 10) ? 10 : nivel + 1;
                nivel_text.text = "Nivel " + nivel;
                StartCoroutine(ActivaMensajeSubeNivel());           //Preguntar si no esta mal tener una corrutina dentro de otra
                yield return new WaitForSeconds(4f);
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                PreparaNivel();
            }
            else if ((_numAciertos == 3 || _numAciertos == 4) && _numRespuestasDadas == 6)
            {
                bocina.clip = victoria_audioClip;
                bocina.Play();
                mascota.GetComponent<Animator>().Rebind();
                mascota.GetComponent<Animator>().SetInteger("Estado", 1); //aplaudiendo
                coins_panel.SetActive(true);
                coins_text.text = "¡+" + _NeurocoinsRecibidasAlGanar + " Neurocoins!";
                ActualizaNeurocoins(_NeurocoinsRecibidasAlGanar);
                GuardaPartida(true, false);
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                fallos_text.text = "Fallos: " + _numFallos;
                _numAciertos = 0;
                nivel = (nivel == 10) ? 10 : nivel;
                nivel_text.text = "Nivel " + nivel;
                mensajeMismoNivel.SetActive(true);
                //StartCoroutine(ActivaMensajeSubeNivel());           
                yield return new WaitForSeconds(3f);
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                PreparaNivel();
            }
            else if (_numAciertos <= 2 && _numRespuestasDadas == 6)
            {
                bocina.clip = bajasNivel_audioClip;
                bocina.Play();
                mascota.GetComponent<Animator>().Rebind();
                mascota.GetComponent<Animator>().SetInteger("Estado", 2); //triste
                coins_panel.SetActive(true);
                coins_text.text = "¡No te rindas!";
                GuardaPartida(true, false);
                fallos_text.text = "Fallos: " + _numFallos;
                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                _numFallos = 0;
                nivel = (nivel == 1) ? 1 : nivel - 1;
                nivel_text.text = "Nivel " + nivel;
                mensajeBajaNivel.SetActive(true);
                yield return new WaitForSeconds(4f);
                //mensajeBajaNivel.SetActive(false);
                fallos_text.text = "Fallos: " + _numFallos;
                //  Debug.Log("Mensaje:Baja nivel");
                PreparaNivel();
            }
            else
            {
                bocina.clip = victoria_audioClip;
                bocina.Play();
                if (i % 2 == 0)
                {
                    mensajeCorrecto1.SetActive(true);
                    yield return new WaitForSeconds(3f);
                    mensajeCorrecto1.SetActive(false);
                }
                else
                {
                    mensajeCorrecto2.SetActive(true);
                    yield return new WaitForSeconds(3f);
                    mensajeCorrecto2.SetActive(false);
                }

                aciertos_text.text = "Aciertos: " + _numAciertos + "/ 6";
                fallos_text.text = "Fallos: " + _numFallos;
                ActivaFase2();
            }
        }

        IEnumerator ActivaMensajeSubeNivel()
        {
            int i = Random.Range(1, 21);
            if (i % 2 == 0)
            {
                mensajeSubeNivel1.SetActive(true);
                yield return new WaitForSeconds(3f);
                //mensajeSubeNivel1.SetActive(false);
                //   Debug.Log("Mensaje 1: Sube nivel");
            }
            else
            {
                mensajeSubeNivel2.SetActive(true);
                yield return new WaitForSeconds(3f);
                //mensajeSubeNivel2.SetActive(false);
                //   Debug.Log("Mensaje 2: Sube nivel");
            }
        }

        IEnumerator MueveJack()                                         //Corrutina que se encarga de mover el jack (marca el tiempo para poder responder)
        {
            jack.GetComponent<RectTransform>().localPosition = new Vector3(-300f, -8f, 0f);

            seAgotoTiempoDeRespuesta = false;

            switch (nivel)
            {
                case 1:                //Duración aprox: 10 segundos             
                case 2:                 //Duración aprox: 10 segundos
                case 3:                 //Duración aprox: 10 segundos
                case 4:                 //Duración aprox: 10 segundos
                    while (seAgotoTiempoDeRespuesta == false)
                    {
                        jack.GetComponent<RectTransform>().localPosition += new Vector3(30f, 0f, 0f) * Time.deltaTime;
                        yield return null;
                        //franjaLimite.GetComponent<RectTransform>().offsetMax.Set(10f ,10f);              //Con esto vamos reduciendo la barra que representará nuestro tiempo???
                        //franjaLimite.gameObject.SetActive(true);
                        franjaLimite_anim.SetBool("Reproduce", true);
                        franjaLimite_anim.speed = 0.07f;

                    }
                    break;
                case 5:                 //Duración aprox: 8 segundos
                    while (seAgotoTiempoDeRespuesta == false)
                    {
                        jack.GetComponent<RectTransform>().localPosition += new Vector3(40f, 0f, 0f) * Time.deltaTime;
                        yield return null;
                        franjaLimite_anim.SetBool("Reproduce", true);
                        franjaLimite_anim.speed = 0.09f;
                    }
                    break;
                case 6:                 //Duración aprox: 7 segundos
                    while (seAgotoTiempoDeRespuesta == false)
                    {
                        jack.GetComponent<RectTransform>().localPosition += new Vector3(50f, 0f, 0f) * Time.deltaTime;
                        yield return null;
                        franjaLimite_anim.SetBool("Reproduce", true);
                        franjaLimite_anim.speed = 0.117f;
                    }
                    break;
                case 7:                 //Duración aprox: 6 segundos
                    while (seAgotoTiempoDeRespuesta == false)
                    {
                        jack.GetComponent<RectTransform>().localPosition += new Vector3(60f, 0f, 0f) * Time.deltaTime;
                        yield return null;
                        franjaLimite_anim.SetBool("Reproduce", true);
                        franjaLimite_anim.speed = 0.137f;
                    }
                    break;
                case 8:                 //Duración aprox: 5 segundos              
                case 9:                 //Duración aprox: 5 segundos
                case 10:                //Duración aprox: 5 segundos
                    while (seAgotoTiempoDeRespuesta == false)
                    {
                        jack.GetComponent<RectTransform>().localPosition += new Vector3(70f, 0f, 0f) * Time.deltaTime;
                        yield return null;
                        franjaLimite_anim.SetBool("Reproduce", true);
                        franjaLimite_anim.speed = 0.158f;
                    }
                    break;
                default:
                    break;
            }
            //Debug.Log("Se agoto tiempo de respuesta, se toma ejercicio como malo.");
            franjaLimite.gameObject.SetActive(false);
            StartCoroutine(Incorrecto());
        }


        void DesactivaTodosLosBotonesFase1()
        {
            botonesFase1Nivel1.SetActive(false);
            botonesFase1Nivel2A.SetActive(false);
            botonesFase1Nivel2B.SetActive(false);
            botonesFase1Nivel2C.SetActive(false);
            botonesFase1Nivel3.SetActive(false);
            botonesFase1Nivel4A.SetActive(false);
            botonesFase1Nivel4B.SetActive(false);
            botonesFase1Nivel4C.SetActive(false);
            botonesFase1Nivel4D.SetActive(false);
            botonesFase1Nivel5.SetActive(false);
            botonesFase1Nivel6A.SetActive(false);
            botonesFase1Nivel6B.SetActive(false);
            botonesFase1Nivel6C.SetActive(false);
            botonesFase1Nivel6D.SetActive(false);
            botonesFase1Nivel6E.SetActive(false);
            botonesFase1Nivel7A.SetActive(false);
            botonesFase1Nivel7B.SetActive(false);
            botonesFase1Nivel7C.SetActive(false);
            botonesFase1Nivel7D.SetActive(false);
            botonesFase1Nivel7E.SetActive(false);
            botonesFase1Nivel7F.SetActive(false);
            botonesFase1Nivel8.SetActive(false);
            botonesFase1Nivel9A.SetActive(false);
            botonesFase1Nivel9B.SetActive(false);
            botonesFase1Nivel9C.SetActive(false);
            botonesFase1Nivel9D.SetActive(false);
            botonesFase1Nivel9E.SetActive(false);
            botonesFase1Nivel9F.SetActive(false);
            botonesFase1Nivel10A.SetActive(false);
            botonesFase1Nivel10B.SetActive(false);
            botonesFase1Nivel10C.SetActive(false);
            botonesFase1Nivel10D.SetActive(false);
            botonesFase1Nivel10E.SetActive(false);
            botonesFase1Nivel10F.SetActive(false);
        }

        void DesactivaTodosLosBotonesFase2()
        {
            botonesFase2Nivel1.SetActive(false);
            botonesFase2Nivel2.SetActive(false);
            botonesFase2Nivel3.SetActive(false);
            botonesFase2Nivel4.SetActive(false);
            botonesFase2Nivel5.SetActive(false);
            botonesFase2Nivel6.SetActive(false);
            botonesFase2Nivel7.SetActive(false);
            botonesFase2Nivel8.SetActive(false);
            botonesFase2Nivel9.SetActive(false);
            botonesFase2Nivel10.SetActive(false);
        }

        void ReseteaMarcador()
        {
            _numRespuestasDadas = 0;
            _numAciertos = 0;
            _numFallos = 0;
            fallos_text.text = "Fallos: " + _numFallos;
            aciertos_text.text = "Aciertos: " + _numAciertos;
        }

        public void Pausa()
        {
            Time.timeScale = 0f;
            botonPausa.SetActive(false);
            menuPausa.SetActive(true);
            emisorSonido.Pause();
        }

        public void Reanudar()
        {
            Time.timeScale = 1f;
            botonSalir.SetActive(true);
            menuSalir.SetActive(false);
            emisorSonido.Play();
        }

        public void Salir()         //Esto es para el botón con una X
        {
            Time.timeScale = 0f;
            botonSalir.SetActive(false);
            menuSalir.SetActive(true);
            emisorSonido.Pause();
        }

        public void ConfirmaSalir()     //Esto es para el botón de sí en el menu de salir
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
            botonSalir.SetActive(true);
            SceneManager.LoadScene(menuInicio);
        }

        void DefineSonidosDelNivel(int numBotones)        //
        {
            sonidosAMemorizar.Clear();
            switch (nivel)
            {
                case 1:         //baja
                    int sonido1 = Random.Range(0, 20);
                    int sonido2 = Random.Range(20, 40);
                    sonidosAMemorizar.Add(sonidos[sonido1]);
                    sonidosAMemorizar.Add(sonidos[sonido2]);
                    break;
                case 2:         //baja
                    sonido2 = Random.Range(0, 20);
                    sonido1 = Random.Range(20, 40);
                    sonidosAMemorizar.Add(sonidos[sonido1]);
                    sonidosAMemorizar.Add(sonidos[sonido2]);
                    break;
                case 3:         //media
                                /*for (int i = Random.Range(0, 20); i < 40; i += 4)
                                {
                                    if (sonidosAMemorizar.Count < 3)                //Se agregó el if porque al parecer seguia asignando sonidos hasta acabar con la lista y no hasta cubrir el numero de botones
                                    {
                                        sonidosAMemorizar.Add(sonidos[i]);
                                        Debug.Log("Asigné esto en nivel 3: " + i + "!!!");
                                    }
                                }*/
                                //baja
                    sonido1 = Random.Range(0, 13);
                    sonido2 = Random.Range(14, 26);
                    int sonido3 = Random.Range(27, 40);
                    sonidosAMemorizar.Add(sonidos[sonido1]);
                    sonidosAMemorizar.Add(sonidos[sonido2]);
                    sonidosAMemorizar.Add(sonidos[sonido3]);
                    break;
                case 4:         //media
                                /*for (int i = Random.Range(0, 20); i < 40; i += 4)
                                {
                                    if (sonidosAMemorizar.Count < 4)
                                    {
                                        sonidosAMemorizar.Add(sonidos[i]);
                                        Debug.Log("Asigné esto en nivel 4: " + i + "!!!");
                                    }
                                }*/
                                //baja
                    sonido3 = Random.Range(0, 13);
                    sonido2 = Random.Range(14, 26);
                    sonido1 = Random.Range(27, 40);
                    sonidosAMemorizar.Add(sonidos[sonido1]);
                    sonidosAMemorizar.Add(sonidos[sonido2]);
                    sonidosAMemorizar.Add(sonidos[sonido3]);
                    break;
                case 5:         //baja
                    sonido1 = Random.Range(0, 10);
                    sonido2 = Random.Range(10, 20);
                    sonido3 = Random.Range(20, 30);
                    int sonido4 = Random.Range(30, 40);
                    sonidosAMemorizar.Add(sonidos[sonido1]);
                    sonidosAMemorizar.Add(sonidos[sonido2]);
                    sonidosAMemorizar.Add(sonidos[sonido3]);
                    sonidosAMemorizar.Add(sonidos[sonido4]);
                    break;
                case 6:         //baja
                    sonido4 = Random.Range(0, 10);
                    sonido3 = Random.Range(10, 20);
                    sonido2 = Random.Range(20, 30);
                    sonido1 = Random.Range(30, 40);
                    sonidosAMemorizar.Add(sonidos[sonido1]);
                    sonidosAMemorizar.Add(sonidos[sonido2]);
                    sonidosAMemorizar.Add(sonidos[sonido3]);
                    sonidosAMemorizar.Add(sonidos[sonido4]);
                    break;
                case 7:         //media
                    for (int i = 12; i < 16; i++)        //El for va asignando los sonidos hasta cubrir el # de botones
                    {
                        sonidosAMemorizar.Add(sonidos[i]);
                        // Debug.Log("Asigné esto nivel 7: " + i);
                    }
                    break;
                case 8:         //media
                    for (int i = 15; i < 20; i++)        //El for va asignando los sonidos hasta cubrir el # de botones
                    {
                        sonidosAMemorizar.Add(sonidos[i]);
                        //  Debug.Log("Asigné esto nivel 8: " + i);
                    }
                    break;
                case 9:         //alta
                    for (int i = Random.Range(0, 15); i < 40; i += 4)
                    {
                        if (sonidosAMemorizar.Count < 5)
                        {
                            sonidosAMemorizar.Add(sonidos[i]);
                            //   Debug.Log("Asigné esto en nivel 9: " + i + "!!!");
                        }
                    }
                    break;
                case 10:        //alta
                    for (int i = Random.Range(0, 15); i < 40; i += 4)
                    {
                        if (sonidosAMemorizar.Count < 5)
                        {
                            sonidosAMemorizar.Add(sonidos[i]);
                            //  Debug.Log("Asigné esto en nivel 10: " + i + "!!!");
                        }
                    }
                    break;
            }
        }

        public void ReproduceSonido(string colorBoton)
        {
            emisorSonido.Stop();
            emisorSonido.clip = diccionarioBotones[colorBoton];
            emisorSonido.loop = false;
            emisorSonido.Play();

        }

        void EligeSonidoAleatorio()
        {
            sonidoFaseJugar = sonidosAMemorizar[Random.Range(0, sonidosAMemorizar.Count)];    //Con esto podemos elegir la cancion que va a sonar en la fase 2, una aleatoria desde la lista usada en la fase 1
                                                                                              //Debug.Log("El sonido fue: " + sonidoFaseJugar);
        }

        public void GuardaPartida(bool victoria,bool agoto_tiempo)
        {
            Partida p = new Partida();
            p.nivel = nivel;
            p.juego = "Color Band";
            p.fecha = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            p.victoria = victoria;
            p.agoto_tiempo = agoto_tiempo;
            StartCoroutine(AduanaCITAN.SubePartidaA_CITAN(p));
        }

        public void ActualizaNeurocoins(int coins)
        {
            StartCoroutine(AduanaCITAN.ActualizaNeurocoins_CITAN(coins));
        }
    }
}