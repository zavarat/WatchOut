using UnityEngine;
//using UnityEngine.Advertisements;
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
    //[SerializeField]
    //private BoxCollider col_joyStick;
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

    [SerializeField]
    private PlayerBuff playerBuffController;

    [SerializeField]
    private TestJoyStick playerJoyStick;

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
        playerJoyStick.SetIsGameStop(true);

        playerBuffController.OnNoEnemyBuff();

        menuObj.SetActive(true);
        spr_menuBg.alpha = 0.7f;
        spr_cancle.alpha = 0.7f;

        isFlickering = true;
        StartCoroutine(FlickeringSprite(spr_exitGame));
    }
    public void CloseMenu()
    {
        playerJoyStick.SetIsGameStop(false);

        menuObj.SetActive(false);
        isFlickering = false;

        playerBuffController.OffNoEnemyBuff();
    }

    // - 광고 막고 그냥 종료하는걸로...
    //게임 종료시 unity ads 를 보여주고 나서 종료.
    public void ExitGame()
    {
        //ShowUnityAds();
        Application.Quit();
    }

    //// 유니티광고를 보여주는 method.
    //public void ShowUnityAds()
    //{
    //    if (Advertisement.IsReady())
    //    {
    //        var options = new ShowOptions { resultCallback = HandleEndGameShowAds };
    //        Advertisement.Show("video", options);
    //    }
    //}
    //// unity-ads Show Result Call back
    //private void HandleEndGameShowAds(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            //Debug.Log("The ad was successfully shown.");
    //            Application.Quit();
    //            break;
    //        case ShowResult.Skipped:
    //            //Debug.Log("The ad was skipped before reaching the end.");
    //            Application.Quit();
    //            break;
    //        case ShowResult.Failed:
    //            //Debug.LogError("The ad failed to be shown.");
    //            Application.Quit();
    //            break;
    //    }
    //}

    public void OpenGameOver()
    {
        playerJoyStick.SetIsGameStop(true);

        //col_joyStick.enabled = false;
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
        playerJoyStick.SetIsGameStop(false);

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
