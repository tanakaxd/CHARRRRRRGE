using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnBound : MonoBehaviour
{
    private Vector3 center;
    float bound = 500;
    float sqrBound;

    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(0, 0, 0);
        sqrBound = bound * bound;

    }

    // Update is called once per frame
    void Update()
    {
        float sqrDistance = Vector3.SqrMagnitude(transform.position - center);
        if (sqrDistance >= sqrBound)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
