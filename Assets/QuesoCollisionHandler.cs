using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesoCollisionHandler : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isCollidingWithQuesadilla = false;

    void Start()
    {
        // Almacena la posición original
        originalPosition = transform.position;
    }

    void Update()
    {
        // Si está en colisión, regresa el queso a la posición original
        if (isCollidingWithQuesadilla)
        {
            transform.position = originalPosition;
            isCollidingWithQuesadilla = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si colisiona con el objeto "Quesadilla"
        if (other.CompareTag("Quesadilla"))
        {
            isCollidingWithQuesadilla = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Cuando el queso deja de colisionar con la quesadilla
        if (other.CompareTag("Quesadilla"))
        {
            isCollidingWithQuesadilla = false;
        }
    }
}
