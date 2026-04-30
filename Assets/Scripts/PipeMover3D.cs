using UnityEngine;

public class PipeMover3D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float destroyX = -15f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        float speed = moveSpeed * (gameManager != null ? gameManager.pipeSpeedMultiplier : 1f);
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
