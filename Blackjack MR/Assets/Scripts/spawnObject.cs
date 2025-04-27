using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject spawnable;
    public AudioClip spawnSound;
    public Transform spawnPoint;
    private AudioSource audiosource;

    void Start()
    {
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.playOnAwake = false;
    }

    public void SpawnObject()
    {
        if(spawnable != null && spawnPoint  != null)
        {
            Instantiate(spawnable, spawnPoint.position, spawnPoint.rotation);
        } else
        {
            Debug.LogError("Object not found.");
        }

        if (spawnSound != null && audiosource != null)
        {
            audiosource.PlayOneShot(spawnSound);
        }
    }

}
