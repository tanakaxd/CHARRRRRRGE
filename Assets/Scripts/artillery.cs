using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artillery : MonoBehaviour
{
    public float shootPower = 100;
    public GameObject projectilePrefab;

    private Rigidbody rb;
    private Rigidbody turretRb;
    private Transform turret;
    private Transform shotPoint;

    private float yAngularVelocity = 10;
    private float xAngularVelocity = 5;
    //private Rigidbody projectileRb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turret = transform.GetChild(0).transform;
        turretRb = turret.GetComponent<Rigidbody>();
        shotPoint = turret.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Rotate();
        //LaunchAngle();
        
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject projectile = Instantiate(projectilePrefab, shotPoint.position, turret.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(projectileRb.transform.forward * shootPower, ForceMode.Impulse);
        }
        
    }

    void Rotate()
    {
        float yangle = Input.GetAxis("Horizontal");
        float xangle = Input.GetAxis("Vertical");
        //turretRbにすると子要素が独立して動いてしまう。子要素つまりturretに対して「動かない」という力が働くから？
        //rigidbodyではなくtransformの方のrotationで動かしている
        rb.rotation = Quaternion.Euler(0, yangle * Time.fixedDeltaTime * yAngularVelocity, 0) * rb.rotation;
        turret.rotation = Quaternion.Euler(-xangle * Time.fixedDeltaTime * xAngularVelocity, 0, 0) * turret.rotation;
    }

    void LaunchAngle()
    {
        float xangle = Input.GetAxis("Vertical");
        Rigidbody turrentRb = turret.GetComponent<Rigidbody>();
        turrentRb.rotation = Quaternion.Euler(xangle * Time.fixedDeltaTime * xAngularVelocity, 0, 0) * turrentRb.rotation;

    }
}
