using UnityEngine;
using System.Collections;

public class PlayerItemManager : MonoBehaviour {

    [SerializeField]
    private GameObject obj_sprint;
    [SerializeField]
    private GameObject obj_shield;

    [SerializeField]
    private UISprite spr_SprintBuff;
    [SerializeField]
    private UISprite spr_ShieldBuff;
    [SerializeField]
    private UILabel lbl_sprintRemainTime;
    [SerializeField]
    private UILabel lbl_shieldRemainTime;

    private bool isShieldOn = false;
    private bool isSprintOn = false;

    private float remainSprintTime = 0.0f;
    private float remainShieldTime = 0.0f;

    [SerializeField]
    private PlayerBuff playerBuff;

    
    private TestJoyStick playerSpeedControl;

	// Use this for initialization
	void Start () {

        playerSpeedControl = gameObject.GetComponent<TestJoyStick>();
	}

    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ItemBox") == false) return;

        ItemBox itemBox = collision.gameObject.GetComponent<ItemBox>();

        if (itemBox.GetItemBoxType().Equals(ItemBox.ITEMBOX_TYPE.SPEED_UP))
        {
            remainSprintTime = itemBox.GetPersistenceTime();
            if(isSprintOn == true)
            {
                remainSprintTime = itemBox.GetPersistenceTime();
            }
            else
            {
                playerBuff.OnSprintBuff();
                playerSpeedControl.SetPlayerSpeed(6.0f);
                obj_sprint.SetActive(true);
                StartCoroutine(CountRemainSprintTime());
                // bool value change first
                isSprintOn = true;
                StartCoroutine(FlickeringSprite(spr_SprintBuff));

            }
        }
        else if(itemBox.GetItemBoxType().Equals(ItemBox.ITEMBOX_TYPE.SHIELD_ON))
        {
            remainShieldTime = itemBox.GetPersistenceTime();
            if(isShieldOn == true)
            {
                remainShieldTime = itemBox.GetPersistenceTime();
            }
            else
            {
                playerBuff.OnShieldBuff();
                obj_shield.SetActive(true);
                StartCoroutine(CountRemainSheildTime());
                // bool value change first
                isShieldOn = true;
                StartCoroutine(FlickeringSprite(spr_ShieldBuff));
            }
        }

    }

    IEnumerator CountRemainSprintTime()
    {
        while(true)
        {
            if(remainSprintTime > 0.0f)
            {
                lbl_sprintRemainTime.text = remainSprintTime.ToString();
                remainSprintTime -= 1.0f;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                lbl_sprintRemainTime.text = "0";
                isSprintOn = false;
                obj_sprint.SetActive(false);
                playerBuff.OffSprintBuff();
                playerSpeedControl.SetPlayerSpeed(4.5f);
                break;
            }
        }
    }

    IEnumerator CountRemainSheildTime()
    {
        while (true)
        {
            if (remainShieldTime > 0.0f)
            {
                lbl_shieldRemainTime.text = remainShieldTime.ToString();
                remainShieldTime -= 1.0f;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                lbl_shieldRemainTime.text = "0";
                isShieldOn = false;
                obj_shield.SetActive(false);
                playerBuff.OffShieldBuff();
                break;
            }
        }
    }

    IEnumerator FlickeringSprite(UISprite _spr)
    {
        float initAlpha = 1.0f;
        _spr.alpha = initAlpha;
        while (true)
        {
            if ((_spr.tag.Equals("shield_buff")) && (isShieldOn == false)) break;
            if ((_spr.tag.Equals("sprint_buff")) && (isSprintOn == false)) break;

            if (_spr.alpha <= 0.5f) _spr.alpha = initAlpha;
            _spr.alpha -= 0.25f;
            yield return new WaitForSeconds(0.25f);
        }
    }
        
}
