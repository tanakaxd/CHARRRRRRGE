using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject GuardPrefab;
    public GameObject bombPrefab;

    private int maxHp = 3;
    private int hp;

    private float bombInterval = 20;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        //ShootBomb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootBomb()
    {
        Instantiate(bombPrefab, transform.position, transform.rotation);
        Invoke("ShootBomb", bombInterval);
        bombInterval-=1;
        bombInterval = Mathf.Clamp(bombInterval, 3, 20);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Infantry"))
        {
            ChangeHealth(1);
            collision.gameObject.GetComponent<infantry>().Die();
        }
    }

    public void ChangeHealth(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Engine.instance.GameClear();
        }
    }
}
