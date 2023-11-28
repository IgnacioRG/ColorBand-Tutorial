using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorBand
{
    public class JackPlug : MonoBehaviour
    {
        public Transform target;
        public float speed;

        // Update is called once per frame
        void Update()
        {
            // StartCoroutine(Movimiento());
            Movimiento();
        }


        void Movimiento()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        }
    }
}
