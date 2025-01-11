using UnityEngine;
using System.Collections;

public class weaponSkin : MonoBehaviour {
    public GameObject Pistol;
    public GameObject AK;
    public GameObject MG;
    public GameObject Snipe;
    public GameObject Shotgun;
    public GameObject Crossbow;
    public GameObject Shotgun2;
    public GameObject Uzi;
    public GameObject Famas;
    public GameObject Rifle;
    public GameObject RPG;
    public GameObject Launcher;
    public GameObject VSS;
    public GameObject USAS;
    public GameObject ASh;
    //public GameObject NewSkin;//for new gun

    void Start() {//activation of the desired skin (skins should be deactivated)
        if (PlayerShooting.GUN == 0) Pistol.SetActive(true);
        if (PlayerShooting.GUN == 1) AK.SetActive(true);
        if (PlayerShooting.GUN == 2) MG.SetActive(true);
        if (PlayerShooting.GUN == 3) Snipe.SetActive(true);
        if (PlayerShooting.GUN == 4) Shotgun.SetActive(true);
        if (PlayerShooting.GUN == 5) Crossbow.SetActive(true);
        if (PlayerShooting.GUN == 6) Shotgun2.SetActive(true);
        if (PlayerShooting.GUN == 7) Uzi.SetActive(true);
        if (PlayerShooting.GUN == 8) Famas.SetActive(true);
        if (PlayerShooting.GUN == 9) Rifle.SetActive(true);
        if (PlayerShooting.GUN == 10) RPG.SetActive(true);
        if (PlayerShooting.GUN == 11) Launcher.SetActive(true);
        if (PlayerShooting.GUN == 12) VSS.SetActive(true);
        if (PlayerShooting.GUN == 13) USAS.SetActive(true);
        if (PlayerShooting.GUN == 14) ASh.SetActive(true);
        //if (PlayerShooting.GUN == 15) NewSkin.SetActive(true);//for new gun
	}
}
