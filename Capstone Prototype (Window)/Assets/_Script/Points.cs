using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Collider))]
public class Points : MonoBehaviour
{
    // text mesh pro that will hold the Number
    [SerializeField]
    TMP_Text PointText;

    // parent object for the text mesh pro that will hold the Number
    [SerializeField]
    private GameObject Point;

    float StartTime;

    //kick bag collider
    Collider col;

    public float MinValue = 0.25f;
    public float MaxValue = 1f;


    private void Awake()
    {
        // when this script get starting , it will get the time for the first frams
        StartTime = Time.time;
    }

    private void Start()
    {
        //cash the Colider component into the Col variable 

        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if we didn't found a colider , or thr Colider is null do nothing
        if (!col.enabled || col == null)
        {
            return;
        }

        // if the time passed since the first frame is less than 0.3 do nothing
        if (Time.time - StartTime < 0.3f)
        {
            return;
        }

        // else Call the SpawnAndMove function 
        SpawnAndMove();

    }
    /// <summary>
    /// This function will instantaite the point amd move it up.
    /// </summary>
    private void SpawnAndMove()
    {
        //instattate the Point
        GameObject PointInstance =  Instantiate(Point, transform.position, Quaternion.identity);
        //set the points value to the TMP
        PointText = PointInstance.GetComponentInChildren<TMP_Text>();
        Debug.Log(PointText);
        PointText.text = "2.0";

    }

}
