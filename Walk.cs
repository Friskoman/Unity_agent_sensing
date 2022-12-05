using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 target;
    private bool isWalking;
    public float walkPointRange;
    private Vector3 CenterPoint = new Vector3(0,1,0);



    void FixedUpdate()
    {
        if(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if(transform.position == target)
        {
            isWalking = false;
        }

        if (!isWalking)
        {
            SearchTargetPoint();
            isWalking = true;
        }

    }

    private void SearchTargetPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        target = new Vector3(randomX, transform.position.y, randomZ);

    }

}
