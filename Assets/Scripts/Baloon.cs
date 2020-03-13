using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 10;
    private int xpPerPotion = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fall();
    }

    void Fall()
    {
        //rb.MovePosition(transform.position - transform.up * speed * Time.fixedDeltaTime);
        //rb.AddForce(-transform.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger: "+other);
        if (other.gameObject.CompareTag("Projectile"))
        {
            Engine.instance.UpdateXp(xpPerPotion);
        }
        Destroy(gameObject);
    }
}
