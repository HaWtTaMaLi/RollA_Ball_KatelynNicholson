using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //Body
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    //UI
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject gameOver;
    public GameObject nextScene;
    //Health
    public float maxHealth = 100;
    public float currentHealth;
    [SerializeField] public HealthBar healthBar;
    public AudioClip collisionSound;

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
        if (Input.GetKeyDown(KeyCode.Escape)) //if the escape key is pressed then set active Pause Menu UI
        {

            PauseGame();
            
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();

        if(count >= 8)
        {
            nextScene.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 20;
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);

            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 pushDirection = transform.position - collision.transform.position;
            rb.AddForce(pushDirection.normalized * 500f);

            if (currentHealth <= 0)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameOver.gameObject.SetActive(true);
            }
        }

        if (collision.gameObject.CompareTag("PickUp"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;
            SetCountText();
        }

        if (other.CompareTag("NextTrigger"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
    }

    public GameObject PauseMenu;

    public void PauseGame()
    {
        //Debug.Log("Escape Key Pressed");
        PauseMenu.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseEscape()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}