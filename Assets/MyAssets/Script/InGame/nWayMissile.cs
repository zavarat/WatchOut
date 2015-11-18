using UnityEngine;
using System.Collections;

public class nWayMissile : MonoBehaviour {

    private Vector3 dirVector;
    private float speed = 2.0f;

    void Start()
    {
        dirVector = gameObject.transform.forward;
    }

	public void FireMissile()
    {
        StartCoroutine(GoMissile());
    }

    IEnumerator GoMissile()
    {
        Debug.Log("GO");
        while(true)
        {
            gameObject.transform.position += (dirVector * Time.deltaTime * speed);
            
        }
        yield return null;
    }
}
