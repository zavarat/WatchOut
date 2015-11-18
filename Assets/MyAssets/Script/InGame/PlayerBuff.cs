using UnityEngine;
using System.Collections;

public class PlayerBuff : MonoBehaviour {

    [SerializeField]
    private GameObject obj_sprintBuff;
    [SerializeField]
    private GameObject obj_shieldBuff;
    [SerializeField]
    private GameObject obj_noEnemyBuff;

    public void OnSprintBuff() { obj_sprintBuff.SetActive(true); }
    public void OffSprintBuff() { obj_sprintBuff.SetActive(false); }

    public void OnShieldBuff() { obj_shieldBuff.SetActive(true); }
    public void OffShieldBuff() { obj_shieldBuff.SetActive(false); }

    public void OnNoEnemyBuff() { obj_noEnemyBuff.SetActive(true); }
    public void OffNoEnemyBuff() { obj_noEnemyBuff.SetActive(false); }
}
