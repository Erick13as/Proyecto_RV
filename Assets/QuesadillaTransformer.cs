using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class QuesadillaTransformer : MonoBehaviour
{
    public QuesadillaMonitor quesadillaMonitor; // Referencia a QuesadillaMonitor

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

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private InputAction buttonAAction;
    private InputAction buttonBAction;
    private InputAction buttonXAction;
    private InputAction buttonYAction;

    private void Awake()
    {
        var actionMap = new InputActionMap("XRI RightHand");

        buttonAAction = actionMap.AddAction("A Button", InputActionType.Button);
        buttonAAction.AddBinding("<XRController>{RightHand}/primaryButton");

        buttonBAction = actionMap.AddAction("B Button", InputActionType.Button);
        buttonBAction.AddBinding("<XRController>{RightHand}/secondaryButton");

        buttonXAction = actionMap.AddAction("X Button", InputActionType.Button);
        buttonXAction.AddBinding("<XRController>{LeftHand}/primaryButton");

        buttonYAction = actionMap.AddAction("Y Button", InputActionType.Button);
        buttonYAction.AddBinding("<XRController>{LeftHand}/secondaryButton");
    }

    private void OnEnable()
    {
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
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
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
        quesadillaMonitor.CerrarQuesadilla(); // Notifica que la quesadilla ha sido doblada
        ActualizarModelo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Queso") && !tieneQueso)
        {
            AgregarQueso();
            Destroy(other.gameObject);
        }

        CambiarModelo scriptCuchara = other.GetComponent<CambiarModelo>();

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

    private void AgregarQueso()
    {
        tieneQueso = true;
        quesadillaMonitor.AgregarQueso(); // Notifica que se ha agregado queso
        ActualizarModelo();
    }

    private void AgregarPollo()
    {
        tienePollo = true;
        quesadillaMonitor.AgregarPollo(); // Notifica que se ha agregado pollo
        ActualizarModelo();
    }

    private void AgregarFrijoles()
    {
        tieneFrijoles = true;
        quesadillaMonitor.AgregarFrijoles(); // Notifica que se han agregado frijoles
        ActualizarModelo();
    }

    private void ActualizarModelo()
    {
        DesactivarTodosLosModelos();

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

        if (modeloAActivar != null) modeloAActivar.SetActive(true);
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

        if (tortillaDoblada != null) tortillaDoblada.SetActive(false);
        if (quesadillaQuesoDoblada != null) quesadillaQuesoDoblada.SetActive(false);
        if (quesadillaPolloDoblada != null) quesadillaPolloDoblada.SetActive(false);
        if (quesadillaFrijolesDoblada != null) quesadillaFrijolesDoblada.SetActive(false);
        if (quesadillaPolloQuesoDoblada != null) quesadillaPolloQuesoDoblada.SetActive(false);
        if (quesadillaFrijolesQuesoDoblada != null) quesadillaFrijolesQuesoDoblada.SetActive(false);
        if (quesadillaPolloFrijolesDoblada != null) quesadillaPolloFrijolesDoblada.SetActive(false);
        if (quesadillaCompletaDoblada != null) quesadillaCompletaDoblada.SetActive(false);
    }
}