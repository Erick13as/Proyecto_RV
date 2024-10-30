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
    public float initialDelay = 2f;
    private const string TORTILLA_TAG = "Tortilla";

    private Dictionary<GameObject, float> objectTimers = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, int> objectMaterialIndex = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, Renderer> targetRenderers = new Dictionary<GameObject, Renderer>();

    private void OnTriggerEnter(Collider other)
    {
        // Buscar todos los objetos con tag Tortilla en la jerarquía
        Transform[] allChildren = other.GetComponentsInChildren<Transform>(true);
        bool foundTortilla = false;

        foreach (Transform child in allChildren)
        {
            if (child.CompareTag(TORTILLA_TAG))
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Usar el objeto principal como key para el tracking
                    if (!objectTimers.ContainsKey(other.gameObject))
                    {
                        objectTimers.Add(other.gameObject, -initialDelay);
                        objectMaterialIndex.Add(other.gameObject, -1);
                        targetRenderers.Add(other.gameObject, renderer);
                        foundTortilla = true;
                    }
                }
            }
        }

        // Si no encontramos en los hijos, verificar el objeto principal
        if (!foundTortilla && other.CompareTag(TORTILLA_TAG))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null && !objectTimers.ContainsKey(other.gameObject))
            {
                objectTimers.Add(other.gameObject, -initialDelay);
                objectMaterialIndex.Add(other.gameObject, -1);
                targetRenderers.Add(other.gameObject, renderer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectTimers.ContainsKey(other.gameObject))
        {
            objectTimers.Remove(other.gameObject);
            objectMaterialIndex.Remove(other.gameObject);
            targetRenderers.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        List<GameObject> objectsToUpdate = new List<GameObject>(objectTimers.Keys);

        foreach (GameObject obj in objectsToUpdate)
        {
            if (obj == null)
            {
                objectTimers.Remove(obj);
                objectMaterialIndex.Remove(obj);
                targetRenderers.Remove(obj);
                continue;
            }

            objectTimers[obj] += Time.deltaTime;

            if (objectTimers[obj] > 0)
            {
                int currentIndex = objectMaterialIndex[obj];
                if (currentIndex < materialTimePairs.Count - 1)
                {
                    float timeInCurrentMaterial = objectTimers[obj];
                    if (currentIndex == -1 || timeInCurrentMaterial > materialTimePairs[currentIndex].duration)
                    {
                        currentIndex++;
                        objectMaterialIndex[obj] = currentIndex;
                        objectTimers[obj] = 0;

                        // Usar el renderer guardado
                        if (targetRenderers.ContainsKey(obj) && targetRenderers[obj] != null && 
                            currentIndex < materialTimePairs.Count && 
                            materialTimePairs[currentIndex].material != null)
                        {
                            targetRenderers[obj].material = materialTimePairs[currentIndex].material;
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        // Limpiar las colecciones cuando se desactiva el componente
        objectTimers.Clear();
        objectMaterialIndex.Clear();
        targetRenderers.Clear();
    }
}