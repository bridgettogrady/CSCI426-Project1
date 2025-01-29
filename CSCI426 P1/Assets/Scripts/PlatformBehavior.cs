using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float speed = 3;
    public float multiplier = 0.5f;
    // whether or not this platform can move
    private bool activated = false;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) {
            velocity = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right*velocity*speed*Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null && this.tag == "Bounce")
        {
            // Apply movement force to the object based on platform velocity
            rb.velocity += new Vector2(-(velocity * speed * multiplier), 0); // Adjust multiplier for desired effect
        }
    }

    public void Activate() {
        activated = true;
    }

    public void Deactivate() {
        activated = false;
    }
}
