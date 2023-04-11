using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksDropping : MonoBehaviour
{
    public Rigidbody[] objects;
    public float bufferTime = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        int objectLength = objects.Length;
        Debug.Log(objectLength);

        float startTime = Time.deltaTime;
        Debug.Log(startTime);

        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(NextScene());
    }
    public void EnableGravity(int index)
    {
        /*
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].useGravity = true;
        }
        */
        objects[index].useGravity = true;

    }
    IEnumerator NextScene()
    {
        float ran = Random.Range(1.0f, 5.0f);

        yield return new WaitForSeconds(3+2*ran);

        //yield return new WaitForSeconds(2 * ran);

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].useGravity = true;
        }
    }
}
