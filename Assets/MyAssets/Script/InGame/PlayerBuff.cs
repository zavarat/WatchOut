using UnityEngine;
using System.Collections;

public class PlayerBuff : MonoBehaviour {

    [SerializeField]
    private GameObject obj_sprintBuff;
    [SerializeField]
    private GameObject obj_shieldBuff;

    public void OnSprintBuff() { obj_sprintBuff.SetActive(true); }
    public void OffSprintBuff() { obj_sprintBuff.SetActive(false); }

    public void OnShieldBuff() { obj_shieldBuff.SetActive(true); }
    public void OffShieldBuff() { obj_shieldBuff.SetActive(false); }
}
