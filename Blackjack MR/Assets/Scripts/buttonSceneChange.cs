using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonSceneChange : MonoBehaviour
{
    public void ChangeSceneOnPressToMR()
    {
        Debug.Log("Changing Scene");
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
    }

    public void ChangeSceneOnPressToVR()
    {
        Debug.Log("Changing Scene");
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
    }
}
