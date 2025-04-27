using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//IGNORE CLASS - META SDK Integrated 
public class followHead : MonoBehaviour
{
    void Start()
    {
        
    }

    
    public Transform playerCamera;
    void Update()
    {
     if (playerCamera != null)
        {
            transform.LookAt(playerCamera);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
