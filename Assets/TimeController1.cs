using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController1 : MonoBehaviour
{
    [SerializeField] GameObject player;
    public ArrayList playerPositions;
    public bool isReversing = false;

    [SerializeField] int limit = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerPositions = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isReversing = true;
            Reverse();
        }
        else
        {
            isReversing = false;
            print(playerPositions.Count);
            playerpositionCalc();
        }
    }


    void playerpositionCalc()
    {
        if (playerPositions.Count < limit)
        {
            playerPositions.Add(player.transform.position);

        }
        else
        {
            playerPositions.RemoveAt(0);
            playerPositions.Add(player.transform.position);
        }
    }
    /*void FixedUpdate()
    {
        if (!isReversing)
        {
            playerPositions.Add(player.transform.position);
        }
        else if (limit != 0)
        {

            //player.transform.position = (Vector3)playerPositions[playerPositions.Count - 1];
            player.transform.position = (Vector3)playerPositions[positionCount()];
            playerPositions.RemoveAt(playerPositions.Count - 1);
            //Array.Length
           
        }
    } */

    void Reverse()
    { 

        if (playerPositions.Count != 0) {
            player.transform.position = (Vector3)playerPositions[playerPositions.Count - 1];
            playerPositions.RemoveAt(playerPositions.Count - 1);
        }

    }
}
