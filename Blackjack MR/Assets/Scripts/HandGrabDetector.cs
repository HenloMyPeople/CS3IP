using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//------------------------------
//IGNORE - HUD NO LONGER IN USE
//------------------------------
public class HandGrabDetector : MonoBehaviour
{
    public objectProperties metadata;
    public OVRHand leftHand;
    public OVRHand rightHand;
    public float grabDistance = 0.1f;

    private bool isGrabbedByLeftHand = false;
    private bool isGrabbedByRightHand = false;
    private HUDManager hudManager;

    void Update()
    {
        if (IsHandGrabbing(leftHand))
        {
            if (!isGrabbedByLeftHand && IsNearHand(leftHand))
            {
                isGrabbedByLeftHand = true;
                metadata.isGrabbed = true;
                hudManager.UpdateHUD(metadata);
            }
        }
        else if (isGrabbedByLeftHand)
        {
            isGrabbedByLeftHand = false;
            metadata.isGrabbed = false;
            hudManager.ClearHUD();
        }
        if (IsHandGrabbing(rightHand))
        {
            if (!isGrabbedByRightHand && IsNearHand(rightHand))
            {
                isGrabbedByRightHand = true;
                metadata.isGrabbed = true;
                hudManager.UpdateHUD(metadata);
            }
        }
        else if (isGrabbedByRightHand)
        {
            isGrabbedByRightHand = false;
            metadata.isGrabbed = false;
            hudManager.ClearHUD();
        }
    }

    private bool IsHandGrabbing(OVRHand hand)
    {
        return hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }
    private bool IsNearHand(OVRHand hand)
    {
        float distance = Vector3.Distance(hand.transform.position, transform.position);
        return distance <= grabDistance;
    }
}
