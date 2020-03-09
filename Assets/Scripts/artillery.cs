using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artillery : MonoBehaviour
{
    public float shootPower = 100;
    public GameObject projectilePrefab;
    //private Rigidbody projectileRb;
    // Start is called before the first frame update
    void Start()
    {
        //projectileRb = projectilePrefab.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(projectileRb.transform.forward * shootPower, ForceMode.Impulse);
        
    }
}
