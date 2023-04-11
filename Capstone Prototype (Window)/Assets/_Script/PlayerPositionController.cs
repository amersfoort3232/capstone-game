using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    public Vector3 fixedPosition;

    void LateUpdate()
    {
        transform.position = fixedPosition;
    }
}