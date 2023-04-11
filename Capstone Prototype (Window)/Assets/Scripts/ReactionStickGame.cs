using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReactionStickGame : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject prefabToSpawn;

    public SceneManager sceneManager;

    public GameObject player;

    public GameObject startButton;
    public GameObject gameMachine;
    public GameObject sticks;
    public GameObject startPoint;
    public GameObject uiElement;
    public Text scoreText;
    public float dropInterval = 1.5f;

    private bool gameStarted = false;
    private int totalGrabbedSticks = 0;

    public GameObject uiRestartGameElement;

    private void Start()
    {
        scoreText.text = "Sticks Grabbed: " + totalGrabbedSticks.ToString();
        // Disable the game machine and the sticks at the start
        // gameMachine.SetActive(false);
        sticks.SetActive(false);
    }

    private void Update()
    {
        //scoreText.text = "Sticks Grabbed: " + totalGrabbedSticks.ToString();
        // Check if the game has started
        if (gameStarted)
        {          
            // Check if it's time to drop a stick
            if (Time.timeSinceLevelLoad % dropInterval < Time.deltaTime)
            {
                // Enable a random stick to be dropped
                int randomStickIndex = Random.Range(0, sticks.transform.childCount);
                Transform randomStick = sticks.transform.GetChild(randomStickIndex);
                randomStick.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    public void StartGame()
    {
        uiElement.SetActive(true);
        uiRestartGameElement.SetActive(true);

        // Disable the start button and enable the game machine and sticks
        startButton.SetActive(false);
        //gameMachine.SetActive(true);
        sticks.SetActive(true);

        // Teleport the player to the game machine
        //Vector3 startingPosition = new Vector3(startPoint.position.x, player.transform.position.y, startPoint.position.z);
        //player.transform.position = startingPosition;


        // Set gameStarted to true after 5s
        StartCoroutine(ExampleCoroutine());
    }

    public void AddGrabbedStick(GameObject stick)
    {
        // Disable the stick and increase the totalGrabbedSticks count
        totalGrabbedSticks++;
        //stick.SetActive(false);

        // Update the score text
        scoreText.text = "Sticks Grabbed: " + totalGrabbedSticks.ToString();
    }

    public void OnButtonPressed()
    {
        Debug.Log("button pressed");
        totalGrabbedSticks = 0;
        StartGame();
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5.0f);
        gameStarted = true;
    }

    public void RestartGame(){
        SceneManager.LoadScene("reaction");
    }
}
