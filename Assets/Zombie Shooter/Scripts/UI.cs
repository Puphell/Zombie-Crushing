using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Image cooldown;
    public Image reload;
    public Image magazine;
    public Image HP;
    public Image Ammo;
    public Image Turret;
    public Image Mine;
	
	void Update () {
        if (PlayerShooting.curRateOfFire > 0) cooldown.fillAmount = (PlayerShooting.RateOfFire - PlayerShooting.curRateOfFire) / PlayerShooting.RateOfFire;//cooldown between shots
        else cooldown.fillAmount = 1;

        if (PlayerShooting.curReload > 0) reload.fillAmount = (PlayerShooting.Reload - PlayerShooting.curReload) / PlayerShooting.Reload;//reload cooldown
        else reload.fillAmount = (1.0f * PlayerShooting.curMagazine / PlayerShooting.Magazine);

        magazine.fillAmount = (1.0f * PlayerShooting.curMagazine / PlayerShooting.Magazine);

        HP.fillAmount = Player.curHP / Player.HP;//heath point bar
        Ammo.fillAmount = 1.0f * PlayerShooting.curAmmo / PlayerShooting.Ammo;//ammo bar

        if (PlayerShooting.turret) Turret.fillAmount = 1; else Turret.fillAmount = 0;//turret button
        Mine.fillAmount = 1.0f * PlayerShooting.curmines / PlayerShooting.mines;//mine button
	}
}
