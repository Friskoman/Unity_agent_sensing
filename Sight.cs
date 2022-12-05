using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionEventArgs
{
    public Collider collider;
}

public class Sight : MonoBehaviour
{
    const Modality MODALITY = Modality.SIGHT;

    [SerializeField] float angle = 60f;
    [SerializeField] float distance = 20f;
    [SerializeField] float aspect = 2f;
    [SerializeField] Color gizmoColor = Color.red;
    [SerializeField] LayerMask layerMask;
    HashSet<Collider> lastInSight;
    internal HashSet<Collider> inSight;

    public delegate void VisionEventHandler(object sender, VisionEventArgs args);
    public event VisionEventHandler OnEnterVision, OnLeaveVision;
    float coneRadius;
    
    // Start is called before the first frame update
    void Awake() {
        inSight = new HashSet<Collider>();
        coneRadius = Mathf.Tan(Mathf.Deg2Rad * angle / 2f) * distance;
    }

    Collider coneHitCollider;
    RaycastHit[] coneHits;
    RaycastHit hitInfo;

    void FixedUpdate() {
        lastInSight = new HashSet<Collider>(inSight);

        // overlaps = Physics.OverlapSphere(transform.position, distance, layerMask);
        // foreach (collider c in overlaps) {
        coneHits = ConeCastExtension.ConeCastAll(transform.position, coneRadius, transform.forward, distance, angle);

        foreach (RaycastHit coneHit in coneHits){
            coneHitCollider = coneHit.collider;
            
            if(Physics.Raycast(transform.position, coneHitCollider.transform.position - transform.position, out hitInfo, distance, layerMask)
                && hitInfo.collider == coneHitCollider){
                if(!inSight.Contains(coneHitCollider)) {
                    inSight.Add(coneHitCollider);
                    OnEnterVision.Invoke(this, new VisionEventArgs() { collider = coneHitCollider});
                } else {
                    lastInSight.Remove(coneHitCollider);
                }
            }
        }

        foreach (Collider c in lastInSight) {
            inSight.Remove(c);
            OnLeaveVision.Invoke(this, new VisionEventArgs() { collider = coneHitCollider});
        }
    }


    void OnDrawGizmos() {
        if(inSight != null){
            Gizmos.color = Color.magenta;
            foreach(Collider c in inSight){
                Gizmos.DrawLine(transform.position, c.transform.position);
            }
        }

        Gizmos.color = gizmoColor;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawFrustum(Vector3.zero, angle, distance, .5f, aspect);
    }
}