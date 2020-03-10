using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Collider[] colliders;
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
        StartCoroutine("DetonateSelf");
    }

    IEnumerator DetonateSelf()
    {
        colliders[1].enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
