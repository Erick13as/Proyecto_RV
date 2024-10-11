using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PanMaterialChanger : MonoBehaviour
{
    [System.Serializable]
    public class MaterialTimePair
    {
        public Material material;
        public float duration;
    }

    public List<MaterialTimePair> materialTimePairs;
    public float initialDelay = 3f; // Tiempo de espera inicial antes de cambiar
    private const string TORTILLA_TAG = "Tortilla";

    private Dictionary<GameObject, float> objectTimers = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, int> objectMaterialIndex = new Dictionary<GameObject, int>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TORTILLA_TAG) && !objectTimers.ContainsKey(other.gameObject))
        {
            objectTimers.Add(other.gameObject, -initialDelay); // Comenzamos en negativo para el retraso inicial
            objectMaterialIndex.Add(other.gameObject, -1); // -1 indica que aún no ha cambiado
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectTimers.ContainsKey(other.gameObject))
        {
            objectTimers.Remove(other.gameObject);
            objectMaterialIndex.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        List<GameObject> objectsToUpdate = new List<GameObject>(objectTimers.Keys);

        foreach (GameObject obj in objectsToUpdate)
        {
            objectTimers[obj] += Time.deltaTime;
            
            if (objectTimers[obj] > 0) // Solo procesamos si ha pasado el tiempo de espera inicial
            {
                int currentIndex = objectMaterialIndex[obj];
                if (currentIndex < materialTimePairs.Count - 1) // Verificamos si no es el último material
                {
                    float timeInCurrentMaterial = objectTimers[obj];
                    if (currentIndex == -1 || timeInCurrentMaterial > materialTimePairs[currentIndex].duration)
                    {
                        // Avanzamos al siguiente material
                        currentIndex++;
                        objectMaterialIndex[obj] = currentIndex;
                        objectTimers[obj] = 0; // Reiniciamos el temporizador para este material

                        // Aplicamos el nuevo material
                        Renderer renderer = obj.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material = materialTimePairs[currentIndex].material;
                        }
                    }
                }
            }
        }
    }
}