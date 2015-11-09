using UnityEngine;
using System.Collections;
using System.IO;

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
    private UserDataProcess userDataProcess;
    private UserGameData userGameData;
    [SerializeField]
    private UILabel lbl_curRecord;
    [SerializeField]
    private UILabel lbl_lastRecord;

    // gameOver popup -> 다른 collider는 false ( 임시코드).
    [SerializeField]
    private BoxCollider col_joyStick;
    [SerializeField]
    private BoxCollider col_jump;
    [SerializeField]
    private BoxCollider col_menuPop;

    [SerializeField]
    private UISprite spr_sfx;
    [SerializeField]
    private UISprite spr_bgm;
    [SerializeField]
    private MissileGenerator misGenerator;
    [SerializeField]
    private GameBGM_Manager gameBgm;

    bool isOnSfx = true;
    public void OnOffSfx()
    {
        if(isOnSfx == true)
        {
            spr_sfx.spriteName = "SoundOff_red";
            misGenerator.MissilesSfxOnOff(true);
            isOnSfx = false;
        }
        else
        {
            spr_sfx.spriteName = "SoundOn";
            misGenerator.MissilesSfxOnOff(false);
            isOnSfx = true;
        }
    }
    bool isOnBgm = true;
    public void OnOffBGM()
    {
        if(isOnBgm == true)
        {
            spr_bgm.spriteName = "MusicOff_red";
            gameBgm.OffBgm();
            isOnBgm = false;
        }
        else
        {
            spr_bgm.spriteName = "Music";
            gameBgm.OnBgm();
            isOnBgm = true;
        }
    }

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
        col_joyStick.enabled = false;
        col_jump.enabled = false;
        col_menuPop.enabled = false;

        gameTimer.SetGameTimeState(GameTimer.GAME_TIME_STATE.TIME_END);

        //for windows and editor save&load commands.
        if((Application.platform == RuntimePlatform.WindowsPlayer) ||
        (Application.platform == RuntimePlatform.WindowsEditor))
        {
            userDataProcess = new UserDataProcess();
            userDataProcess.InitData();
            // 먼저 기존에 존재하는 시간기록을 가져온다.
            userDataProcess.LoadUserDataForWindows();
            userGameData = userDataProcess.GetUserGameRecord();
            lbl_lastRecord.text = userGameData.GetGameTime().ToStringType();

            // 현재 기록한 시간을 저장한다.
            userGameData.SetGameTime(gameTimer.GetGameTimeInfo());
            userDataProcess.SaveUserDataForWindows(userGameData);
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            string timeRecord = PlayerPrefs.GetString("PlayerTimeRecord");
            if (timeRecord == "") lbl_lastRecord.text = "00:00:00";
            else lbl_lastRecord.text = timeRecord;
            PlayerPrefs.SetString("PlayerTimeRecord", gameTimer.GetGameTimeString());
        }
        
        // 현재 기록한 시간을 UI에 보여준다.
        lbl_curRecord.text = gameTimer.GetGameTimeString();
        
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
