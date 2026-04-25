using UnityEngine;

public class Rock3D : MonoBehaviour
{
    // Rigidbody for physics (movement, gravity)
    public Rigidbody rb;

    // Force applied when player presses space (jump upward)
    public float RiseForce = 8f;

    // Audio sources for jump and collision
    public AudioSource flapSound;
    public AudioSource hitSound;

    // Particle effect that plays when scoring
    public GameObject scoreParticleEffect;

    // Animator for playing attack animations
    public Animator animator;

    // Reference to GameManager
    private GameManager gameManager;

    // Tracks if the player is still alive
    private bool alive = true;

    void Start()
    {
        // Auto-assign Rigidbody if not set in Inspector
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        // Auto-assign AudioSource if not set
        if (flapSound == null)
            flapSound = GetComponent<AudioSource>();

        // Auto-assign Animator if not set
        if (animator == null)
            animator = GetComponent<Animator>();

        // Find GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Stop all input if player is dead
        if (!alive) return;

        // When player presses Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply upward movement
            rb.velocity = Vector3.up * RiseForce;

            // Play jump sound
            if (flapSound != null)
                flapSound.Play();

            // Handle animation if Animator exists
            if (animator != null)
            {
                // Reset all attack triggers so animations don't overlap
                animator.ResetTrigger("Attack1");
                animator.ResetTrigger("Attack2");
                animator.ResetTrigger("Attack3");

                // Pick a random animation (0, 1, or 2)
                int rand = Random.Range(0, 3);

                if (rand == 0)
                {
                    // Debug message for testing
                    Debug.Log("Attack1 pressed");

                    // Trigger Attack1 animation
                    animator.SetTrigger("Attack1");
                }
                else if (rand == 1)
                {
                    Debug.Log("Attack2 pressed");

                    // Trigger Attack2 animation
                    animator.SetTrigger("Attack2");
                }
                else
                {
                    Debug.Log("Attack3 pressed");

                    // Trigger Attack3 animation
                    animator.SetTrigger("Attack3");
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Prevent multiple collisions after death
        if (!alive) return;

        // Mark player as dead
        alive = false;

        // Play hit sound
        if (hitSound != null)
            hitSound.PlayOneShot(hitSound.clip);

        // Delay GameOver slightly so sound can play first
        Invoke(nameof(CallGameOver), 0.15f);
    }

    // Separate function so it can be delayed with Invoke()
    void CallGameOver()
    {
        if (gameManager != null)
            gameManager.GameOver();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player passed through scoring zone
        if (other.CompareTag("Scoring"))
        {
            // Increase score
            if (gameManager != null)
                gameManager.AddScore();

            // Spawn particle effect at scoring position
            if (scoreParticleEffect != null)
            {
                GameObject effect = Instantiate(
                    scoreParticleEffect,
                    other.transform.position,
                    Quaternion.identity
                );

                // Destroy particle after 2 seconds to avoid clutter
                Destroy(effect, 2f);
            }
        }
    }
}