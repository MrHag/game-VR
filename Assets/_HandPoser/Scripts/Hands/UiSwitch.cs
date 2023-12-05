using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UiSwitch : MonoBehaviour
{

    [SerializeField] private ActionBasedController targetController = null;

    [SerializeField] private GameObject uiObject = null;

    [SerializeField] private GameplayHand hand = null;

    private void UIPressed(InputAction.CallbackContext context)
    {
        uiObject.SetActive(true);
    }

    private void UIReleased(InputAction.CallbackContext context)
    {
        uiObject.SetActive(false);
    }

    private void OnEnable()
    {
        targetController.uiPressAction.action.performed += UIPressed;
        targetController.uiPressAction.action.canceled += UIReleased;
    }

    // private void Update()
    // {

    //     if (targetController != null)
    //     {


    //         var state = targetController.uiPressInteractionState;
    //         if (state.activatedThisFrame)
    //         {
    //             UIPressed();
    //         }
    //         else
    //         if (state.deactivatedThisFrame)
    //         {
    //             UIReleased();
    //         }
    //     }
    // }
}
