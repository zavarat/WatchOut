using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

    [SerializeField]
    private GravityAttractor planetGravity;
    [SerializeField]
    private Rigidbody bodyRd;
    
    void Awake()
    {
        bodyRd.useGravity = false;
        bodyRd.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planetGravity.Attract(gameObject);
    }

    public void SetPlanetGravity(GravityAttractor _planetGravity)
    {
        planetGravity = _planetGravity;
    }

    
}
