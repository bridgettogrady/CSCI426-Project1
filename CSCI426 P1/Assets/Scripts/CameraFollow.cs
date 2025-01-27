using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraOffsetY = 0.0f;
    public float smoothSpeed = 5.0f;

    private float initialCameraY;

    // Start is called before the first frame update
    void Start()
    {
        initialCameraY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player != null) {
            // only move if player is below center of screen
            if (player.position.y < transform.position.y + cameraOffsetY) {
                Vector3 targetPos = new Vector3(transform.position.x, player.position.y - cameraOffsetY, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
            }

            // prevent camera from moving up
            if (transform.position.y > initialCameraY) {
                transform.position = new Vector3(transform.position.x, initialCameraY, transform.position.z);
            }
        }
    
    }
}
