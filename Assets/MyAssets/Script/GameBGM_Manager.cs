using UnityEngine;
using System.Collections;

public class GameBGM_Manager : MonoBehaviour {
    [SerializeField]
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource.volume = 0.6f;
        audioSource.Play();
	}

    public void OnBgm()
    {
        audioSource.Play();
    }

    public void OffBgm()
    {
        audioSource.Stop();
    }
	
}
