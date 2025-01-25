using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    private float startX;
    private float startY;
    public float upwardForce = 10f;
    public float horzForce = 5f;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (col.gameObject.tag == "Bounce")
        {
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        }
        if (col.gameObject.tag == "Left")
        {
            rb.AddForce(Vector2.left * horzForce, ForceMode2D.Impulse);
        }
        if (col.gameObject.tag == "Right")
        {
            rb.AddForce(Vector2.right * horzForce, ForceMode2D.Impulse);
        }
        else if (col.gameObject.tag == "Reset")
        {
            Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(col.transform.position, col.collider.bounds.size, 0);
            bool otherOverlap = false;
            foreach (var collider in overlappingColliders)
            {
                if (collider.gameObject.tag == "Bounce" || collider.gameObject.tag == "Left" || collider.gameObject.tag == "Right")
                {
                    otherOverlap = true;
                    break;
                }
            }
            if (!otherOverlap)
            {
                transform.position = new Vector3(startX, startY, 0);
            }
        }

    }


}

    
