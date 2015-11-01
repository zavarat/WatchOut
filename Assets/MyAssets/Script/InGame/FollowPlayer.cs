using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    [SerializeField]
    private Transform playerTransform;
    
    public float upDistance = 7.0f;
    public float backDistance = 10.0f;
    public float trackingSpeed = 3.0f;
    public float rotationSpeed = 9.0f;

    private Vector3 v3To;
    private Quaternion qTo;

    void LateUpdate()
    {
        
        v3To = playerTransform.position - playerTransform.forward * backDistance + playerTransform.up * upDistance;
        transform.position = Vector3.Lerp(transform.position, v3To, trackingSpeed * Time.deltaTime);
        qTo = Quaternion.LookRotation(playerTransform.position - transform.position, playerTransform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
    }
	
}
