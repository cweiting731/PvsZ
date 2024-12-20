using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("FollowTarget is not assigned. Please assign it in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 direction = new Vector3(0, 0, 1);
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
