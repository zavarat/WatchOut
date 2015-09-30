using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;
    private Rigidbody rd;

    float touchX;
    Quaternion xQuaternion;

    public void Attract(GameObject body)
    {
       
        //playerObject.transform.localRotation = originRotPlayer * xQuaternion;
        //originRotPlayer = playerObject.transform.localRotation;

        Vector3 targetDir = (body.transform.position - transform.position).normalized;
        Vector3 bodyUp = body.transform.up;

        if ((Input.touchCount > 0) && (Input.touchCount < 2))
        {
            touchX = Input.GetTouch(0).deltaPosition.x;
            xQuaternion = Quaternion.AngleAxis(touchX, Vector3.up);
            body.transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.transform.rotation * xQuaternion;
        }
        else
            body.transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.transform.rotation;

        rd = body.GetComponent<Rigidbody>();
        rd.AddForce(targetDir * gravity);
        
    }

  
}
