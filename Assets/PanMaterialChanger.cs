using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public QuesadillaMonitor quesadillaMonitor; // Referencia a QuesadillaMonitor

    private void OnTriggerEnter(Collider other)
    {
        Transform[] allChildren = other.GetComponentsInChildren<Transform>(true);
        bool foundTortilla = false;

        foreach (Transform child in allChildren)
        {
            if (child.CompareTag(TORTILLA_TAG))
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (!objectTimers.ContainsKey(other.gameObject))
                    {
                        objectTimers.Add(other.gameObject, -initialDelay);
                        objectMaterialIndex.Add(other.gameObject, -1);
                        targetRenderers.Add(other.gameObject, renderer);
                        foundTortilla = true;
                        
                        // Notificar al QuesadillaMonitor que la tortilla ha sido colocada en el sartén
                        quesadillaMonitor.AgregarTortilla();
                        Debug.Log("Tortilla colocada en el sartén.");
                    }
                }
            }
        }

        if (!foundTortilla && other.CompareTag(TORTILLA_TAG))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null && !objectTimers.ContainsKey(other.gameObject))
            {
                objectTimers.Add(other.gameObject, -initialDelay);
                objectMaterialIndex.Add(other.gameObject, -1);
                targetRenderers.Add(other.gameObject, renderer);

                // Notificar al QuesadillaMonitor que la tortilla ha sido colocada en el sartén
                quesadillaMonitor.AgregarTortilla();
                Debug.Log("Tortilla colocada en el sartén.");
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

                        if (targetRenderers.ContainsKey(obj) && targetRenderers[obj] != null && 
                            currentIndex < materialTimePairs.Count && 
                            materialTimePairs[currentIndex].material != null)
                        {
                            targetRenderers[obj].material = materialTimePairs[currentIndex].material;

                            // Notificar al QuesadillaMonitor que la tortilla ha avanzado en la cocción
                            if (currentIndex == materialTimePairs.Count - 1)
                            {
                                quesadillaMonitor.CocinarQuesadilla();
                                Debug.Log("La quesadilla ha sido cocinada completamente.");
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        objectTimers.Clear();
        objectMaterialIndex.Clear();
        targetRenderers.Clear();
    }
}
