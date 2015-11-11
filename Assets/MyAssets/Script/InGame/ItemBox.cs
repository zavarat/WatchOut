using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

    public enum ITEMBOX_TYPE { SPEED_UP, SHIELD_ON };

    private float persistenceTime = 0.0f;
    private ITEMBOX_TYPE itemType;

    [SerializeField]
    private GameObject pickUpEffect;

    void Start()
    {
        int randType = Random.Range(0, 2); // 0 ~ 1 random

        if (randType == 0) itemType = ITEMBOX_TYPE.SPEED_UP;
        else if (randType == 1) itemType = ITEMBOX_TYPE.SHIELD_ON;

        if (itemType.Equals(ITEMBOX_TYPE.SHIELD_ON))
        {
            persistenceTime = 4.0f;
        }
        else if(itemType.Equals(ITEMBOX_TYPE.SPEED_UP))
        {
            persistenceTime = 5.0f;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // and then box hit animation..
            Instantiate(pickUpEffect, gameObject.transform.position, gameObject.transform.rotation);
            // detroy self
            Destroy(gameObject);
        }
    }

    public float GetPersistenceTime() { return persistenceTime; }
    public ITEMBOX_TYPE GetItemBoxType() { return itemType; }
}
