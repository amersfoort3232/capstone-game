using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifferentPointSystem : MonoBehaviour
{
    // Variable declaration
    [SerializeField] GameObject Point;
    private TMP_Text PointText;
    [SerializeField] int Pointer;
    [SerializeField] GameObject ResultObj;
    Result _result;

    private void Awake()
    {
        // cash the result Gameobject in awake insted of calling it evey frame
        _result = ResultObj.GetComponent<Result>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // if the Collider het a game object with Box Tag on Do the Following
        if (other.gameObject.tag == "Box")
        {
            // call SpawnAndMove Function
            SpawnAndMove(other.gameObject.transform.position);
            //send the Value of the pointer to the Result Class to preview
            _result.PutValue(Pointer);
        }
    }

    private void SpawnAndMove(Vector3 place)
    {
        //instattate the Point
        GameObject PointInstance = Instantiate(Point, place, Quaternion.identity);
        //set the points value to the TMP
        PointText = PointInstance.GetComponentInChildren<TMP_Text>();
        // preview the pointer Number
        PointText.text = Pointer.ToString();

    }
}
