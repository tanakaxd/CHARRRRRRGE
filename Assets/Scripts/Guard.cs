using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private Rigidbody rb;

    private bool underHallucination = false;

    private int maxHp = 30;
    private int hp;

    private bool isJumping = false;

    public float explosionForce = 3000;
    public float explosionRadius = 10;
    public float jumpForce = 3000;
    public float jumpInterval = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = maxHp;
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {
        rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        Invoke("Jump", jumpInterval);
    }

    void Attack()
    {

    }

    void Explode()
    {
        Debug.Log("explode called");
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }

    public void ChangeHealth(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);
        Debug.Log(hp);
        if (hp <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Infantry") && !underHallucination)
        {
            collision.gameObject.GetComponent<infantry>().Die();
        }
        if (collision.gameObject.CompareTag("Projectile") && DoctrineManager.instance.AskLearned("Hallucination"))
        {
            underHallucination = true;
        }
    }

}
