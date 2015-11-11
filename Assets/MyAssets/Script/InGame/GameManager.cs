using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameTimer gameTimer;
    [SerializeField]
    private MissileGenerator misGenerator;
    [SerializeField]
    private UILabel lbl_gameStartTimer;
    private float countTime = 3.0f;

    [SerializeField]
    private ItemDropManager itemDropManager;

    [SerializeField]
    private GameMsgManager gameMsgManager;

    [SerializeField]
    private UI_Menu ui_Menu;

    [SerializeField]
    private GameObject factedDomeSky0;
    [SerializeField]
    private GameObject factedDomeSky1;
    [SerializeField]
    private GameObject factedDomeSky2;
    [SerializeField]
    private GameObject factedDomeSky3;
    [SerializeField]
    private GameObject factedDomeSky4;
    [SerializeField]
    private GameObject factedDomeSky5;
    private GameObject[] factedDomeSkies = new GameObject[6];

    

	// Use this for initialization
	void Start () {
        factedDomeSkies[0] = factedDomeSky0;
        factedDomeSkies[1] = factedDomeSky1;
        factedDomeSkies[2] = factedDomeSky2;
        factedDomeSkies[3] = factedDomeSky3;
        factedDomeSkies[4] = factedDomeSky4;
        factedDomeSkies[5] = factedDomeSky5;
        Instantiate(factedDomeSkies[PlayerPrefs.GetInt("domeSkyNum")], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        
        StartCoroutine(GameStartCounter());
        StartCoroutine(GameLevelController());
	}

    public void GameStart()
    {
        gameTimer.StartTimer();
        misGenerator.StartMisProcess();
    }
    public void GameStop()
    {
        ui_Menu.OpenMenu();
        gameTimer.SetGameTimeState(GameTimer.GAME_TIME_STATE.TIME_END);
    }
    public void GameReStart()
    {
        ui_Menu.CloseMenu();
        gameTimer.SetGameTimeState(GameTimer.GAME_TIME_STATE.TIME_GO);
        gameTimer.ReStartTimer();
    }

    // 게임 난이도는 초/분 단위등으로 조절한다.
    // 다만, 시간의 경우 0~60간격으로 계속 흐르므로, 난이도 메소드가 중복되서 불리운다.
    // 이에따라 난이도별로 메소드를 만들어서 1번만 실행되도록 한다.
    IEnumerator GameLevelController()
    {
        int curGameTimeSec = gameTimer.GetGameTimeInfo().GetSeconds();
        while(true)
        {
            if (curGameTimeSec == 20) CreateItem30sec();
            if (curGameTimeSec == 46) CreateItem46sec();

            if (curGameTimeSec == 1) { StartLevel1(); }
            else if (curGameTimeSec == 3) { StartLevel2(); }
            else if (curGameTimeSec == 5) { StartLevel3(); }

            

            curGameTimeSec = gameTimer.GetGameTimeInfo().GetSeconds();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void CreateItem30sec()
    {
        gameMsgManager.StartMsg("[44AEDF]ItemBox Created!!");
        itemDropManager.CreateItemBoxes(2);
    }
    private void CreateItem46sec()
    {
        gameMsgManager.StartMsg("[44AEDF]ItemBox Created!!");
        itemDropManager.CreateItemBoxes(3);
    }

    bool isLevel1Start = false;
    private void StartLevel1()
    {
        if (isLevel1Start == true) return;

        gameMsgManager.StartMsg("[F3F625]Diffculty 1 Up!");
        misGenerator.StartLevel1();

        isLevel1Start = true;
    }
    bool isLevel2Start = false;
    private void StartLevel2()
    {
        if (isLevel2Start == true) return;

        gameMsgManager.StartMsg("[F3F625]Diffculty 2 Up!");
        misGenerator.StartLevel2();

        isLevel2Start = true;
    }
    bool isLevel3Start = false;
    private void StartLevel3()
    {
        if (isLevel3Start == true) return;

        gameMsgManager.StartMsg("[F3F625]Diffculty 3 Up!");
        misGenerator.StartLevel3();

        isLevel3Start = true;
    }



    IEnumerator GameStartCounter()
    {
        while(true)
        {
            if (countTime > 0.0f)
            {
                lbl_gameStartTimer.text = countTime.ToString();
                countTime -= 1.0f;
                yield return new WaitForSeconds(1.0f);

            }
            else
            {
                lbl_gameStartTimer.text = "0";
                lbl_gameStartTimer.gameObject.SetActive(false);
                break;
            }
        }

        GameStart();
    }
}
