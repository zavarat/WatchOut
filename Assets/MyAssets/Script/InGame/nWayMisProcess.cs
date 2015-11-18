using UnityEngine;
using System.Collections;

public class nWayMisProcess : MonoBehaviour {

    private nWayMissile[] nWayMissiles;
    private int nWayMisLength = 0;

    void Start()
    {
        // idx 0은 최상위계층, 따라서 1 to length 까지 이다. 
        nWayMissiles = gameObject.GetComponentsInChildren<nWayMissile>();
        nWayMisLength = nWayMissiles.Length;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Planet"))
        {
            Debug.Log("Hit Planet, nWayMis");
            for (int idx = 1; idx < nWayMisLength; ++idx)
            {
                nWayMissiles[idx].FireMissile();
            }
        }
        
    }
}
