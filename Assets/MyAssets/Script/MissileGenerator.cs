using UnityEngine;
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

	// Use this for initialization
	void Start ()
    {
        missileList = new List<GameObject>();

        positionGroup = generatePosGroup.GetComponentsInChildren<Transform>();
        for(int idx = 0; idx < missileMaxNum; ++idx)
        {
            missileList.Add(Instantiate(missilePrefab,
                Vector3.zero,
                Quaternion.identity) as GameObject);
            missileList[idx].transform.position = positionGroup[Random.Range(0, 19)].position;
            
            EffectSettings settings = missileList[idx].GetComponent<EffectSettings>();
            settings.MoveSpeed = 1.0f;
            settings.Target = missileTarget;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
