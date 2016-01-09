using UnityEngine;
using System.Collections;

public class PC_Controller : MonoBehaviour
{

    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Transform playerTransfrom;
    [SerializeField]
    private Animator playerAnimator;


    private float moveSpeed = 5.0f;
    private float rotSpeed = 3.0f;
    private bool isPlayerGround = true;

    void FixedUpdate()
    {
        MovingCharacter();
        CharacterJump();
        RotationCharacter();
    }

    private void MovingCharacter()
    {
        Vector3 dirVec;
        if (Input.GetKey(KeyCode.UpArrow)) dirVec = playerTransfrom.forward;
        else if (Input.GetKey(KeyCode.RightArrow)) dirVec = playerTransfrom.right;
        else if (Input.GetKey(KeyCode.LeftArrow)) dirVec = -playerTransfrom.right;
        else if ((Input.GetKey(KeyCode.UpArrow)) &&
                (Input.GetKey(KeyCode.RightArrow)))
            dirVec = playerTransfrom.forward + playerTransfrom.right;
        else if ((Input.GetKey(KeyCode.UpArrow)) &&
                (Input.GetKey(KeyCode.LeftArrow)))
            dirVec = playerTransfrom.forward - playerTransfrom.right;
        else
        {
            playerAnimator.SetBool("playerRunStop", true);
            return;
        }
        if (isPlayerGround == true) playerAnimator.Play("Running@loop", 0);
        dirVec *= Time.deltaTime * moveSpeed;
        playerRb.MovePosition(gameObject.transform.position + dirVec);

    }

    private void RotationCharacter()
    {
        float rotX = Input.GetAxisRaw("Horizontal"); ;
        rotX *= rotSpeed;
        Quaternion xQuaternion = Quaternion.AngleAxis(rotX, Vector3.up);
        playerTransfrom.rotation *= xQuaternion;
    }
    private float jumpSpeed = 300.0f;
    public void CharacterJump()
    {
        if ((isPlayerGround == true) && (Input.GetKey(KeyCode.X)))
        {
            playerAnimator.SetBool("playerJumping", true);
            playerAnimator.SetBool("playerRunStop", true);
            playerRb.AddForce(playerTransfrom.up * jumpSpeed);
            isPlayerGround = false;
        }
    }
    // player 지면보다 위에 있는지 확인한다.
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isPlayerGround = true;
            playerAnimator.SetBool("playerJumping", false);
        }
    }
}
