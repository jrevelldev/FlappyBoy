using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destroyX = -10f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
}
