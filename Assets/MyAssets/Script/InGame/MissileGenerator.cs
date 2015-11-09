﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MissileGenerator : MonoBehaviour {

    private int missileMaxNum = 20;

    [SerializeField]
    private GameObject defaultMisPrefab0;
    [SerializeField]
    private GameObject defaultMisPrefab1;
    private GameObject[] defaultMisArr = new GameObject[2];
    private int defaultNum = 0;
    [SerializeField]
    private GameObject homingMisSlow;
    [SerializeField]
    private GameObject homingMisNormal;
    [SerializeField]
    private GameObject homingMisFast;

    [SerializeField]
    private GameObject targetPlanet;
    [SerializeField]
    private GameObject targetPlayer;
    private List<GameObject> missileList;
    
    [SerializeField]
    private GameObject generatePosGroup;
    private Transform[] positionGroup;
    private int posGroupLength;

    [SerializeField]
    private GameObject generateHomingPosGroup;
    private Transform[] homingPosGroup;
    private int homingPosGroupLength;

    [SerializeField]
    private AudioClip fireMeteoSfx;
    [SerializeField]
    private AudioClip frozenMeteoSfx;
    [SerializeField]
    private AudioClip explosionSfx;
   

	// Use this for initialization
	void Start ()
    {
        
        defaultNum = Random.Range(0, 2);
        defaultMisArr[0] = defaultMisPrefab0;
        defaultMisArr[1] = defaultMisPrefab1;

        missileList = new List<GameObject>();

        positionGroup = generatePosGroup.GetComponentsInChildren<Transform>();
        posGroupLength = positionGroup.Length;

        homingPosGroup = generateHomingPosGroup.GetComponentsInChildren<Transform>();
        homingPosGroupLength = homingPosGroup.Length;

        for(int idx = 0; idx < missileMaxNum; ++idx)
        {
            missileList.Add(Instantiate(defaultMisArr[defaultNum],
                Vector3.zero,
                Quaternion.identity) as GameObject);
            missileList[idx].transform.position = positionGroup[Random.Range(1, posGroupLength)].position;
            missileList[idx].SetActive(false);

            EffectSettings settings = missileList[idx].GetComponent<EffectSettings>();
            settings.MoveSpeed = Random.RandomRange(1.0f, 5.0f);
            settings.Target = targetPlanet;
            settings.CollisionEnter += (n, e) =>
                {
                    // to do
                    if(e.Hit.collider.CompareTag("Planet"))
                    {
                        AudioSource sfxAudio = settings.gameObject.GetComponent<AudioSource>();
                        sfxAudio.clip = explosionSfx;
                        sfxAudio.volume = 0.2f;
                        sfxAudio.Play();
                    }
                    
                };
        }
    }

    public void MissilesSfxOnOff(bool _flag)
    {
        for(int idx = 0; idx < missileMaxNum; ++idx)
        {
            AudioSource sfx = missileList[idx].GetComponent<AudioSource>();
            sfx.mute = _flag;
        }
    }
    

    bool isStartLevel1 = false;
    public void StartLevel1()
    {
        if (isStartLevel1 == true) return;

        int elementNum = 0;
        for(int idx = 0; idx <=2; idx++)
        {
            elementNum = Random.Range(0, 20);
            GameObject deleteObj = missileList[elementNum];
            DestroyImmediate(deleteObj);
            GameObject createObj = Instantiate(homingMisSlow,
                Vector3.zero,
                Quaternion.identity) as GameObject;
            missileList[elementNum] = createObj;
            missileList[elementNum].transform.position = homingPosGroup[Random.Range(0, homingPosGroupLength)].position;


            EffectSettings settings = missileList[elementNum].GetComponent<EffectSettings>();
            settings.MoveSpeed = 1.7f;
            settings.Target = targetPlayer;
            
        }
        isStartLevel1 = true;
    }
    bool isStartLevel2 = false;
    public void StartLevel2()
    {
        if (isStartLevel2 == true) return;

        int elementNum = 0;
        for (int idx = 0; idx <= 4; idx++)
        {
            elementNum = Random.Range(0, 20);
            GameObject deleteObj = missileList[elementNum];
            DestroyImmediate(deleteObj);
            GameObject createObj = Instantiate(homingMisSlow,
                Vector3.zero,
                Quaternion.identity) as GameObject;
            missileList[elementNum] = createObj;
            missileList[elementNum].transform.position = homingPosGroup[Random.Range(0, homingPosGroupLength)].position;

            EffectSettings settings = missileList[elementNum].GetComponent<EffectSettings>();
            settings.MoveSpeed = Random.Range(2.3f, 3.7f);
            settings.Target = targetPlayer;

        }
        isStartLevel2 = true;
    }
    bool isStartLevel3 = false;
    public void StartLevel3()
    {

    }
    

    public void StartMisProcess()
    {
        StartCoroutine(RePositioningMis());
    }

    IEnumerator RePositioningMis()
    {
        while(true)
        {
            missileMaxNum = missileList.Count;
            for (int idx = 0; idx < missileMaxNum; ++idx)
                if (missileList[idx].activeSelf == false)
                {
                    missileList[idx].transform.position = positionGroup[Random.Range(1, posGroupLength)].position;
                    missileList[idx].SetActive(true);
                    AudioSource sfx = missileList[idx].GetComponent<AudioSource>();
                    sfx.clip = frozenMeteoSfx;
                    sfx.volume = 0.3f;
                    sfx.PlayDelayed(0.35f);
                }

                yield return new WaitForSeconds(0.25f);
        }
    }

}
