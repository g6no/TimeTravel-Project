using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float enemySpeed = 20f;
    [SerializeField] float deathPosition;
    int yRotation = 180;
    bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            processMovement();
        }
    }

    void processMovement()
    {
        transform.Translate(new Vector2(1f, 0f) * enemySpeed);
    }

    void makeActive()
    {
        isActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            //enemySpeed = -enemySpeed;
            print(collision.gameObject.tag);
            transform.rotation = Quaternion.Euler(0,yRotation,0);
            yRotation += 180;
        }
        else if(collision.gameObject.tag == "Player")
        {
            isActive = false;
            transform.Translate(new Vector2(0f, deathPosition));        
        }
        else
        {
            print(collision.gameObject.tag);
            //isActive = true;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            //enemySpeed = -enemySpeed;
            print(collision.gameObject.tag);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            yRotation += 180;
        }
        else
        {
            return;
        }
    }
}
