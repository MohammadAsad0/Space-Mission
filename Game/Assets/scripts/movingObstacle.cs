using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class movingObstacle : MonoBehaviour
{
    [SerializeField] Vector3 movingVector = new Vector3(10f, 0f, 0f);
    Vector3 startingPos;
    [SerializeField] float movingRate = 3f;
    [Range(0, 1)] [SerializeField] float movingFactor;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "level4Obstacle")
        {
            if ((gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled)
            {
                Invoke("level4Obstacle", 2f);
            }
        }
        else
        {
            level3Obstacle();
        }
    }

    private void level4Obstacle()
    {
        (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;
    }

    private void level3Obstacle()
    {
        float speed = Time.time / movingRate;
        float value = Mathf.Sin(Mathf.PI * 2f * speed);
        movingFactor = (value / 2f) + 0.5f;
        Vector3 abc = movingVector * movingFactor;
        transform.position = startingPos + abc;
    }
}
