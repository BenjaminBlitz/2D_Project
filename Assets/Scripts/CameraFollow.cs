using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed;
    public PlayerInventory playerStats;
    private void LateUpdate()
    {
        smoothSpeed = 1.2f + (0.2f*playerStats.playerSpeed);
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);

        transform.position = smoothedPosition;

    }
}
