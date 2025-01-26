using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float speed = 3;
    // whether or not this platform can move
    private bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && this.tag != "Reset") {
            float move = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right*move*speed*Time.deltaTime);
        }
    }

    public void Activate() {
        activated = true;
    }

    public void Deactivate() {
        activated = false;
    }
}
