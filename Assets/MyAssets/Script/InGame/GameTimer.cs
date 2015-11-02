using UnityEngine;
using System.Collections;
using System;

public struct GameTime
{
    int milliseconds;
    int seconds;
    int minutes;
    string strBuffer;

    public void InitTime()
    {
        milliseconds = 0;
        seconds = 0;
        minutes = 0;
    }

    public void AddMinutes(int _min) { minutes += _min; }
    public void AddMilliSeconds(int _ms)
    {
        milliseconds += _ms;
        if (milliseconds >= 100)
        {
            milliseconds = 0;
            AddSeconds(1);
        }
    }
    public void AddSeconds(int _sec)
    {
        seconds += _sec;
        if(seconds >= 60)
        {
            seconds = 0;
            AddMinutes(1);
        }
    }

    public int GetMilliSeconds() { return milliseconds; }
    public int GetSeconds() { return seconds; }
    public int GetMinutes() { return minutes; }

    public string ToStringType()
    {
        strBuffer = "";

        if (minutes >= 10) strBuffer += minutes.ToString();
        else strBuffer += "0" + minutes.ToString();
        strBuffer += ":";

        if (seconds >= 10) strBuffer += seconds.ToString();
        else strBuffer += "0" + seconds.ToString();
        strBuffer += ":";

        if (milliseconds >= 10) strBuffer += milliseconds.ToString();
        else strBuffer += "0" + milliseconds.ToString();

        return strBuffer;
    }
}

public class GameTimer : MonoBehaviour
{

    [SerializeField]
    private UILabel lbl_gameTimer;
    private Vector3 originLabelScale;
    private GameTime gameTime;

    private GAME_TIME_STATE curTimeState = GAME_TIME_STATE.TIME_GO;
    
    /// <summary>
    /// GameTime 애니메이션 발생간격( sec % SEC_ANIMATE )
    /// </summary>
    private const int SEC_ANIMATE = 5;

    public void StartTimer()
    {
        originLabelScale = lbl_gameTimer.gameObject.transform.localScale;

        gameTime.InitTime();
        StartCoroutine(GameTimeProcess());
    }
    public void ReStartTimer()
    {
        StartCoroutine(GameTimeProcess());
    }

    private void Ani_ScaleUp()
    {
        Vector3 scaleUp = new Vector3(54.0f, 54.0f, 0.0f);
        iTween.ScaleTo(lbl_gameTimer.gameObject,
            iTween.Hash("scale", scaleUp,
            "name", "scaleUp",
            "time", 1.0f,
            "speed", 70.0f,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.none));
    }
    private void Ani_ScaleDown()
    {
        iTween.ScaleTo(lbl_gameTimer.gameObject,
            iTween.Hash("scale", originLabelScale,
            "name", "scaleDown",
            "time", 1.0f,
            "speed", 30.0f,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.none));
    }

    IEnumerator LblAnimaition()
    {
        Ani_ScaleUp();
        yield return new WaitForSeconds(0.23f);
        iTween.StopByName("scaleUp");
        Ani_ScaleDown();
    }
	
    IEnumerator GameTimeProcess()
    {
        while(true)
        {
            if (curTimeState == GAME_TIME_STATE.TIME_END) break;
 
            gameTime.AddMilliSeconds(1);
            lbl_gameTimer.text = gameTime.ToStringType();

            int sec = gameTime.GetSeconds();
            if ((sec > 0) && (sec % SEC_ANIMATE == 0)) StartCoroutine(LblAnimaition());

            yield return new WaitForSeconds(0.0001f);
        }
    }


    public enum GAME_TIME_STATE { TIME_END, TIME_GO }

    /// <summary>
    /// Setting Game Stop / Go state
    /// </summary>
    /// <param name="_timeState"> </param>
    public void SetGameTimeState(GAME_TIME_STATE _timeState)
    {
        curTimeState = _timeState;
    }

}
