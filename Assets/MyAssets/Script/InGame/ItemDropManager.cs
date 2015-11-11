using UnityEngine;
using System.Collections;

public class ItemDropManager : MonoBehaviour {

    [SerializeField]
    private GameObject itemDropPosGroup;
    private Transform[] itemDropPosList = new Transform[8];
    private int itemDropPosLength = 0;

    [SerializeField]
    private ItemBox itemBox_origin;
    [SerializeField] // itemBox를 끌어당길 planet
    private GravityAttractor planetGravity;

    private int curItemBoxes = 0;

	// Use this for initialization
	void Start () {
        // scene 별로 planet이 다르므로, 아이템박스가 가져야할 planet을 스크립트로 제어한다.
        GravityBody itemGravityBody = itemBox_origin.GetComponent<GravityBody>();
        itemGravityBody.SetPlanetGravity(planetGravity);

        itemDropPosList = itemDropPosGroup.GetComponentsInChildren<Transform>();
        itemDropPosLength = itemDropPosList.Length;
	}

    public int GetCurItemBoxes() { return curItemBoxes; }

    public void CreateItemBoxes(int _createNumber)
    {
        for(int idx = 0; idx < _createNumber; idx++)
        {
            Transform createPos = itemDropPosList[Random.Range(1, itemDropPosLength)];
            Instantiate(itemBox_origin,
                createPos.position,
                new Quaternion(0,0,0,0));

            curItemBoxes++;
        }
    }
	
}
