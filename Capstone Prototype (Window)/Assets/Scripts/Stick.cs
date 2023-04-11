using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Rigidbody[] objects;
    public float buffer;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WaitAndDrop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDrop()
    {
        StartCoroutine(WaitAndDrop());
    }

    IEnumerator WaitAndDrop()
    {
        float ran = Random.Range(1.0f, 5.0f);

        yield return new WaitForSeconds(5 + buffer);

        //yield return new WaitForSeconds(2 * ran);

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].useGravity = true;
        }
    }
}
