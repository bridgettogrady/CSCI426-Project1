using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraOffsetY = 0.0f;
    public float smoothSpeed = 5.0f;
    public float topThirdOffset = 0.0f;
    private float currentCameraY;


    // Start is called before the first frame update
    void Start()
    {
        currentCameraY = transform.position.y;
    }

    void LateUpdate()
    {

        if (player != null)
        {
            // Calculate the target Y position for the camera
            float targetY = player.position.y - topThirdOffset;

            // Only move the camera down if the player is below the current camera Y
            if (targetY < currentCameraY)
            {
                // Smoothly interpolate the camera to the new position
                currentCameraY = Mathf.Lerp(currentCameraY, targetY, smoothSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, currentCameraY, transform.position.z);
            }
        }
    
    }
}
