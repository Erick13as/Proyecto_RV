using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PantallaManager pantallaManager;
    public string[] tiposQuesadilla = { "quesadilla_completa", "quesadilla_frijoles_queso", "quesadilla_frijoles", "quesadilla_pollo_frijoles", "quesadilla_pollo_queso", "quesadilla_pollo", "quesadilla_queso" };
    private List<string> pedidoActual = new List<string>();
    private int pedidoCompleto = 0;
    private bool juegoEnCurso = true;

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
        pedidoCompleto = 0;
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
        if (!juegoEnCurso) return;

        if (pedidoCompleto < pedidoActual.Count)
        {
            Debug.Log("Quesadilla completada: " + pedidoActual[pedidoCompleto]);

            // Mostrar la imagen correspondiente a la quesadilla actual en PantallaManager
            pantallaManager.MostrarTipoQuesadilla(pedidoActual[pedidoCompleto]);
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

    public void QuesadillaQuemada()
    {
        if (!juegoEnCurso) return;

        Debug.Log("La quesadilla se ha quemado. Fin del juego.");
        juegoEnCurso = false;
        MostrarMensaje("Pedido fallido. La quesadilla se quemó", 2f, ReiniciarJuego);
    }

    private void FinalizarJuego()
    {
        juegoEnCurso = false;
        Debug.Log("¡Juego completado!");
        pantallaManager.DisplaySpecificImage(1); // Imagen de "Termina el juego"
    }

    private void ReiniciarJuego()
    {
        juegoEnCurso = true;
        GenerarPedido();
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
