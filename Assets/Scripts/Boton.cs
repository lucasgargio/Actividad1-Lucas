using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] int idBoton;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out MovimientoControl movimientoControl))
        {
            gM.BotonPulsado(idBoton);
            Debug.Log("Hola");
        }
    }
}
