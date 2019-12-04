using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement1 : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    [SerializeField] float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool isFalling = false;
    int loadingTime = 2;
    bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (isActive == true)
        {

            if (Input.GetButtonDown("Jump") && !isFalling)
            {
                jump = true;
                animator.SetBool("isJumping", true);
            }
            else
            {
                jump = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }
    }
        

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("No problem");
                break;
            case "Finish":
                print("Congrats, You won!");
                Invoke("LoadNextScene", loadingTime);
                isActive = false;
                break;
            default:
                print("Lose");
                RepeatCurrentScene();
                isActive = false;
                break;
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
