using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAround : MonoBehaviour
{
    float speed = 50 ;
    public Transform objectToLookAt;

    void FixedUpdate()
    {
        Vector3 direction = objectToLookAt.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
  
    }

}
