using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorTrampaPinchoTecho : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int id;

    public void Activar()
    {
        gM.InteractuableEjecutado(id);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovimientoControl movimientoControl))
        {
            Activar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovimientoControl movimientoControl))
        {
            Activar();
        }
    }

}

