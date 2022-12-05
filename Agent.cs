using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] Sight sight;
    [SerializeField] LayerMask layerMask;


    void Awake()
    {
        sight.OnEnterVision += EnterVision;
        sight.OnLeaveVision += LeftVision;
    }

    void LeftVision(object sender, VisionEventArgs args)
    {
        Debug.Log($"{gameObject.name}no longer sighted: {args.collider.gameObject.name}");
    }


    void EnterVision(object sender, VisionEventArgs args)
    {
        Debug.Log($"{gameObject.name}Sighted: {args.collider.gameObject.name}");
        if (args.collider.gameObject.layer == 6)
        {
            if (args.collider.gameObject != null)
            {    
                Destroy(args.collider.gameObject);
            } 
            
        }
            
    }

    void FixedUpdate()
    {
        UpdateState();
        Reason();
        act();
    }

    void UpdateState(){
        foreach(Collider collider in sight.inSight)
        {

        }
    }
    void Reason()
    {

    }
    void  act()
    {

    }
   
}
