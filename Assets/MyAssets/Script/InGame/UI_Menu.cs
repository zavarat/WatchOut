using UnityEngine;
using System.Collections;

public class UI_Menu : MonoBehaviour {

    [SerializeField]
    private UISprite spr_menuBg;
    [SerializeField]
    private UISprite spr_cancle;
    [SerializeField]
    private UISprite spr_exitGame;
    [SerializeField]
    private GameObject menuObj;

    [SerializeField]
    private GameObject gameOverObj;
    [SerializeField]
    private UISprite spr_gameOverBg;
    [SerializeField]
    private UISprite spr_backToMenuBg;

    private bool isFlickering = true;

    [SerializeField]
    private GameTimer gameTimer;

    public void OpenMenu()
    {
        
        menuObj.SetActive(true);
        spr_menuBg.alpha = 0.7f;
        spr_cancle.alpha = 0.7f;

        isFlickering = true;
        StartCoroutine(FlickeringSprite(spr_exitGame));
    }
    public void CloseMenu()
    {
        menuObj.SetActive(false);
        isFlickering = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenGameOver()
    {
        gameTimer.SetGameTimeState(GameTimer.GAME_TIME_STATE.TIME_END);

        gameOverObj.SetActive(true);
        spr_gameOverBg.alpha = 0.7f;

        isFlickering = true;
        StartCoroutine(FlickeringSprite(spr_backToMenuBg));
    }

    public void GoToMainMenu()
    {
        isFlickering = false;
        GameObject skydomeObj = GameObject.FindGameObjectWithTag("SkyDome");
        DestroyImmediate(skydomeObj);

        Application.LoadLevelAsync("MainMenu");
    }

    IEnumerator FlickeringSprite(UISprite _spr)
    {
        float initAlpha = 1.0f;
        _spr.alpha = initAlpha;
        while(isFlickering)
        {
            if (_spr.alpha <= 0.5f) _spr.alpha = initAlpha;
            _spr.alpha -= 0.25f;
            yield return new WaitForSeconds(0.25f);
        }
    }
	
}
