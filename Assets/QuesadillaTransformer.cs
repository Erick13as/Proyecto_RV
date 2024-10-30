using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesadillaTransformer : MonoBehaviour
{
    // Referencias a los diferentes modelos de quesadilla
    public GameObject quesadillaBase;
    public GameObject quesadillaQueso;
    public GameObject quesadillaPollo;
    public GameObject quesadillaFrijoles;
    public GameObject quesadillaPolloQueso;
    public GameObject quesadillaFrijolesQueso;
    public GameObject quesadillaPolloFrijoles;
    public GameObject quesadillaCompleta; 

    // Estado actual de la quesadilla
    private bool tieneQueso = false;
    private bool tienePollo = false;
    private bool tieneFrijoles = false;

    private void Start()
    {
        // Quesadilla base
        DesactivarTodosLosModelos();
        if (quesadillaBase != null) quesadillaBase.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto es "Queso" y no ha sido agregado aún
        if (other.CompareTag("Queso") && !tieneQueso)
        {
            AgregarQueso();
            Destroy(other.gameObject); // Eliminar el objeto de queso tras agregarlo
        }

        // Obtener el script de la cuchara para otros ingredientes
        CambiarModelo scriptCuchara = other.GetComponent<CambiarModelo>();

        if (scriptCuchara != null)
        {
            // Verificar el tag y si la cuchara tiene el material
            if (other.CompareTag("CucharaPollo") && !tienePollo && scriptCuchara.TieneMaterial())
            {
                AgregarPollo();
                scriptCuchara.UsarMaterial();
            }
            else if (other.CompareTag("CucharaFrijoles") && !tieneFrijoles && scriptCuchara.TieneMaterial())
            {
                AgregarFrijoles();
                scriptCuchara.UsarMaterial();
            }
        }
    }

    private void AgregarQueso()
    {
        tieneQueso = true;
        ActualizarModelo();
    }

    private void AgregarPollo()
    {
        tienePollo = true;
        ActualizarModelo();
    }

    private void AgregarFrijoles()
    {
        tieneFrijoles = true;
        ActualizarModelo();
    }

    private void ActualizarModelo()
    {
        DesactivarTodosLosModelos();

        // Activar el modelo correspondiente según la combinación de ingredientes
        if (tieneQueso && tienePollo && tieneFrijoles)
        {
            if (quesadillaCompleta != null) quesadillaCompleta.SetActive(true);
        }
        else if (tieneQueso && tienePollo)
        {
            if (quesadillaPolloQueso != null) quesadillaPolloQueso.SetActive(true);
        }
        else if (tieneQueso && tieneFrijoles)
        {
            if (quesadillaFrijolesQueso != null) quesadillaFrijolesQueso.SetActive(true);
        }
        else if (tienePollo && tieneFrijoles)
        {
            if (quesadillaPolloFrijoles != null) quesadillaPolloFrijoles.SetActive(true);
        }
        else if (tieneQueso)
        {
            if (quesadillaQueso != null) quesadillaQueso.SetActive(true);
        }
        else if (tienePollo)
        {
            if (quesadillaPollo != null) quesadillaPollo.SetActive(true);
        }
        else if (tieneFrijoles)
        {
            if (quesadillaFrijoles != null) quesadillaFrijoles.SetActive(true);
        }
        else
        {
            if (quesadillaBase != null) quesadillaBase.SetActive(true);
        }
    }

    private void DesactivarTodosLosModelos()
    {
        if (quesadillaBase != null) quesadillaBase.SetActive(false);
        if (quesadillaQueso != null) quesadillaQueso.SetActive(false);
        if (quesadillaPollo != null) quesadillaPollo.SetActive(false);
        if (quesadillaFrijoles != null) quesadillaFrijoles.SetActive(false);
        if (quesadillaPolloQueso != null) quesadillaPolloQueso.SetActive(false);
        if (quesadillaFrijolesQueso != null) quesadillaFrijolesQueso.SetActive(false);
        if (quesadillaPolloFrijoles != null) quesadillaPolloFrijoles.SetActive(false);
        if (quesadillaCompleta != null) quesadillaCompleta.SetActive(false); 
    }
}
