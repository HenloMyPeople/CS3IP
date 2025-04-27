using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public ColorType acceptedColor;
    public AudioClip correctDropSound;
    public AudioClip invalidDropSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CubeColor cube = other.GetComponent<CubeColor>();
        if (cube != null)
        {
            if (cube.cubeColor == acceptedColor)
            {
                Debug.Log("Correct drop!");
                if (correctDropSound != null)
                {
                    audioSource.PlayOneShot(correctDropSound);
                }
                Destroy(cube.gameObject);
            }
            else
            {
                Debug.Log("Incorrect drop!");
                audioSource.PlayOneShot(invalidDropSound);
            }
        }
    }
}
