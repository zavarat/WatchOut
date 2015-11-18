using UnityEngine;
using System.Collections;

public class LoadingProcess : MonoBehaviour {

    [SerializeField]
    private UISprite spr_loading;

    private int mapNum = 0;

    void Start()
    {
        StartCoroutine(FlickeringSprite());
        StartCoroutine(LoadingRoutine());
    }

    IEnumerator LoadingRoutine()
    {
        yield return new WaitForSeconds(1.0f);

        // 0 to 4 number
        mapNum = Random.Range(0, 5);
        Application.LoadLevelAsync("InGame_Map" + mapNum);
    }


    IEnumerator FlickeringSprite()
    {
        float initAlpha = 1.0f;
        spr_loading.alpha = initAlpha;
        while (true)
        {
            if (spr_loading.alpha <= 0.5f) spr_loading.alpha = initAlpha;
            spr_loading.alpha -= 0.25f;
            yield return new WaitForSeconds(0.25f);
        }
        
    }
}
