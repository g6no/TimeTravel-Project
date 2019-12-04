using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public ArrayList gameObjectPositions;
    public ArrayList gameObjectRotations;

    //Variables
    public bool isReversing = false;

    //SerializedFields
    [SerializeField] int limit = 250;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectPositions = new ArrayList();
        gameObjectRotations = new ArrayList();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            SendMessage("UnFreeze");
            //EnemyMovement.isActive = true;
            //gameObject.SetActive(true);
            isReversing = true;
            Reverse();
        }
        else
        {
            isReversing = false;
            //print(gameObjectPositions.Count);
            playerpositionCalc();
        }
    }


    void playerpositionCalc()
    {
        if (gameObjectPositions.Count < limit && gameObjectRotations.Count < limit)
        {
            gameObjectPositions.Add(gameObject.transform.position);
            gameObjectRotations.Add(gameObject.transform.localEulerAngles);

        }
        else
        {
            gameObjectPositions.RemoveAt(0);
            gameObjectPositions.Add(gameObject.transform.position);
            gameObjectRotations.RemoveAt(0);
            gameObjectRotations.Add(gameObject.transform.localEulerAngles);
        }
    }

    void Reverse()
    {

        if (gameObjectPositions.Count != 0) {
            gameObject.transform.position = (Vector3)gameObjectPositions[gameObjectPositions.Count - 1];
            gameObjectPositions.RemoveAt(gameObjectPositions.Count - 1);
        }
        if (gameObjectRotations.Count != 0)
        {
            gameObject.transform.localEulerAngles = (Vector3)gameObjectRotations[gameObjectRotations.Count - 1];
            gameObjectRotations.RemoveAt(gameObjectRotations.Count - 1);
        }
    }

    void DeathSequence()
    {

    }
}
