using Unity.VisualScripting;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    public GameObject[] elements;
    public float gridSize = 0.5f;
    public Transform parent;
    public float delay;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (parent == null)
        {
            parent = GameObject.Find("World").transform;
        }
        Invoke(nameof(Init), delay);
    }

    private void Init() {
        Collider2D c = GetComponent<Collider2D>();
        // For each point in the grid
        for (float x = c.bounds.min.x; x < c.bounds.max.x; x += gridSize)
        {
            for (float y = c.bounds.min.y; y < c.bounds.max.y; y += gridSize)
            {
                // If (x; y) is within collider
                if (!c.OverlapPoint(new Vector2(x, y))) continue;

                // Randomly select an element
                GameObject element = elements[Random.Range(0, elements.Length)];
                // Instantiate the element at the point
                var go = Instantiate(element, new Vector3(x, y, 0), Quaternion.identity, parent);
                go.name = element.name;
            }
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
