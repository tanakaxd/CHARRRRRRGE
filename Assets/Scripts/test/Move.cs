using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    public AudioClip explosionSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EmitSound();

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
            //Vector3 desired = target.transform.position - transform.position;
            //Vector3 steer = desired - rb.velocity;
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
    }


    void EmitSound()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, 50.0f);
        Invoke("EmitSound", 2.0f);
    }

}
