using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Player")]
    private int count;
    public float speed = 2;
    public float maxHealth = 100;
    public AudioSource lowHealth;
    [SerializeField] public HealthBar healthBar;

    [Header("Player UI")]
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject nextLevelObject;
    public GameObject gameOver;
    public GameObject nextScene;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float currentHealth;

    [Header("Camera")]
    public Transform cameraTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();

        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void Update()
    {
        // Keep spinning
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape)) //if the escape key is pressed then set active Pause Menu UI
        {
            PauseGame();
        }

        if (currentHealth <= maxHealth * 0.3f)
        {
            if (!lowHealth.isPlaying)
                lowHealth.Play();
        }
        else
        {
            if (lowHealth.isPlaying)
                lowHealth.Stop();
        }

    }
    private void FixedUpdate()
    {
        Vector3 input = new Vector3(movementX, 0.0f, movementY);

        if (input.magnitude > 1f) input.Normalize();
        Vector3 movement = cameraTransform.forward * input.z + cameraTransform.right * input.x;
        movement.y = 0; //lock vertical

        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString() + "/8";

        if(count >= 8)
        {
            nextScene.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            nextLevelObject.SetActive(true);
            winTextObject.SetActive(true);
            youWon = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            youWon = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 20;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
            SoundManager.PlaySound(SoundType.HITSOUND);

            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 pushDirection = transform.position - collision.transform.position;
            rb.AddForce(pushDirection.normalized * 300f);

            if (currentHealth <= 0)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameOver.SetActive(true);
            }
        }

        if (collision.gameObject.CompareTag("PickUp"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //pickup
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            SoundManager.PlaySound(SoundType.COLLECTED);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Heal"))
        {
            currentHealth += 20;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
            SoundManager.PlaySound(SoundType.HEAL);
            other.gameObject.SetActive(false);
        }

        //next level trigger
        if (other.CompareTag("NextTrigger"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }

        //falling out of world
        if (other.CompareTag("Catcher"))
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(currentScene);
        }
    }

    public GameObject PauseMenu;
    public bool youWon = false;

    public void PauseGame()
    {
        bool isPaused = !PauseMenu.activeSelf;
        PauseMenu.SetActive(isPaused);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (isPaused)
        {
            Time.timeScale = 0f;
            winTextObject.SetActive(false);
        }
        else
        {
            PauseEscape();
        }
    }

    public void PauseEscape()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (youWon)
        {
            winTextObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}