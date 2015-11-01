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

	// Use this for initialization
	void Start () {

        StartCoroutine(GameStartCounter());
	}

    public void GameStart()
    {
        gameTimer.StartTimer();
        misGenerator.StartMisProcess();
    }
    public void GameStop()
    {
        ui_Menu.OpenMenu();
        gameTimer.SetGameState(true);
    }
    public void GameReStart()
    {
        ui_Menu.CloseMenu();
        gameTimer.SetGameState(false);
        gameTimer.ReStartTimer();
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
