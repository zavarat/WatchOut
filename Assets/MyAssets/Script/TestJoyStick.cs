using UnityEngine;
using System.Collections;

public class TestJoyStick : MonoBehaviour {

    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private GameObject playerObject;

    private Vector3 startTouch;
    private Vector3 endTouch;

    private Vector3 dirVector;

    private Ray screenToTouch;
    private RaycastHit rayCastHit;

    private int curFingerID;
    private float maxDist;

	void Start () 
    {
        curFingerID = 9999;
        maxDist = 0.50f;
	}


	void FixedUpdate () 
    {

        if (Input.touchCount == 0) return;
        if (Input.touchCount >= 2) return;

        screenToTouch = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);// -playerObject.transform.forward;

        curFingerID = Input.GetTouch(0).fingerId;

        TouchPhase touchPhase = Input.GetTouch(0).phase;
        switch (touchPhase)
        {
            case TouchPhase.Began:
                if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
                    (rayCastHit.collider.gameObject.tag == "Planet"))
                {
                    Vector3 hitPos = new Vector3(rayCastHit.point.x, rayCastHit.point.y, rayCastHit.point.z);
                    startTouch = hitPos - playerObject.transform.position;
                }
                break;

            case TouchPhase.Moved:
                if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
                    (rayCastHit.collider.gameObject.tag == "Planet"))
                {
                    Vector3 hitPos = new Vector3(rayCastHit.point.x, rayCastHit.point.y, rayCastHit.point.z);
                    endTouch = hitPos - playerObject.transform.position;

                    Vector3 dirVector = endTouch - startTouch;
                    dirVector.Normalize();

                    if (dirVector.sqrMagnitude > maxDist)
                    {
                        playerRb.MovePosition(transform.position + dirVector * Time.deltaTime * 10.0f);
                    }
                       
                }
                else if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
                        (rayCastHit.collider.gameObject.tag == "Player"))
                {
                    playerRb.MovePosition(transform.position + transform.forward * Time.deltaTime * 10.0f);
                   
                }
                break;

            case TouchPhase.Stationary:
                 if (curFingerID != Input.GetTouch(0).fingerId) break;
                 if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
                    (rayCastHit.collider.gameObject.tag == "Planet"))
                 {
                     Vector3 dirVector = endTouch - startTouch;
                     dirVector.Normalize();

                     if (dirVector.sqrMagnitude > maxDist)
                     {
                         playerRb.MovePosition(transform.position + dirVector * Time.deltaTime * 10.0f);
                     }
                 }
                 else if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
                         (rayCastHit.collider.gameObject.tag == "Player"))
                 {
                     playerRb.MovePosition(transform.position + transform.forward * Time.deltaTime * 10.0f);
                 }
                break;
        }
	}
}


