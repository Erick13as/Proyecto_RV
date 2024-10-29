using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarModelo : MonoBehaviour
{
    // Referencias a los modelos 
    public GameObject modeloInicial;
    public GameObject modeloConMaterial;
    
    private bool tienePollo = false;

    private void Start()
    {
        if (modeloInicial != null) modeloInicial.SetActive(true);
        if (modeloConMaterial != null) modeloConMaterial.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Para recoger el material
        if (other.CompareTag("Tortilla") && !tienePollo)
        {
            CambiarAModeloConMaterial();
        }
        // Para soltar el material
        else if (other.CompareTag("Plato") && tienePollo)
        {
            CambiarAModeloInicial();
        }
    }

    private void CambiarAModeloConMaterial()
    {
        modeloInicial.SetActive(false);
        modeloConMaterial.SetActive(true);
        tienePollo = true;
    }

    private void CambiarAModeloInicial()
    {
        modeloInicial.SetActive(true);
        modeloConMaterial.SetActive(false);
        tienePollo = false;
    }
}