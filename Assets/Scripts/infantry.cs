using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infantry : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody rb;

    private GameObject target;
    private bool onGround;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        speed = DoctrineManager.instance.AskLearned("WarIsPeace") ? speed*2 : speed;
        dead = false;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        if (onGround && !dead)
        {
            Vector3 desired = target.transform.position - transform.position;
            Vector3 steer = desired - rb.velocity;
            rb.AddForce(steer * speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.CompareTag("Projectile") && !DoctrineManager.instance.AskLearned("IKnowWhatYouDont")) || collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;

        transform.Find("Boximon Fiery").gameObject.SetActive(false);
        transform.Find("DeadBody").gameObject.SetActive(true);
        gameObject.tag = "Dead";
    }

    private void OnCollisionStay(Collision collision)
    {
        onGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
}
