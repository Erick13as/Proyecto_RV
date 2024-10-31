using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarModelo : MonoBehaviour
{
    // Referencias a los modelos
    public GameObject modeloInicial;
    public GameObject modeloConMaterial;
   
    // Tags configurables desde el Inspector
    public string tagParaRecoger = "Tortilla";
    public string tagParaSoltar = "Plato";
   
    private bool tieneMaterial = false;
   
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