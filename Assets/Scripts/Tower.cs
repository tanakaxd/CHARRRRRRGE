using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int health;
    private int maxHealth = 1000;

    public GameObject guardPrefab;
    private float spawnInterval = 1;

    public GameObject bulletPrefab;
    private GameObject turretFocal;
    private GameObject turret;
    private GameObject shotPoint;
    private float shootPower =150;
    private float shootInterval = 1;
    private int xpPerTower = 200;
    private Quaternion initialTurretAngle;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        turretFocal = transform.Find("Turret Tower").Find("TurretFocal").gameObject;
        turret = turretFocal.transform.Find("Turret").gameObject;
        //initialTurretAngle = turret.transform.rotation;
        shotPoint = turret.transform.Find("ShotPoint").gameObject;
        Shoot();
        //SpawnGuard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            transform.Find("Compound").gameObject.SetActive(false);
            transform.Find("Turret Tower").gameObject.SetActive(false);
            transform.Find("Slugs").gameObject.SetActive(true);
            CancelInvoke("Shoot");
            Engine.instance.UpdateXp(xpPerTower);
        }
        Debug.Log("current health: " + health);

    }

    bool Aim()
    {
        //GameObject target = GameObject.FindGameObjectWithTag("Infantry");
        if (Engine.instance.Infantiries.Count <= 0) return false;
        int randomIndex = Random.Range(0, Engine.instance.Infantiries.Count);
        //Debug.Log(randomIndex);
        //Debug.Log(Engine.instance.Infantiries.Count);
        GameObject target = Engine.instance.Infantiries[randomIndex];
        if (target == null) return false;
        Vector3 dir = target.transform.position - transform.position;
        turretFocal.transform.rotation =  Quaternion.LookRotation(dir);
        return true;
    }

    void Shoot()
    {
        bool targetable;
        targetable = Aim();
        if (targetable)
        {
            //GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, turret.transform.rotation);

            GameObject bullet = ObjectsPooler.sharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = shotPoint.transform.position;
                //bullet.transform.rotation = turret.transform.rotation;
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(turret.transform.forward * shootPower, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("objectpool vacant!");
            }
        }
        Invoke("Shoot", shootInterval);
    }

    void SpawnGuard()
    {
        GameObject guard = Instantiate(guardPrefab, transform.position + transform.forward * 5, guardPrefab.transform.rotation)as GameObject;
        guard.transform.SetParent(gameObject.transform);
        Invoke("SpawnGuard", spawnInterval);
    }
}
