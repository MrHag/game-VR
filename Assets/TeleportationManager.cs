using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;

    [SerializeField] private TeleportationProvider provider;
    private InputAction _thumbstick;

    private bool _isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();

        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
         Debug.Log(cancel);
        cancel.Enable();

        cancel.performed += OnTeleportCancel;

       // var _thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");

        foreach (var item in actionAsset.FindActionMap("XRI LeftHand Locomotion"))
        {
            Debug.Log(item);
        }

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (!_isActive)
            return;

        if (!_thumbstick.triggered)
            return;


        if (!rayInteractor.GetCurrentRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
        }

        var req = new TeleportRequest() { destinationPosition = hit.point };

        provider.QueueTeleportRequest(req);
    }

    void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
    }

    void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
    }
}
