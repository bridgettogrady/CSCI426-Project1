using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    private float startX;
    private float startY;
    public float upwardForce = 10f;
    public float horzForce = 5f;
    public AudioSource bounce;
    public AudioSource woosh;
    public AudioSource lose;
    private Rigidbody2D rb;
    public Sprite sad;
    private Sprite normal;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        normal = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -10 || transform.position.x > 10)
        {
            transform.position = new Vector3(startX, startY, 0);
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bounce")
        {
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
            bounce.Play();
        }
        if (col.gameObject.tag == "Left")
        {
            rb.AddForce(Vector2.left * horzForce, ForceMode2D.Impulse);
            woosh.Play();
        }
        if (col.gameObject.tag == "Right")
        {
            rb.AddForce(Vector2.right * horzForce, ForceMode2D.Impulse);
            woosh.Play();
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
                spriteRenderer.sprite = sad;
                lose.Play();
                Invoke("Reset", 1.5f);
            }
        }

    }

    private void Reset()
    {
        spriteRenderer.sprite = normal;
        transform.position = new Vector3(startX, startY, 1f);
        rb.velocity = Vector3.zero;
    }


}

    
