using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class QuesadillaTransformer2 : MonoBehaviour
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

    // Referencias a los modelos doblados
    public GameObject tortillaDoblada;
    public GameObject quesadillaQuesoDoblada;
    public GameObject quesadillaPolloDoblada;
    public GameObject quesadillaFrijolesDoblada;
    public GameObject quesadillaPolloQuesoDoblada;
    public GameObject quesadillaFrijolesQuesoDoblada;
    public GameObject quesadillaPolloFrijolesDoblada;
    public GameObject quesadillaCompletaDoblada;

    // Estado actual de la quesadilla
    private bool tieneQueso = false;
    private bool tienePollo = false;
    private bool tieneFrijoles = false;
    private bool estaDoblada = false;

    // Variables para el manejo de la posición
    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;
    private bool estaEnPlato = false;
    private Coroutine restaurarPosicionCoroutine;

    // Componente para detectar si está siendo agarrada
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    // Referencias a los botones del control
    private InputAction buttonAAction;
    private InputAction buttonBAction;
    private InputAction buttonXAction;
    private InputAction buttonYAction;

    // Materiales originales de los objetos con el tag Tortilla
    private Dictionary<GameObject, Material[]> materialesOriginalesTortilla = new Dictionary<GameObject, Material[]>();

    private void Awake()
    {
        // Crear las acciones de input
        var actionMap = new InputActionMap("XRI RightHand");

        buttonAAction = actionMap.AddAction("A Button", InputActionType.Button);
        buttonAAction.AddBinding("<XRController>{RightHand}/primaryButton");

        buttonBAction = actionMap.AddAction("B Button", InputActionType.Button);
        buttonBAction.AddBinding("<XRController>{RightHand}/secondaryButton");

        buttonXAction = actionMap.AddAction("X Button", InputActionType.Button);
        buttonXAction.AddBinding("<XRController>{LeftHand}/primaryButton");

        buttonYAction = actionMap.AddAction("Y Button", InputActionType.Button);
        buttonYAction.AddBinding("<XRController>{LeftHand}/secondaryButton");

        // Guardar los materiales originales solo para los objetos con el tag Tortilla
        GuardarMaterialesTortilla();
    }

    private void OnEnable()
    {
        // Habilitar las acciones y subscribirse a los eventos
        buttonAAction.Enable();
        buttonBAction.Enable();
        buttonXAction.Enable();
        buttonYAction.Enable();

        buttonAAction.performed += _ => CheckFoldingButton();
        buttonBAction.performed += _ => CheckFoldingButton();
        buttonXAction.performed += _ => CheckFoldingButton();
        buttonYAction.performed += _ => CheckFoldingButton();
    }

    private void OnDisable()
    {
        // Deshabilitar las acciones y desuscribirse de los eventos
        buttonAAction.Disable();
        buttonBAction.Disable();
        buttonXAction.Disable();
        buttonYAction.Disable();

        buttonAAction.performed -= _ => CheckFoldingButton();
        buttonBAction.performed -= _ => CheckFoldingButton();
        buttonXAction.performed -= _ => CheckFoldingButton();
        buttonYAction.performed -= _ => CheckFoldingButton();
    }

    private void Start()
    {
        // Inicializar componentes
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Guardar posición inicial
        posicionOriginal = transform.position;
        rotacionOriginal = transform.rotation;

        // Quesadilla base
        DesactivarTodosLosModelos();
        if (quesadillaBase != null) quesadillaBase.SetActive(true);
    }

    private void CheckFoldingButton()
    {
        if (grabInteractable.isSelected)
        {
            CambiarEstadoDoblado();
        }
    }

    private void CambiarEstadoDoblado()
    {
        estaDoblada = !estaDoblada;
        ActualizarModelo();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto es un plato
        if (other.CompareTag("Plato") && !estaEnPlato && !grabInteractable.isSelected)
        {
            estaEnPlato = true;
            if (restaurarPosicionCoroutine != null)
            {
                StopCoroutine(restaurarPosicionCoroutine);
            }
            restaurarPosicionCoroutine = StartCoroutine(RestaurarPosicionDespuesDeTiempo());
        }

        // Verificar si el objeto es "Queso" y no ha sido agregado aún
        if (other.CompareTag("Queso") && !tieneQueso)
        {
            AgregarQueso();
            Destroy(other.gameObject);
        }

        // Obtener el script de la cuchara para otros ingredientes
        CambiarModelo2 scriptCuchara = other.GetComponent<CambiarModelo2>();

        if (scriptCuchara != null)
        {
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

    private IEnumerator RestaurarPosicionDespuesDeTiempo()
    {
        // Desactivar física
        rb.isKinematic = true;

        yield return new WaitForSeconds(3f);

        // Restaurar posición y rotación originales
        transform.position = posicionOriginal;
        transform.rotation = rotacionOriginal;

        // Reactivar física
        rb.isKinematic = false;

        // Restaurar estado original
        tieneQueso = false;
        tienePollo = false;
        tieneFrijoles = false;
        estaDoblada = false;
        estaEnPlato = false;

        // Restaurar materiales originales de los objetos con el tag Tortilla
        RestaurarMaterialesTortilla();

        // Actualizar modelo a estado original
        ActualizarModelo();
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

        // Seleccionar el modelo correspondiente según ingredientes y estado doblado
        GameObject modeloAActivar = null;

        if (tieneQueso && tienePollo && tieneFrijoles)
        {
            modeloAActivar = estaDoblada ? quesadillaCompletaDoblada : quesadillaCompleta;
        }
        else if (tieneQueso && tienePollo)
        {
            modeloAActivar = estaDoblada ? quesadillaPolloQuesoDoblada : quesadillaPolloQueso;
        }
        else if (tieneQueso && tieneFrijoles)
        {
            modeloAActivar = estaDoblada ? quesadillaFrijolesQuesoDoblada : quesadillaFrijolesQueso;
        }
        else if (tienePollo && tieneFrijoles)
        {
            modeloAActivar = estaDoblada ? quesadillaPolloFrijolesDoblada : quesadillaPolloFrijoles;
        }
        else if (tieneQueso)
        {
            modeloAActivar = estaDoblada ? quesadillaQuesoDoblada : quesadillaQueso;
        }
        else if (tienePollo)
        {
            modeloAActivar = estaDoblada ? quesadillaPolloDoblada : quesadillaPollo;
        }
        else if (tieneFrijoles)
        {
            modeloAActivar = estaDoblada ? quesadillaFrijolesDoblada : quesadillaFrijoles;
        }
        else
        {
            modeloAActivar = estaDoblada ? tortillaDoblada : quesadillaBase;
        }

        // Activar el modelo seleccionado
        if (modeloAActivar != null)
        {
            modeloAActivar.SetActive(true);
        }
    }

    private void DesactivarTodosLosModelos()
    {
        // Desactivar todas las posibles combinaciones de quesadilla
        quesadillaBase.SetActive(false);
        quesadillaQueso.SetActive(false);
        quesadillaPollo.SetActive(false);
        quesadillaFrijoles.SetActive(false);
        quesadillaPolloQueso.SetActive(false);
        quesadillaFrijolesQueso.SetActive(false);
        quesadillaPolloFrijoles.SetActive(false);
        quesadillaCompleta.SetActive(false);

        // Desactivar todos los modelos doblados
        tortillaDoblada.SetActive(false);
        quesadillaQuesoDoblada.SetActive(false);
        quesadillaPolloDoblada.SetActive(false);
        quesadillaFrijolesDoblada.SetActive(false);
        quesadillaPolloQuesoDoblada.SetActive(false);
        quesadillaFrijolesQuesoDoblada.SetActive(false);
        quesadillaPolloFrijolesDoblada.SetActive(false);
        quesadillaCompletaDoblada.SetActive(false);
    }

    private void GuardarMaterialesTortilla()
    {
        // Recorrer todos los hijos y el objeto padre para guardar materiales de los que tienen el tag Tortilla
        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.CompareTag("Tortilla"))
            {
                GuardarMateriales(child.gameObject);
            }
        }

        if (CompareTag("Tortilla"))
        {
            GuardarMateriales(gameObject);
        }
    }

    private void GuardarMateriales(GameObject modelo)
    {
        if (modelo != null)
        {
            Material[] materiales = modelo.GetComponent<Renderer>().materials;
            materialesOriginalesTortilla[modelo] = materiales;
        }
    }

    private void RestaurarMaterialesTortilla()
    {
        foreach (var kvp in materialesOriginalesTortilla)
        {
            GameObject modelo = kvp.Key;
            Material[] materialesOriginales = kvp.Value;
            modelo.GetComponent<Renderer>().materials = materialesOriginales;
        }
    }
}
