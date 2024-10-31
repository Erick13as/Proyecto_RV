using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarModelo : MonoBehaviour
{
    public GameObject modeloInicial;
    public GameObject modeloConMaterial;

    // Tags configurables desde el Inspector
    public string tagParaRecoger = "Tortilla";
    public string tagParaSoltar = "Plato";

    private bool tieneMaterial = false;
    public QuesadillaMonitor quesadillaMonitor; // Referencia a QuesadillaMonitor

    private void Start()
    {
        if (modeloInicial != null) modeloInicial.SetActive(true);
        if (modeloConMaterial != null) modeloConMaterial.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Para recoger el material
        if (other.CompareTag(tagParaRecoger) && !tieneMaterial)
        {
            CambiarAModeloConMaterial();
        }
        // Para soltar el material
        else if (other.CompareTag(tagParaSoltar) && tieneMaterial)
        {
            CambiarAModeloInicial();

            // Notificar a QuesadillaMonitor según el tipo de material que se agrega
            if (tagParaRecoger == "Frijoles")
            {
                quesadillaMonitor.AgregarFrijoles();
                Debug.Log("Frijoles agregados a la quesadilla.");
            }
            else if (tagParaRecoger == "Pollo")
            {
                quesadillaMonitor.AgregarPollo();
                Debug.Log("Pollo agregado a la quesadilla.");
            }
            // Añadir otros ingredientes si es necesario
        }
    }

    private void CambiarAModeloConMaterial()
    {
        modeloInicial.SetActive(false);
        modeloConMaterial.SetActive(true);
        tieneMaterial = true;
    }

    private void CambiarAModeloInicial()
    {
        modeloInicial.SetActive(true);
        modeloConMaterial.SetActive(false);
        tieneMaterial = false;
    }

    // Verificar si la cuchara tiene material
    public bool TieneMaterial()
    {
        return tieneMaterial;
    }

    // Usar el material de la cuchara
    public void UsarMaterial()
    {
        CambiarAModeloInicial();
    }
}
