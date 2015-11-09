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

    IEnumerator GameLevelController()
    {
        float curGameTimeSec = gameTimer.GetGameTimeInfo().GetSeconds();
        while(true)
        {
            if((curGameTimeSec >= 10) && ( curGameTimeSec <= 13))
            {
                misGenerator.StartLevel1();
            }
            if ((curGameTimeSec >= 30) && (curGameTimeSec <= 33))
            {
                misGenerator.StartLevel2();
            }

            curGameTimeSec = gameTimer.GetGameTimeInfo().GetSeconds();
            yield return new WaitForSeconds(0.25f);
        }
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
