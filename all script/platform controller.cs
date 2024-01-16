using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformcontroller : MonoBehaviour
{
    public Transform posA, posB;
    public int Speed;
    Vector2 targetPos;
    bool movingToPosB = true; // Added to keep track of movement direction

    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 1f)
        {
            targetPos = posB.position;
            movingToPosB = true; // Update movement direction
        }

        if (Vector2.Distance(transform.position, posB.position) < 1f)
        {
            targetPos = posA.position;
            movingToPosB = false; // Update movement direction
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);

        // Flip the platform based on the movement direction
        if (movingToPosB)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // No flipping
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Flip horizontally
        }
    }
}
