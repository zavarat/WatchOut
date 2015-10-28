using UnityEngine;
using System.Collections;

public class UIManger : MonoBehaviour {

    [SerializeField]
    private GameObject playerObject;

    public void ReverseCameraView()
    {
        Camera.main.transform.RotateAround(playerObject.transform.position, 
            playerObject.transform.up,
            180.0f);
    }


}
