using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    public float upDistance = 7.0f;
    public float backDistance = 10.0f;
    public float trackingSpeed = 3.0f;
    public float rotationSpeed = 9.0f;

    private Vector3 v3To;
    private Quaternion qTo;

    private Vector3 dirVec;

    void LateUpdate()
    {
        dirVec = target.position - transform.position;
        dirVec.Normalize();

        v3To = target.position - dirVec * backDistance + target.up * upDistance;
        transform.position = Vector3.Lerp(transform.position, v3To, trackingSpeed * Time.deltaTime);
        qTo =  Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
    }
	
}
