using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Collider[] colliders;

    private int damageToTower;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<SphereCollider>();
        damageToTower = DoctrineManager.instance.AskLearned("CorrosiveAcid") ? 15 : 10;
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
        }else if (collision.gameObject.CompareTag("Guard"))
        {
            collision.gameObject.GetComponent<Guard>().ChangeHealth(damageToTower);
        }
        else
        {
            //Debug.Log("hit something else: "+ collision.gameObject);    
        }

        StartCoroutine("DetonateSelf");
    }

    IEnumerator DetonateSelf()
    {
        if (DoctrineManager.instance.AskLearned("ExplosiveShot")) 
        {
            colliders[1].GetComponent<SphereCollider>().radius = 15;
        }
        colliders[1].enabled = true;
        yield return null;
        Destroy(gameObject);
    }
}
