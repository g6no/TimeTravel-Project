using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {

        enemies = GameObject.FindGameObjectsWithTag("HitBox");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            foreach (GameObject enemy in enemies)
            {
                print(enemy);
                enemy.SetActive(true);
            }
        }
    }
}
