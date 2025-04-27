using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//IGNORE - couldn't get it working
public class SpatialAnchorPlacement : MonoBehaviour
{
    public OVRHand hand;
    public GameObject emptyPlayer;
    public GameObject emptyAI;
    public GameObject startButton;
    public GameObject hitButton;
    public GameObject standButton;

    void Update()
    {
        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            CreateSpatialAnchor();
        }
    }

    public void CreateSpatialAnchor()
    {
        GameObject playerCardPoint = Instantiate(hand.gameObject);
    }
}
