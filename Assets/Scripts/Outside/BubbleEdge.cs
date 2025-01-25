using UnityEngine;

public class BubbleEdge : MonoBehaviour
{
    public int pointCount = 10;
    private EdgeCollider2D edgeCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();

        // Places points in a circle around the GameObject pivot
        Vector2[] points = new Vector2[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float angle = i * Mathf.PI * 2 / (pointCount - 1);
            float x = Mathf.Cos(angle) / 2;
            float y = Mathf.Sin(angle) / 2;
            points[i] = new Vector2(x, y);
        }
        edgeCollider.points = points;

        // Closes the edge
        /*
        edgeCollider.adjacentStartPoint = points[^1];
        edgeCollider.useAdjacentStartPoint = true;
        edgeCollider.adjacentEndPoint = points[0];
        edgeCollider.useAdjacentEndPoint = true;
        */
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
