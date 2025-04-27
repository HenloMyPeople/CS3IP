using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawn : MonoBehaviour
{
    public GameObject[] spawnables;
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
        if (spawnables != null && spawnables.Length > 0 && spawnPoint != null)
        {
            int randomIndex = Random.Range(0, spawnables.Length);
            GameObject selectedPrefab = spawnables[randomIndex];
            Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Objects missing");
        }
        if (spawnSound != null && audiosource != null)
        {
            audiosource.PlayOneShot(spawnSound);
        }
    }
}
