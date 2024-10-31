using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PantallaManager pantallaManager;
    public QuesadillaMonitor quesadillaMonitor;

    public string[] tiposQuesadilla = { "quesadilla_frijoles", "quesadilla_frijoles_queso", "quesadilla_pollo", "quesadilla_pollo_frijoles", "quesadilla_pollo_queso", "quesadilla_queso" };
    private List<string> pedidoQuesadillas = new List<string>(); 
    private int quesadillaActual = 0; 
    private bool juegoEnCurso = true;

    private void Start()
    {
        IniciarPedido();
    }

    // Inicia un nuevo pedido de 3 quesadillas aleatorias
    private void IniciarPedido()
    {
        pedidoQuesadillas.Clear();
        juegoEnCurso = true;
        SeleccionarQuesadillasAleatorias();
        quesadillaActual = 0;
        pantallaManager.DisplaySpecificImage(0); // Imagen: "Ha llegado un cliente con un pedido de 3 quesadillas"
        Debug.Log("Pedido iniciado: Cliente ha solicitado 3 quesadillas.");
        StartCoroutine(EsperarYEmpezarSiguienteQuesadilla());
    }

    // Selecciona 3 tipos de quesadillas aleatorias del conjunto disponible
    private void SeleccionarQuesadillasAleatorias()
    {
        List<int> indicesUsados = new List<int>();

        while (pedidoQuesadillas.Count < 3)
        {
            int index = Random.Range(0, tiposQuesadilla.Length);
            if (!indicesUsados.Contains(index))
            {
                pedidoQuesadillas.Add(tiposQuesadilla[index]);
                indicesUsados.Add(index);
            }
        }
    }

    // Controla el inicio de cada quesadilla del pedido
    private IEnumerator EsperarYEmpezarSiguienteQuesadilla()
    {
        yield return new WaitForSeconds(2f);
        IniciarQuesadilla();
    }

    // Inicia la preparación de la quesadilla actual
    private void IniciarQuesadilla()
    {
        string tipoQuesadilla = pedidoQuesadillas[quesadillaActual];
        pantallaManager.DisplaySpecificImage(1); // Imagen: "Prepara la quesadilla X"
        Debug.Log("Inicia preparación de la quesadilla: " + tipoQuesadilla);
        pantallaManager.MostrarTipoQuesadilla(tipoQuesadilla);
        quesadillaMonitor.IniciarNuevaQuesadilla();
    }

    // Llamado desde `QuesadillaMonitor` al completar una quesadilla
    public void CompletarQuesadilla()
    {
        if (!juegoEnCurso) return;

        Debug.Log("Quesadilla completada correctamente.");
        pantallaManager.DisplaySpecificImage(3); // Imagen: "La quesadilla está lista! Excelente, pasa a la siguiente."
        quesadillaActual++;

        if (quesadillaActual >= pedidoQuesadillas.Count)
        {
            FinalizarJuego(true); // Si completó las 3 quesadillas, el usuario gana el juego
        }
        else
        {
            StartCoroutine(EsperarYEmpezarSiguienteQuesadilla());
        }
    }

    // Llamado desde `QuesadillaMonitor` si la quesadilla se quema
    public void QuesadillaQuemada()
    {
        if (!juegoEnCurso) return;

        Debug.Log("La quesadilla se ha quemado. Fin del juego.");
        pantallaManager.DisplaySpecificImage(4); // Imagen: "No lograste cocinar la quesadilla correctamente. Inténtalo de nuevo"
        FinalizarJuego(false); // Finaliza el juego con pérdida
    }

    // Finaliza el juego, ya sea con éxito o con pérdida, y prepara el reinicio
    private void FinalizarJuego(bool exito)
    {
        juegoEnCurso = false;
        if (exito)
        {
            pantallaManager.DisplaySpecificImage(5); // Imagen: "Has ganado"
            Debug.Log("¡Felicidades! Pedido completado.");
        }
        else
        {
            pantallaManager.DisplaySpecificImage(6); // Imagen: "Has perdido"
            Debug.Log("Perdiste. La quesadilla se quemó. Empezando de nuevo...");
        }

        StartCoroutine(ReiniciarJuego()); // Reinicia el juego después de un breve periodo
    }

    // Reinicia el pedido y el juego después de un breve retraso
    private IEnumerator ReiniciarJuego()
    {
        yield return new WaitForSeconds(3f);
        IniciarPedido();
    }
}
