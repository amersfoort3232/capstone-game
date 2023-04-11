using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public ReactionStickGame game;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The player has grabbed the stick, notify the game
            game.AddGrabbedStick(gameObject);
            gameObject.SetActive(false);
            Debug.Log("grabbed");
        }
        else
        {
            // The stick has been dropped on the floor, disable it
            gameObject.SetActive(false);
        }
    }
}
