using UnityEngine;
using System.Collections;

public class GameMsgManager : MonoBehaviour {

    [SerializeField]
    private GameObject obj_gameMsg;
    [SerializeField]
    private UILabel lbl_gameMsg;

    private Vector3 destLabelScale = new Vector3(0, 0, 0);
    private Vector3 originScale = new Vector3(64.0f, 64.0f, 0.0f);

    private void SetGameMessage(string _msg) { lbl_gameMsg.text = _msg; }
    public void StartMsg(string _msg)
    {
        SetGameMessage(_msg);
        lbl_gameMsg.gameObject.SetActive(true);
        lbl_gameMsg.gameObject.transform.localScale = originScale;
        StartCoroutine(LblAnimaition());
    }

    private void Ani_ScaleUp()
    {
        Vector3 scaleUp = new Vector3(256.0f, 256.0f, 0.0f);
        iTween.ScaleTo(lbl_gameMsg.gameObject,
            iTween.Hash("scale", scaleUp,
            "name", "MsgScaleUp",
            "time", 1.0f,
            "speed", 100.0f,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.none));
    }
    private void Ani_ScaleDown()
    {
        iTween.ScaleTo(lbl_gameMsg.gameObject,
            iTween.Hash("scale", destLabelScale,
            "name", "MsgScaleDown",
            "time", 1.0f,
            "speed", 110.0f,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.none));
    }

    IEnumerator LblAnimaition()
    {
        Ani_ScaleUp();
        yield return new WaitForSeconds(0.23f);
        Ani_ScaleDown();
        yield return new WaitForSeconds(0.85f);
        lbl_gameMsg.gameObject.SetActive(false);
    }
}
