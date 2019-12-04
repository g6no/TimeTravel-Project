using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    //Serialize
    [SerializeField] float speed = 20f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float autoJumpPower = 5f;
    private TimeController timeController;

    bool groundCheck = false;
    private bool m_FacingRight = true;
    bool notDead = true;

    static public float elapsedTime;

    public Animator animator;

    Rigidbody2D rbPlayer;


    // Start is called before the first frame update
    void Start()
    {
       rbPlayer = GetComponent<Rigidbody2D>();
        timeController = FindObjectOfType(typeof(TimeController)) as TimeController;
    }

    void processHMovement()
    {
        float xThrow = Input.GetAxis("Horizontal");
        if (xThrow != 0)
        {
            animator.SetBool("isRunning", true);
            transform.Translate(new Vector2(xThrow,0f) * speed);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (xThrow > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (xThrow < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    void processVMovement()
    {
        if (groundCheck == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("isJumping", true);
                //transform.Translate(new Vector2(0f, 1f) * jumpPower);
                rbPlayer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else
            {
                animator.SetBool("isJumping", false);

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }
        if (collision.gameObject.tag == "Enemies")
        {
            notDead = false;
            animator.SetBool("isRunning", false);
            print("Death");
            //deathAnimation
        }
        if (collision.gameObject.tag == "HitBox")
        {
            print("Hitbox");
            rbPlayer.AddForce(Vector2.up * autoJumpPower, ForceMode2D.Impulse);
            autoJumpPower += 1;
        }
        if (collision.gameObject.tag == "Finish")
        {
            LoadNextScene();
            print("You Won!");
        }
        if (collision.gameObject.tag == "Death")
        {
            Death();
        }
        /*if (collision.gameObject.tag == "Obstacle")
        {
            
        }*/

    }

    IEnumerator Death()
    {
        RepeatCurrentScene();
        yield return new WaitForSeconds(2);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundCheck = false;
        }
    }

    void UnFreeze()
    {
        notDead = true;
        animator.SetBool("isRunning", true);
    }
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            RepeatCurrentScene();
        }

    }
    private void FixedUpdate()
    {
        if (!timeController.isReversing && notDead == true)
        {
            processHMovement();
            processVMovement();
        }
    }
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void RepeatCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void firstLevelReset()
    {
        SceneManager.LoadScene(0);
    }
}
