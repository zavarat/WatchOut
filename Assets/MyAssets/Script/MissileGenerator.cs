using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MissileGenerator : MonoBehaviour {

    public int missileMaxNum = 20;

    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private GameObject missileTarget;
    private List<GameObject> missileList;
    
    [SerializeField]
    private GameObject generatePosGroup;
    private Transform[] positionGroup;
    private int posGroupLength;

	// Use this for initialization
	void Start ()
    {
        missileList = new List<GameObject>();

        positionGroup = generatePosGroup.GetComponentsInChildren<Transform>();
        posGroupLength = positionGroup.Length;

        for(int idx = 0; idx < missileMaxNum; ++idx)
        {
            missileList.Add(Instantiate(missilePrefab,
                Vector3.zero,
                Quaternion.identity) as GameObject);
            missileList[idx].transform.position = positionGroup[Random.Range(1, posGroupLength)].position;
            
            EffectSettings settings = missileList[idx].GetComponent<EffectSettings>();
            settings.MoveSpeed = Random.RandomRange(1.0f, 5.0f);
            settings.Target = missileTarget;
            settings.CollisionEnter += (n, e) =>
                {
                };
        }

        StartCoroutine(RePositioningMis());
        
    }

    IEnumerator RePositioningMis()
    {
        while(true)
        {
            for (int idx = 0; idx < missileMaxNum; ++idx)
                if (missileList[idx].activeSelf == false)
                {
                    missileList[idx].transform.position = positionGroup[Random.Range(1, posGroupLength)].position;
                    missileList[idx].SetActive(true);
                }

                yield return new WaitForSeconds(0.25f);
        }
    }

}
