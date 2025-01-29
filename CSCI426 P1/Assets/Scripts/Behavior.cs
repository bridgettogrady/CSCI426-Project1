using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Behavior : MonoBehaviour
{
    // camera component
    public GameObject camera;

    private float startX;
    private float startY;
    public float upwardForce = 10f;
    public float horzForce = 5f;
    public AudioSource bounce;
    public AudioSource woosh;
    public AudioSource lose;
    public AudioSource win;
    private Rigidbody2D rb;
    public Sprite sad;
    private Sprite normal;
    private SpriteRenderer spriteRenderer;
    public Image tintPanel;
    private float pulseDuration = 0.2f;
    private float pulseScale = 1.1f;
    public GameObject leftArrow;
    public GameObject rightArrow;
    private float lifetime = 1f;
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
            StartCoroutine(PulseEffect(col.gameObject));
        }
        if (col.gameObject.tag == "Left")
        {
            SpawnLeftArrow(transform.position);
            rb.AddForce(Vector2.left * horzForce, ForceMode2D.Impulse);
            woosh.Play();
        }
        if (col.gameObject.tag == "Right")
        {
            SpawnRightArrow(transform.position);
            rb.AddForce(Vector2.right * horzForce, ForceMode2D.Impulse);
            woosh.Play();
        }
        if (col.gameObject.tag == "Win")
        {
            tintPanel.color = new Color(0, 1f, 0, 0.137f);
            win.Play();
            Invoke("Reset", 3f);
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
                tintPanel.color = new Color(1f, 0, 0, 0.255f);
                lose.Play();
                Invoke("Reset", 1.5f);
            }
        }

    }

    private void Reset()
    {
        tintPanel.color = new Color(1, 1, 1, 0);
        spriteRenderer.sprite = normal;
        transform.position = new Vector3(startX, startY, 1f);
        rb.velocity = Vector3.zero;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PulseEffect(GameObject platform)
    {
        Vector3 originalScale = platform.transform.localScale;
        Vector3 targetScale = originalScale * pulseScale;
        float elapsedTime = 0f;
        while (elapsedTime < pulseDuration / 2)
        {
            platform.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / (pulseDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        platform.transform.localScale = targetScale;
        elapsedTime = 0f;
        while (elapsedTime < pulseDuration / 2 )
        {
            platform.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / (pulseDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        platform.transform.localScale = originalScale;
    }

    void SpawnLeftArrow(Vector3 pos)
    {
        GameObject arrow = Instantiate(leftArrow, pos, Quaternion.identity);
        Destroy(arrow, lifetime);
    }

    void SpawnRightArrow(Vector3 pos)
    {
        GameObject arrow = Instantiate(rightArrow, pos, Quaternion.identity);
        Destroy(arrow, lifetime);
    }

}

    
