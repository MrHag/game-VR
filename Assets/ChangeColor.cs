using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeColor : MonoBehaviour
{
    public GameObject objectToChange;
    private Renderer objectRender;
    public InputActionAsset asset;

    private InputAction activate; 
    // Start is called before the first frame update
    void Start()
    {
        objectRender = objectToChange.GetComponent<Renderer>();
        activate = asset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
        activate.performed += OnActivate;
        activate.canceled += OnActivateCanceled;
    }

    private void OnActivate(InputAction.CallbackContext context)
    {
        objectRender.material.color = Color.red;
    }

    private void OnActivateCanceled(InputAction.CallbackContext context)
    {
        objectRender.material.color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        
        // if (UnityEngine.XR.XRDevice.isPresent)
        //     {
        //         if (SteamVR_Actions.default_GrabPinch.GetStartedDown(SteamVR_Input_Sources.Any))
        //         {
        //             objectToChange.GetComponent<Renderer>().material.color = Color.red;
        //         }
        //     }
    }
}