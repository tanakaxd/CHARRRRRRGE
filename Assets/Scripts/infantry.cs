using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infantry : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody rb;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        Vector3 desired = target.transform.position - transform.position;
        Vector3 steer = desired - rb.velocity;
        rb.AddForce(steer * speed * Time.fixedDeltaTime);
    }
}
