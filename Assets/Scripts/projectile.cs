using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Collider[] colliders;

    public ParticleSystem explosionParticle;
    public AudioClip explosionSound;

    //private AudioSource audio;

    private bool detonated = false;

    private int damageToObject;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<SphereCollider>();
        damageToObject = DoctrineManager.instance.AskLearned("CorrosiveAcid") ? 200 : 100;
        //audio = GetComponent<AudioSource>();
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
            collision.gameObject.GetComponent<Tower>().ChangeHealth(damageToObject);
        }else if (collision.gameObject.CompareTag("Drone"))
        {
            collision.gameObject.GetComponent<Drone>().ChangeHealth(damageToObject);
        }else if (collision.gameObject.CompareTag("Bomb"))
        {
            collision.gameObject.GetComponent<Bomb>().ChangeHealth(damageToObject);

        }
        else
        {
            //Debug.Log("hit something else: "+ collision.gameObject);    
        }
        //Debug.Log("hit something");
        if (!detonated)
        {
            detonated = true;
            StartCoroutine("DetonateSelf");
        }
    }

    IEnumerator DetonateSelf()
    {
        if (DoctrineManager.instance.AskLearned("ExplosiveShot")) 
        {
            colliders[1].GetComponent<SphereCollider>().radius = 10;
        }

        GameObject effect = ExplosionParticlePooler.sharedInstance.GetPooledObject();
        if (effect != null)
        {
            effect.SetActive(true);
            effect.transform.position = transform.position;
            ParticleSystem explosionEffect = effect.GetComponent<ParticleSystem>();
            explosionEffect.Play();
            //AudioSource.PlayClipAtPoint(explosionSound, transform.position, 10.0f);
            //Debug.Log("detonated");
        }

        //explosionParticle.transform.parent = null;
        //explosionParticle.Play();
        colliders[1].enabled = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
