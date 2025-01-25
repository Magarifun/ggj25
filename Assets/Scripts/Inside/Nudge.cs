using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Nudge : MonoBehaviour
{
    public float maxInterval = 10f;
    public float maxNudgeImpulse = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextNudge();
    }

    private void NextNudge()
    {
        Invoke(nameof(DoNudge), Random.Range(0, maxInterval));
    }

    public void DoNudge()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(-maxNudgeImpulse, maxNudgeImpulse), ForceMode2D.Impulse);   
        NextNudge();
    }
}
