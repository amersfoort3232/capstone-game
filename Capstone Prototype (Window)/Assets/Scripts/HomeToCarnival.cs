using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeToCarnival : MonoBehaviour
{
    // Name of the scene you want to switch to
    public int sceneNum = 1;

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider that was touched has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneNum);
        }
    }
}
