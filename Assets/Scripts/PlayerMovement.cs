using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public Animator animator;
    public GameObject Story1UI;
    public GameObject Story2UI;
    public GameObject Story3UI;
    public GameObject Story4UI;
    public GameObject Story5UI;
    public GameObject Story6UI;
    public GameObject Story7UI;
    public GameObject Story8UI;
    public GameObject PauseMenu;

    private Camera cam;
    private Rigidbody2D rb;
    private CapsuleCollider2D capColl;
    private float inputAxis;
    private Vector2 velocity;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 4f;
    public float maxJumpTime = 1f;
    private float extraHeightText = 0.1f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool falling { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capColl = GetComponent<CapsuleCollider2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        grounded = IsGrounded();
        HorizontalMovement();
        if (grounded)
        {
            VerticalMovement();
        }
        
        ApplyGrounded();
        animationFunction();
        QuitInfoFunction();

        if (Input.GetButtonDown("Cancel"))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
        animator.SetFloat("speed", Mathf.Abs(inputAxis));
        if (grounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            velocity.x = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputAxis, rb.velocity.y);
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 minimumCam = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 maximumCam = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        position.x = Mathf.Clamp(position.x, minimumCam.x + 0.5f, maximumCam.x - 0.5f);
        rb.MovePosition(position);

    }

    private void VerticalMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (grounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            velocity.y = jumpForce;
            jumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    private void ApplyGrounded()
    {
        falling = velocity.y < 0f;
        velocity.y += gravity * 2f * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity);
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycasthit = Physics2D.Raycast(capColl.bounds.center, Vector2.down, capColl.bounds.extents.y + extraHeightText, platformLayerMask);
        Debug.Log(raycasthit.collider);
        return raycasthit.collider != null && raycasthit.rigidbody != rb;
    }

    public void animationFunction()
    {
        if (grounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isGrounded", true);
        }

        if (jumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);
        }

        if (falling)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OffScreen"))
        {
            SceneManager.LoadScene("GameOverScene");
        }

        if (collision.gameObject.name == "StoryBox1")
        {
            Story1UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox2")
        {
            Story2UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox3")
        {
            Story3UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox4")
        {
            Story4UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox5")
        {
            Story5UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox6")
        {
            Story6UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox7")
        {
            Story7UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "StoryBox8")
        {
            Story8UI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.gameObject.name == "FlagPole")
        {
            SceneManager.LoadScene("EndingScene");
        }
    }

    private void QuitInfoFunction()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            Story1UI.SetActive(false);
            Story2UI.SetActive(false);
            Story3UI.SetActive(false);
            Story4UI.SetActive(false);
            Story5UI.SetActive(false);
            Story6UI.SetActive(false);
            Story7UI.SetActive(false);
            Story8UI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
}
