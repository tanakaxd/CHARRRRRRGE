using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    public GameObject prefab;
    public ParticleSystem smoke;

    private int maxHp = 100;
    private int hp;
    private int xpPerDrone = 150;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private float[] intervals = { 2, 3, 2, 3 };
    private int index;
    private float rebornTime = 8;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        initialRot = rb.rotation;
        //Debug.Log(transform.rotation);
        Debug.Log(rb.rotation);

        hp = maxHp;
        index = 0;
        Rotate();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        HoverAround();
    }

    void HoverAround()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    void Rotate()
    {
        rb.rotation = rb.rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        //Debug.Log(transform.rotation);
        Debug.Log("rotated: "+rb.rotation);

        Invoke("Rotate", intervals[index%4]);
        index++;
    }

    private void OnDisable()
    {
        //StartCoroutine("Reborn");
        CancelInvoke("Rotate");
        Invoke("Reborn", rebornTime);
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

    void Reborn()
    {
        Debug.Log("reborned!");

        gameObject.SetActive(true);
        transform.position = initialPos;
        //Debug.Log(transform.position);
        Debug.Log("before insert rotate:" + rb.rotation);

        rb.rotation = initialRot;
        //Debug.Log(transform.rotation);
        Debug.Log("after insert:" + rb.rotation);

        hp = maxHp;
        index = 0;
        Rotate();
        //Debug.Log(transform.rotation);
        Debug.Log("after rotate:" + rb.rotation);

    }

    public void ChangeHealth(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Engine.instance.UpdateXp(xpPerDrone);
            gameObject.SetActive(false);
        }
    }

    void SmokeScreen()
    {
        smoke.Play();
    }
}
