using UnityEngine;
using System.Collections.Generic;

public class PantallaInstrucciones : MonoBehaviour
{
    // Renderer de la pantalla
    private MeshRenderer pantallaRenderer;

    [System.Serializable]
    public class InstruccionMaterial
    {
        public Material material;
        public string descripcion;
    }

    // Materiales para diferentes estados/instrucciones
    public InstruccionMaterial instruccionInicial;
    public InstruccionMaterial instruccionAgregarFrijoles;
    public InstruccionMaterial instruccionAgregarQueso;
    public InstruccionMaterial instruccionAgregarPollo;
    public InstruccionMaterial instruccionDoblar;
    public InstruccionMaterial instruccionColocarEnPlato;
    public InstruccionMaterial quesadillaCompleta;

    // Referencias a los objetos que necesitamos monitorear
    public QuesadillaTransformer2 quesadillaTransformer;
    private bool mostrandoInstruccionDoblar = false;
    private bool instruccionPlatoMostrada = false;

    private void Start()
    {
        pantallaRenderer = GetComponent<MeshRenderer>();
        if (pantallaRenderer == null)
        {
            Debug.LogError("No se encontró el MeshRenderer en la pantalla");
            return;
        }

        // Mostrar instrucción inicial
        MostrarInstruccion(instruccionInicial.material);
    }

    private void Update()
    {
        if (quesadillaTransformer == null) return;

        // Verificar el estado actual de la quesadilla usando los GameObjects activos
        bool tieneFrijoles = EstaActivoCualquierModelo(new GameObject[] {
            quesadillaTransformer.quesadillaFrijoles,
            quesadillaTransformer.quesadillaFrijolesQueso,
            quesadillaTransformer.quesadillaPolloFrijoles,
            quesadillaTransformer.quesadillaCompleta,
            quesadillaTransformer.quesadillaFrijolesDoblada,
            quesadillaTransformer.quesadillaFrijolesQuesoDoblada,
            quesadillaTransformer.quesadillaPolloFrijolesDoblada,
            quesadillaTransformer.quesadillaCompletaDoblada
        });

        bool tieneQueso = EstaActivoCualquierModelo(new GameObject[] {
            quesadillaTransformer.quesadillaQueso,
            quesadillaTransformer.quesadillaPolloQueso,
            quesadillaTransformer.quesadillaFrijolesQueso,
            quesadillaTransformer.quesadillaCompleta,
            quesadillaTransformer.quesadillaQuesoDoblada,
            quesadillaTransformer.quesadillaPolloQuesoDoblada,
            quesadillaTransformer.quesadillaFrijolesQuesoDoblada,
            quesadillaTransformer.quesadillaCompletaDoblada
        });

        bool tienePollo = EstaActivoCualquierModelo(new GameObject[] {
            quesadillaTransformer.quesadillaPollo,
            quesadillaTransformer.quesadillaPolloQueso,
            quesadillaTransformer.quesadillaPolloFrijoles,
            quesadillaTransformer.quesadillaCompleta,
            quesadillaTransformer.quesadillaPolloDoblada,
            quesadillaTransformer.quesadillaPolloQuesoDoblada,
            quesadillaTransformer.quesadillaPolloFrijolesDoblada,
            quesadillaTransformer.quesadillaCompletaDoblada
        });

        bool estaDoblada = EstaActivoCualquierModelo(new GameObject[] {
            quesadillaTransformer.tortillaDoblada,
            quesadillaTransformer.quesadillaQuesoDoblada,
            quesadillaTransformer.quesadillaPolloDoblada,
            quesadillaTransformer.quesadillaFrijolesDoblada,
            quesadillaTransformer.quesadillaPolloQuesoDoblada,
            quesadillaTransformer.quesadillaFrijolesQuesoDoblada,
            quesadillaTransformer.quesadillaPolloFrijolesDoblada,
            quesadillaTransformer.quesadillaCompletaDoblada
        });

        // Lógica para mostrar las instrucciones en el nuevo orden
        if (!tieneFrijoles && !tieneQueso && !tienePollo && !estaDoblada)
        {
            MostrarInstruccion(instruccionAgregarFrijoles.material);
            mostrandoInstruccionDoblar = false;
            instruccionPlatoMostrada = false;
        }
        else if (tieneFrijoles && !tieneQueso && !tienePollo && !estaDoblada)
        {
            MostrarInstruccion(instruccionAgregarQueso.material);
        }
        else if (tieneFrijoles && tieneQueso && !tienePollo && !estaDoblada)
        {
            MostrarInstruccion(instruccionAgregarPollo.material);
        }
        else if (tieneFrijoles && tieneQueso && tienePollo && !estaDoblada)
        {
            MostrarInstruccion(instruccionDoblar.material);
            mostrandoInstruccionDoblar = true;
        }
        else if (tieneFrijoles && tieneQueso && tienePollo && estaDoblada && !instruccionPlatoMostrada)
        {
            MostrarInstruccion(instruccionColocarEnPlato.material);
            instruccionPlatoMostrada = true;
        }
        else if (tieneFrijoles && tieneQueso && tienePollo && estaDoblada && instruccionPlatoMostrada)
        {
            MostrarInstruccion(quesadillaCompleta.material);
        }
    }

    private bool EstaActivoCualquierModelo(GameObject[] modelos)
    {
        foreach (GameObject modelo in modelos)
        {
            if (modelo != null && modelo.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void MostrarInstruccion(Material material)
    {
        if (pantallaRenderer.material != material)
        {
            pantallaRenderer.material = material;
        }
    }

    // Método público para reiniciar el estado de la pantalla
    public void ReiniciarPantalla()
    {
        mostrandoInstruccionDoblar = false;
        instruccionPlatoMostrada = false;
        MostrarInstruccion(instruccionInicial.material);
    }
}