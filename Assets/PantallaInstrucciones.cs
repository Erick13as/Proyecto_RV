using UnityEngine;
using System.Collections;

public class PantallaInstrucciones : MonoBehaviour
{
    private MeshRenderer pantallaRenderer;

    [System.Serializable]
    public class InstruccionMaterial
    {
        public Material material;
        public string descripcion;
    }

    public InstruccionMaterial instruccionInicial;
    public InstruccionMaterial instruccionInstrumentos;
    public InstruccionMaterial instruccionAgregarFrijoles;
    public InstruccionMaterial instruccionAgregarQueso;
    public InstruccionMaterial instruccionAgregarPollo;
    public InstruccionMaterial instruccionDoblar;
    public InstruccionMaterial instruccionColocarEnPlato;
    public InstruccionMaterial quesadillaCompleta;
    public InstruccionMaterial instruccionFinal; // Nueva instrucción final

    public QuesadillaTransformer2 quesadillaTransformer;
    private bool mostrandoInstruccionDoblar = false;
    private bool instruccionPlatoMostrada = false;
    private bool mostrarSiguienteInstruccion = false;

    private void Start()
    {
        pantallaRenderer = GetComponent<MeshRenderer>();
        if (pantallaRenderer == null)
        {
            Debug.LogError("No se encontró el MeshRenderer en la pantalla");
            return;
        }

        StartCoroutine(MostrarInstruccionInicial());
    }

    private IEnumerator MostrarInstruccionInicial()
    {
        MostrarInstruccion(instruccionInicial.material);
        yield return new WaitForSeconds(5f);
        MostrarInstruccion(instruccionInstrumentos.material);
        yield return new WaitForSeconds(10f);
        mostrarSiguienteInstruccion = true;
    }

    private void Update()
    {
        if (!mostrarSiguienteInstruccion || quesadillaTransformer == null) return;

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
            instruccionPlatoMostrada = true;
        }
        else if (instruccionPlatoMostrada) // Lógica para mostrar la instrucción final
        {
            MostrarInstruccion(instruccionFinal.material);
            mostrarSiguienteInstruccion = false; // Detener el Update
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

    public void ReiniciarPantalla()
    {
        mostrandoInstruccionDoblar = false;
        instruccionPlatoMostrada = false;
        mostrarSiguienteInstruccion = false;
        StartCoroutine(MostrarInstruccionInicial());
    }
}


