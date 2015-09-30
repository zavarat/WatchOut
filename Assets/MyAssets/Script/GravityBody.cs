using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

    [SerializeField]
    private GravityAttractor planetGravity;
    [SerializeField]
    private Rigidbody playerRd;
    
    void Awake()
    {
        playerRd.useGravity = false;
        playerRd.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planetGravity.Attract(gameObject);
    }

    
}
