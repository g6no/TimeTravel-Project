using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float frogJumpPower = 5f;
    [SerializeField] float frogJumpWait = 2f;
    bool isActive = true;

    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FrogJump());
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FrogJump()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(frogJumpWait);
            rb.AddForce(Vector2.up * frogJumpPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(frogJumpWait);
            //gameObject.transform.position = startPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isActive = false;
            transform.Translate(new Vector2(0f, -2f));
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
        else
        {
            print(collision.gameObject.tag);
            //isActive = true;
            return;
        }
    }

    void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints2D.None;

    }
}
