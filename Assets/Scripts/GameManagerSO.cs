using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameManager")]

public class GameManagerSO : ScriptableObject
{
    public event Action<int> OnBotonPulsado;
    public event Action<int> OnNuevaInteraccion;

    public void BotonPulsado(int idBoton)
    {
        //El boton ha sido pulsado
        OnBotonPulsado?.Invoke(idBoton);
    }

    public void InteractuableEjecutado(int idInteraccion)
    {
        OnNuevaInteraccion?.Invoke(idInteraccion);
    }
 
}
