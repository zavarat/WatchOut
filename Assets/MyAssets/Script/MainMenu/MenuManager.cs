using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    

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
    private int domeSkyNum = 0;

    private int mapNum = 0;

    public void Start()
    {
        // 배경 sphere를 랜덤으로 선택.
        domeSkyNum = Random.Range(0, 5);
        factedDomeSkies[0] = factedDomeSky0;
        factedDomeSkies[1] = factedDomeSky1;
        factedDomeSkies[2] = factedDomeSky2;
        factedDomeSkies[3] = factedDomeSky3;
        factedDomeSkies[4] = factedDomeSky4;
        factedDomeSkies[5] = factedDomeSky5;

        Instantiate(factedDomeSkies[domeSkyNum], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        PlayerPrefs.SetInt("domeSkyNum", domeSkyNum);

        StartCoroutine(ExitProcess());
    }

    public void StartGame()
    {
        Application.LoadLevelAsync("Loading");
    }

    IEnumerator ExitProcess()
    {
        while(true)
        {
            if ((Application.platform == RuntimePlatform.Android) && (Input.GetKey(KeyCode.Escape)))
                Application.Quit();
            yield return new WaitForSeconds(0.25f);
        }
    }
}

