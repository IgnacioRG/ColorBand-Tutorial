using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorBand
{
    public class MoveRigidbody : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            // Debug.Log("Entrando a Trigger Jack");
            AdminColorBand.seAgotoTiempoDeRespuesta = true;
        }
    }
}
