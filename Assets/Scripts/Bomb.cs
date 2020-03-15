using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed;
    private int maxHP = 100;
    private int hp;
    private int xpPerBomb = 50;
    private Rigidbody rb;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Artillery");
        rb = GetComponent<Rigidbody>();
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 dir = target.transform.position - transform.position;
        Vector3.Normalize(dir);
        rb.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
    }

    void Detonate()
    {
        Engine.instance.UpdateXp(xpPerBomb);
        Destroy(gameObject);
        //particle system
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Artillery"))
    //    {
    //        Engine.instance.GameOver();
    //        Detonate();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Artillery"))
        {
            Engine.instance.GameOver();
            Detonate();
        }
    }

    public void ChangeHealth(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Detonate();
        }
    }
}
