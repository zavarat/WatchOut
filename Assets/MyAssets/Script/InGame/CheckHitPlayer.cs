using UnityEngine;
using System.Collections;

public class CheckHitPlayer : MonoBehaviour {

    private Animator playerAnimator;
    [SerializeField]
    private UI_Menu uiMenu;

    private bool isPlayerDead = false;

    public void Start()
    {
        isPlayerDead = false;
        playerAnimator = gameObject.GetComponent<Animator>();
    }

	public void HittingPlayer()
    {
        playerAnimator.SetBool("playerHit", true);
        playerAnimator.Play("Damaged@loop", 0);
        playerAnimator.Play("damaged@sd_hmd", 1);
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isPlayerDead == true) return;

        // layer 11 is Missile
        if(collision.collider.transform.gameObject.layer == 11)
        {
            isPlayerDead = true;
            HittingPlayer();
            uiMenu.OpenGameOver();
        }
    }
}
