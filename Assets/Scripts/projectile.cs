using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Collider[] colliders;

    private int damageToTower = 10;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Detonate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            //Debug.Log("hit tower");
            collision.gameObject.GetComponent<Tower>().ChangeHealth(damageToTower);
        }
        else
        {
            //Debug.Log("hit something else: "+ collision.gameObject);    
        }

        StartCoroutine("DetonateSelf");
    }

    IEnumerator DetonateSelf()
    {
        colliders[1].enabled = true;
        yield return null;
        Destroy(gameObject);
    }
}
