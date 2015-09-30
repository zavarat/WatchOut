using UnityEngine;
using System.Collections;

public class TestGravity : MonoBehaviour {

    [SerializeField]
    private GameObject objectPos;
    [SerializeField]
    private Rigidbody objectRb;
    [SerializeField]
    private GameObject planetPos;

    private Vector3 dirVector;
    private float distance;

	void Start()
    {
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        dirVector = objectPos.transform.position - planetPos.transform.position;
        distance = dirVector.sqrMagnitude;
        if(distance > 0.001f)
        {
            dirVector.Normalize();
            objectRb.AddForce((dirVector / distance) * -9.81f);     	
        }
	}
}
