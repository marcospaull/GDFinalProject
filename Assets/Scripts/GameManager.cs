using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool gameOver = false; // tracks if game ended
    public int score = 0;         // player score
    private bool isPaused = false; // pause state
    private bool musicOn = true;   // music toggle

    [Header("Frenzy Mode")]
    public float frenzyDuration = 5f;
    public float frenzyCooldown = 10f;
    public float frenzySpeedBoost = 1.5f;
    public TextMeshProUGUI frenzyText;

    public bool isFrenzy { get; private set; } = false;
    public float pipeSpeedMultiplier { get; private set; } = 1f;
    private int scoreMultiplier = 1;
    private float frenzyTimer = 0f;
    private float cooldownTimer = 0f;
    private bool onCooldown = false;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextShadow;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject settingsMenu;
    public GameObject settingsButton;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource scoreSound;

    void Start()
    {
        // Make sure game runs normally at start
        Time.timeScale = 1f;

        // Initialize UI elements
        if (scoreText != null)
            scoreText.text = "0";

        if (gameOverText != null)
            gameOverText.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);

        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (settingsButton != null)
            settingsButton.SetActive(true);

        // Make sure music starts unmuted
        if (musicSource != null)
            musicSource.mute = false;

        scoreMultiplier = 1;
        UpdateFrenzyUI();
    }

    void Update()
    {
        // Do nothing if game is over
        if (gameOver) return;

        // Press ESC to pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            // Press F to activate Frenzy Mode
            if (Input.GetKeyDown(KeyCode.F) && !isFrenzy && !onCooldown)
                ActivateFrenzy();

            if (isFrenzy)
            {
                frenzyTimer -= Time.deltaTime;
                UpdateFrenzyUI();
                if (frenzyTimer <= 0f)
                    DeactivateFrenzy();
            }

            if (onCooldown)
            {
                cooldownTimer -= Time.deltaTime;
                UpdateFrenzyUI();
                if (cooldownTimer <= 0f)
                {
                    onCooldown = false;
                    UpdateFrenzyUI();
                }
            }
        }
    }

    void ActivateFrenzy()
    {
        isFrenzy = true;
        scoreMultiplier = 2;
        frenzyTimer = frenzyDuration;
        pipeSpeedMultiplier = frenzySpeedBoost;
        UpdateFrenzyUI();
    }

    void DeactivateFrenzy()
    {
        isFrenzy = false;
        scoreMultiplier = 1;
        onCooldown = true;
        cooldownTimer = frenzyCooldown;
        pipeSpeedMultiplier = 1f;
        UpdateFrenzyUI();
    }

    void UpdateFrenzyUI()
    {
        if (frenzyText == null) return;

        if (isFrenzy)
            frenzyText.text = $"2X: {Mathf.CeilToInt(frenzyTimer)}s";
        else if (onCooldown)
            frenzyText.text = $"Cooldown: {Mathf.CeilToInt(cooldownTimer)}s";
        else
            frenzyText.text = "F Ready!";
    }

    // Called when player scores
    public void AddScore()
    {
        if (gameOver) return;

        score += scoreMultiplier;

        // Update UI text
        if (scoreText != null)
            scoreText.text = score.ToString();
        if (scoreTextShadow != null)
            scoreTextShadow.text = score.ToString();

        // Play score sound
        if (scoreSound != null)
            scoreSound.Play();
    }

    // Called when player loses
    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        isPaused = false;
        isFrenzy = false;
        onCooldown = false;
        scoreMultiplier = 1;
        pipeSpeedMultiplier = 1f;
        if (frenzyText != null) frenzyText.text = "";

        // Show game over UI
        if (gameOverText != null)
            gameOverText.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);

        // Hide settings
        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (settingsButton != null)
            settingsButton.SetActive(false);

        // Freeze game
        Time.timeScale = 0f;
    }

    // Toggle pause state
    public void TogglePause()
    {
        isPaused = !isPaused;

        // Show/hide settings menu
        if (settingsMenu != null)
            settingsMenu.SetActive(isPaused);

        if (settingsButton != null)
            settingsButton.SetActive(!isPaused);

        // Pause or resume game time
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Resume game manually
    public void ResumeGame()
    {
        isPaused = false;

        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (settingsButton != null)
            settingsButton.SetActive(true);

        Time.timeScale = 1f;
    }

    // Turn music on/off
    public void ToggleMusic()
    {
        musicOn = !musicOn;

        if (musicSource != null)
            musicSource.mute = !musicOn;
    }

    // Restart the scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
