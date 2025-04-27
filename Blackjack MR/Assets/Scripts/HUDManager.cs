using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//------------------------------
//------------------------------
//IGNORE - HUD NO LONGER IN USE
//------------------------------
//------------------------------
public class HUDManager : MonoBehaviour
{
    public TMPro.TMP_Text objectNameText;
    public TMPro.TMP_Text objectDescriptionText;
    public Image objectSprite;

    public void UpdateHUD(objectProperties metadata)
    {
        if (metadata.isGrabbed)
        {
            objectNameText.text = (metadata.objectName);
            objectDescriptionText.text = (metadata.objectDescription);
            if (metadata.objectSprite != null)
            {
                objectSprite.sprite = metadata.objectSprite;
                objectSprite.enabled = true;
            }
            else
            {
                objectSprite.enabled = false;
            }
        }
        else
        {
            ClearHUD();
        }
    }

    public void ClearHUD()
    {
        objectNameText.text = "";
        objectDescriptionText.text = "";
        objectSprite.enabled = false;
    }
}

