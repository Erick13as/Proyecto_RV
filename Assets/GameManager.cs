using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script controla el flujo principal del juego después del tutorial, gestionando los pedidos de quesadillas y validando su completitud.

public class GameManager : MonoBehaviour
{
    public PantallaManager pantallaManager;
    public string[] tiposQuesadilla = { "quesadilla_frijoles", "quesadilla_frijoles_queso", "quesadilla_pollo", "quesadilla_pollo_frijoles", "quesadilla_pollo_queso", "quesadilla_queso" };
    private List<string> pedidoActual = new List<string>();
    private int pedidoCompleto = 0;

    private void Start()
    {
        IniciarJuego();
    }

    public void IniciarJuego()
    {
        pantallaManager.SetModoJuego();
        MostrarMensaje("Empieza el juego", 2f, GenerarPedido);
    }

    private void GenerarPedido()
    {
        pedidoActual.Clear();
        List<int> indicesUsados = new List<int>();

        while (pedidoActual.Count < 3)
        {
            int index = Random.Range(0, tiposQuesadilla.Length);
            if (!indicesUsados.Contains(index))
            {
                pedidoActual.Add(tiposQuesadilla[index]);
                indicesUsados.Add(index);
            }
        }

        MostrarMensaje("Llegó un cliente", 2f, MostrarPedido);
    }

    private void MostrarPedido()
    {
        foreach (var quesadilla in pedidoActual)
        {
            Debug.Log("Quesadilla a preparar: " + quesadilla);
        }
    }

    public void CompletarQuesadilla()
    {
        if (pedidoCompleto < pedidoActual.Count)
        {
            Debug.Log("Quesadilla completada: " + pedidoActual[pedidoCompleto]);
            pedidoCompleto++;
            
            if (pedidoCompleto < pedidoActual.Count)
            {
                MostrarPedido();
            }
            else
            {
                MostrarMensaje("Pedido completado", 2f, FinalizarJuego);
            }
        }
    }

    private void FinalizarJuego()
    {
        Debug.Log("¡Juego completado!");
        pantallaManager.DisplaySpecificImage(1); // Imagen de "Termina el juego"
    }

    private void MostrarMensaje(string mensaje, float duracion, System.Action callback)
    {
        Debug.Log(mensaje);
        StartCoroutine(EsperarYEjecutar(duracion, callback));
    }

    private IEnumerator EsperarYEjecutar(float duracion, System.Action callback)
    {
        yield return new WaitForSeconds(duracion);
        callback?.Invoke();
    }
}
