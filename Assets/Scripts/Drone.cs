using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    public GameObject prefab;
    //public ParticleSystem smoke;

    private int maxHp = 600;
    private int hp;
    private int xpPerDrone = 300;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private float[] intervals = { 4, 1, 4, 1 };
    private int index;
    private float rebornTime = 8;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        //initialRot = Quaternion.Euler(0,180,0);
        initialRot = rb.rotation;
        //Debug.Log(transform.rotation);
        //Debug.Log(rb.rotation);

        hp = maxHp;
        index = 0;
        Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        //HoverAround();

    }
    private void FixedUpdate()
    {
        HoverAround();
    }

    void HoverAround()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        //rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    void Rotate()
    {
        rb.rotation = rb.rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        //Debug.Log(transform.rotation);
        //Debug.Log("rotated: "+rb.rotation);

        Invoke("Rotate", intervals[index%4]);
        index++;
    }

    private void OnDisable()
    {
        //StartCoroutine("Reborn");
        CancelInvoke();
        //Invoke("ReInstantiate", rebornTime);
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().SpawnDrone();
    }

    //IEnumerator Reborn()
    //{
    //    yield return new WaitForSeconds(rebornTime);
    //    gameObject.SetActive(true);
    //    transform.position = prefab.transform.position;
    //    transform.rotation = prefab.transform.rotation;
    //    hp = maxHp;
    //    index = 0;
    //    Rotate();
    //}

    void ReInstantiate()
    {
        Instantiate(prefab);
    }

    void Reborn()
    {
        //Debug.Log("reborned!");

        gameObject.SetActive(true);
        transform.position = initialPos;
        //Debug.Log(transform.position);
        //Debug.Log("before insert rotate:" + rb.rotation);

        rb.rotation = initialRot;
        //Debug.Log(transform.rotation);
        //Debug.Log("after insert:" + rb.rotation);

        hp = maxHp;
        index = 0;
        Rotate();
        //Debug.Log(transform.rotation);
        //Debug.Log("after rotate:" + rb.rotation);

    }

    public void ChangeHealth(int amount)
    {
        if (DoctrineManager.instance.AskLearned("ElecctroMagneticPulse"))
        {
            hp = 0;
        }
        else
        {
            hp -= amount;   
        }
        if (hp <= 0)
        {
            Engine.instance.UpdateXp(xpPerDrone);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void SmokeScreen()
    {
        //smoke.Play();
    }
}
