using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

    public enum ITEMBOX_TYPE { SPEED_UP, SHIELD_ON };

    private float persistenceTime = 0.0f;
    private int itemType = 0;

    void Start()
    {
        itemType = Random.Range(0, 2); // 0 ~ 1 random
        if (itemType.Equals(ITEMBOX_TYPE.SHIELD_ON))
        {
            persistenceTime = 3.5f;
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
            
            // detroy self
            DestroyImmediate(gameObject);
        }
    }

    public float GetPersistenceTime() { return persistenceTime; }
    public int GetItemBoxType() { return itemType; }
}
