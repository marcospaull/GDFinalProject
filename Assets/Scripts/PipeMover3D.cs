using UnityEngine;

public class PipeMover3D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float destroyX = -15f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
// Destroy the pipe if it goes off-screen
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}