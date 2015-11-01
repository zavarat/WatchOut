using UnityEngine;
using System.Collections;

public class LogoProcessKJ : MonoBehaviour {

    [SerializeField]
    private UISprite spr_Logo;
    [SerializeField]
    private UILabel lbl_logoName;
    private float alphaValue = 0.0f;
    private float transTime = 1.2f;
    void Start()
    {
        lbl_logoName.alpha = alphaValue;
        spr_Logo.alpha = alphaValue;
        StartCoroutine(TransParencing());
    }

    IEnumerator TransParencing()
    {
        while (transTime >= 0.0f)
        {
            alphaValue += 0.02f;
            spr_Logo.alpha = alphaValue;
            lbl_logoName.alpha = alphaValue;

            transTime -= 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        Application.LoadLevelAsync("MainMenu");
    }
}
