using UnityEngine;
using System.Collections;

public class UIManger : MonoBehaviour {

    [SerializeField]
    private GameObject playerObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReverseCameraView()
    {
        Camera.main.transform.RotateAround(playerObject.transform.position, 
            playerObject.transform.up,
            180.0f);
    }


}
