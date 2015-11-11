using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;
    
    private Rigidbody objectRd;


    float touchX;
    Quaternion xQuaternion;

    public void Attract(GameObject body)
    {
        Vector3 targetDir = (body.transform.position - transform.position).normalized;
        Vector3 bodyUp = body.transform.up;
        
        body.transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.transform.rotation;

        objectRd = body.GetComponent<Rigidbody>();
        objectRd.AddForce(targetDir * gravity);

    }
}
