using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryValue
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerMove : MonoBehaviour {
    private int moveNumber;
    public float speed;
    public float tilt;
    //public BoundaryValue boundary;

    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
    float moveHorizontal;
    float moveVertical;

    float[] randomFloat = new float[] { 0.2f, -0.2f };
    // Use this for initialization
    void Start () {
        moveNumber = 1;
        moveHorizontal = 0f;
        moveVertical = 0f;
        StartCoroutine(move());
    }
	
	// Update is called once per frame
	void FixedUpdate() {
       
        //float moveHorizontal = randomFloat[Random.Range(0, randomFloat.Length)];
       
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, zMin, zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
    IEnumerator move()
    {
        while (true)
        {
            if (moveNumber == 1)
            {
                moveHorizontal = -0.2f;
                yield return new WaitForSeconds(3f);
            }
            else
            {
                if (moveNumber % 2 == 0)
                {
                    moveHorizontal = 0.2f;
                    yield return new WaitForSeconds(6f);
                }
                if (moveNumber % 2 == 1)
                {
                    moveHorizontal = -0.2f;
                    yield return new WaitForSeconds(6f);
                }
            }
            moveNumber++;
        }
    }
}
