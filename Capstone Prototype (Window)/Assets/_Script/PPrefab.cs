using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPrefab : MonoBehaviour
{
    [SerializeField]
    public float MoveSpeed { get; private set; }

    /// <summary>
    /// in this update function we are Moving the point TMP up with MoveSpeed on Up Direction
    /// and Start the Couretuine which will start after 5 sec and destroy the point object.
    /// </summary>
    private void Update()
    {
        MoveSpeed = 1.4f;
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Vector3.up * 10f, step);
        StartCoroutine(pointCycle());
    }

    IEnumerator pointCycle()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
