using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameplayHand : BaseHand
{
    // The interactor we react to

    [SerializeField] private XRBaseInteractor targetInteractor = null;


    public bool selected;

    private void OnEnable()
    {
        targetInteractor.selectEntered.AddListener(SelectEnter);
        targetInteractor.selectExited.AddListener(TryApplyDefaultPose);
    }

    private void OnDisable()
    {
        targetInteractor.selectEntered.RemoveListener(TryApplyObjectPose);
        targetInteractor.selectExited.RemoveListener(TryApplyDefaultPose);
    }

    private void SelectEnter(SelectEnterEventArgs interactable)
    {

    }

    private void SelectExit(SelectEnterEventArgs interactable)
    {

    }

    public void TryApplyObjectPose(SelectEnterEventArgs interactable)
    {


        GameObject gameObject = interactable.interactableObject.transform.gameObject;

        if (gameObject.TryGetComponent(out IGrabbing grabbed))
        {
            grabbed.OnGrabStart();
        }

        if (gameObject.TryGetComponent(out PoseContainer poseContainer))
        {
            ApplyPose(poseContainer.pose);
        }
        else
        {
            Debug.LogError("OBJECT POSE ERROR");
        }
    }

    public void TryApplyDefaultPose(SelectExitEventArgs interactable)
    {
        selected = false;

        GameObject gameObject = interactable.interactableObject.transform.gameObject;
        if (gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = false;
        }

        if (gameObject.TryGetComponent(out IGrabbing grabbed))
        {
            grabbed.OnGrabEnd();
        }

        if (gameObject.TryGetComponent(out PoseContainer poseContainer))
        {
            ApplyDefaultPose();
        }
        else
        {
            Debug.LogError("OBJECT POSE ERROR");
        }
    }

    public override void ApplyOffset(Vector3 position, Quaternion rotation)
    {
        Vector3 finalPosition = position * -1.0f;
        Quaternion finalRotation = Quaternion.Inverse(rotation);

        finalPosition = finalPosition.RotatePointAroundPivot(Vector3.zero, finalRotation.eulerAngles);
        // Invert since the we're moving the attach point instead of the hand

        // Since it's a local position, we can just rotate around zero
        targetInteractor.attachTransform.localPosition = finalPosition;
        targetInteractor.attachTransform.localRotation = finalRotation;

        // Set the position and rotach of attach
    }

    private void OnValidate()
    {
        // Let's have this done automatically, but not hide the requirement
        if (!targetInteractor)
            targetInteractor = GetComponentInParent<XRBaseInteractor>();
    }
}