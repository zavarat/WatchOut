using UnityEngine;
using System.Collections;

/// <summary>
/// use for U-Chan licenseLogo
/// </summary>
public class LogoProcessing : MonoBehaviour {

    [SerializeField]
    private UISprite spr_Logo;
    private float alphaValue = 0.0f;
    private float transTime = 1.2f;
    void Start () {

        spr_Logo.alpha = alphaValue;
        StartCoroutine(TransParencing());
	}

    IEnumerator TransParencing()
    {
        while(transTime >= 0.0f)
        {
            alphaValue += 0.02f;
            spr_Logo.alpha = alphaValue;

            transTime -= 0.02f;
            yield return new WaitForSeconds(0.02f);
        }

        Application.LoadLevelAsync("ko-jeomStudioLogo");
        
    }
}
