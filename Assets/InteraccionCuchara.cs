using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionFrijoles : MonoBehaviour
{
    private GameObject frijolEnCuchara;

    void Start()
    {
        // Busca el objeto hijo "Frijol en cuchara"
        frijolEnCuchara = transform.Find("Frijol en cuchara").gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tortilla"))
        {
            // Desactiva los frijoles al tocar la tortilla
            frijolEnCuchara.SetActive(false);
        }
        else if (other.CompareTag("Plato"))
        {
            // Reactiva los frijoles al tocar el plato
            frijolEnCuchara.SetActive(true);
        }
    }
}