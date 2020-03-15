using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artillery : MonoBehaviour
{
    public float shootPower = 100;
    public GameObject projectilePrefab;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private Text bulletText;

    private Rigidbody rb;
    private Rigidbody turretRb;
    private Transform turret;
    private Transform shotPoint;

    private float yAngularVelocity = 10;
    private float xAngularVelocity = 5;
    private float deltaTimePerProjectile = 0.2f;
    private float coolDownTime = 4.0f;
    private bool onCoolDown = false;
    private bool isFiring = false;
    private int projectilePerShot = 4;
    private int bulletCharge;
    public int ProjectilePerShot { get { return projectilePerShot; } set { projectilePerShot = value; } }
    public float CoolDownTime { get {return coolDownTime; } set { coolDownTime = value; } }

    //private Rigidbody projectileRb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        turret = transform.GetChild(0).transform;
        turretRb = turret.GetComponent<Rigidbody>();
        shotPoint = turret.GetChild(0).transform;

        audioSource = GetComponent<AudioSource>();

        bulletText = GameObject.Find("BulletCharge").GetComponent<Text>();

        bulletCharge = projectilePerShot;
    }

    // is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !onCoolDown && !isFiring)
        {
            StartCoroutine("Shoot");
        }
        Rotate();
        //LaunchAngle();
    }

    //void Update()
    //{
    //    Shoot();
    //    Rotate();
    //    //LaunchAngle();

    //}

    IEnumerator Shoot()
    {
        if (Input.GetKeyDown(KeyCode.R) && !onCoolDown)
        {
            GameObject projectile = Instantiate(projectilePrefab, shotPoint.position, turret.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(projectileRb.transform.forward * shootPower, ForceMode.Impulse);
            UpdateBullet(1);
            audioSource.PlayOneShot(shootSound, 0.7f);
            if (bulletCharge <= 0)
            {
                onCoolDown = true;
                yield return new WaitForSeconds(coolDownTime);
                onCoolDown = false;
                bulletCharge = projectilePerShot;
                UpdateBullet(0);
            }
        }

    }

    //IEnumerator Shoot()
    //{
    //    isFiring = true;
    //    for (int i = 0; i < projectilePerShot; i++)
    //    {
    //        GameObject projectile = Instantiate(projectilePrefab, shotPoint.position, turret.rotation);
    //        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
    //        projectileRb.AddForce(projectileRb.transform.forward * shootPower, ForceMode.Impulse);
    //        yield return new WaitForSeconds(deltaTimePerProjectile);
    //    }
    //    isFiring = false;

    //    onCoolDown = true;
    //    yield return new WaitForSeconds(coolDownTime);
    //    onCoolDown = false;
    //}

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

    void UpdateBullet(int amount)
    {
        bulletCharge-=amount;
        bulletText.text = bulletCharge + " / " + projectilePerShot;

    }
}
