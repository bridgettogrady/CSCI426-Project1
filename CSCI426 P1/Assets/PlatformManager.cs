using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // assign in inspector
    public int platformCount = 5;
    public float platformY = 3.0f;
    public float platformXBoundary = 9.0f;
    private List<GameObject> platforms = new List<GameObject>();
    private int highlightIndex = 0;
    private string lastTag;
    private List<string> tags = new List<string>{"Bounce", "Left", "Right"};

    // Start is called before the first frame update
    void Start()
    {
        // instantiate platforms vertically
        for (int i = 0; i < platformCount; i++) {
            // get random X
            float randomX = Random.Range(-platformXBoundary, platformXBoundary);

            GameObject platform = Instantiate(platformPrefab, new Vector3(randomX, -i * platformY, 0.0f), Quaternion.identity);
            platform.transform.parent = transform;

            // if it's the first platform, can't be a reset
            if (i == 0) {
                platform.tag = tags[Random.Range(0, 2)];
            }
            // randomly generate tag number
            else {
                platform.tag = GetRandomTag();
            }

            // assign color
            SpriteRenderer renderer = platform.GetComponent<SpriteRenderer>();
            switch (platform.tag) {
                case "Reset":
                    renderer.color = Color.red;
                    break;
                case "Bounce":
                    renderer.color = Color.blue;
                    break;
                case "Left":
                case "Right":
                    renderer.color = Color.green;
                    break;
            }

            platforms.Add(platform);
        }

        // highlight first platform, set lastTag, and activate first platform
        HighlightPlatform();
        lastTag = platforms[highlightIndex].tag;
        foreach (GameObject platform in platforms) {
            PlatformBehavior behavior = platform.GetComponent<PlatformBehavior>();
            if (platform.tag == platforms[highlightIndex].tag) {
                behavior.Activate();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // going up
        if (Input.GetKeyDown(KeyCode.W)) {
            ChangeHighlight(highlightIndex - 1);
        }
        // going down
        if (Input.GetKeyDown(KeyCode.S)) {
            ChangeHighlight(highlightIndex + 1);
        }

        // activate/deactivate tagged groups
        string currTag = platforms[highlightIndex].tag;
        if (currTag != lastTag) {
            foreach (GameObject platform in platforms) {
                PlatformBehavior behavior = platform.GetComponent<PlatformBehavior>();
                if (platform.tag == lastTag) {
                    behavior.Deactivate();
                }
                if (platform.tag == currTag) {
                    behavior.Activate();
                }
            }
        }

        // update lastTag
        lastTag = currTag;
    }

    private void HighlightPlatform() {
        GameObject highlight = platforms[highlightIndex].transform.Find("Highlight").gameObject;
        SpriteRenderer renderer = highlight.GetComponent<SpriteRenderer>();
        renderer.enabled = true;
    }

    private void ChangeHighlight(int index) {
        // un-highlight
        GameObject highlight = platforms[highlightIndex].transform.Find("Highlight").gameObject;
        SpriteRenderer renderer = highlight.GetComponent<SpriteRenderer>();
        renderer.enabled = false;

        if (index != platforms.Count && index != -1) {
            highlightIndex = index;
        }

        // set renderer to new platform
        highlight = platforms[highlightIndex].transform.Find("Highlight").gameObject;
        renderer = highlight.GetComponent<SpriteRenderer>();
        renderer.enabled = true;
    }

    /* I don't want reset platforms to spawn as often so this functions
    skews the odds */
    private string GetRandomTag() {
        // give reset a 1/3 chance
        if (Random.value < 0.33f) {
            return "Reset";
        }
        else {
            int randomIndex = Random.Range(0, 2);
            return tags[randomIndex];
        }
    }
}
