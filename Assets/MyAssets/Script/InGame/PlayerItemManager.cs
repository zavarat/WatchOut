using UnityEngine;
using System.Collections;

public class PlayerItemManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ItemBox") == false) return;

        ItemBox itemBox = collision.gameObject.GetComponent<ItemBox>();
        if (itemBox.GetItemBoxType().Equals(ItemBox.ITEMBOX_TYPE.SPEED_UP))
        {
            // player buff on, and then buff persistence time counter start ( co-routine)
        }
        else if(itemBox.GetItemBoxType().Equals(ItemBox.ITEMBOX_TYPE.SHIELD_ON))
        {

        }

    }
        
}
