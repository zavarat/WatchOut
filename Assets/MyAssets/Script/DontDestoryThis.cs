using UnityEngine;
using System.Collections;

public class DontDestoryThis : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
}
