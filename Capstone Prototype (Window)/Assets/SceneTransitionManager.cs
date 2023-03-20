using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public int sceneIndex;
    public void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(sceneIndex);
    }          
        }
        
}
