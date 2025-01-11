using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
    public GameObject bullet_1;//Rifles
    public GameObject bolt_1;//Crossbow
    public GameObject shot_1;//Shotguns

    public GameObject Case_1;//ejecting cartridge cases (bullet_1)
    public GameObject Case_2;//(shot_1)

    public GameObject Turret_1;//(using)
    public GameObject Turret_2;//
    public GameObject Turret_3;//
    public GameObject Mine;//(using)

    public static int GUN = 0;//number of selected gun
    public static string PointBlank;//object tags between the player and the bullets spawn point

    public GameObject RangeLineLeft;//left visual boundary of accuracy (2 green lines on the screen)
    public GameObject RangeLineRight;//Right visual boundary of accuracy
    public ParticleSystem Flash;//shot flash
    public Transform ExtractorPoint;//cartridge cases extractor

    private AudioSource audio;
    public AudioClip reload;//reload sound
    public AudioClip OneReload;//one cartridge reload sound
    //shot sounds:
    public AudioClip shoot1;//Assault rifle
    public AudioClip shoot2;//high frequencies (shoot1)
    public AudioClip shoot3;//Shotgun
    public AudioClip shoot4;//Snipe
    public AudioClip shoot5;//Silence
    public AudioClip shoot6;//Crossbow
    public AudioClip shoot7;//RocketLauncher

    private float GlobalReload = 1;//coefficients for all guns
    private float GlobalWeight = 0.5f;
    private float GlobalRepulsion = 2;
    private float GlobalAccuracy = 0.5f;
    private float GlobalShoot_range = 2;
    private float GlobalDestabilization = 0.5f;
    private float GlobalAiming = 2;
    private float GlobalMax_range = 1;

    //for the current gun
    public AudioClip SelectedShoot;
    public GameObject SelectedBullet;
    public GameObject SelectedCase;

    public static float Damage;
    public static float ExplosionRange;//for non-explosive bullets: 0
    public static float Bleeding;//bleeding coefficient (multiplied by damage)
    public static float BulletSpeed;//(6-12)
    public static float RateOfFire;//interval between shots
    public static int Magazine;//bullets in magazine
    public static int BurstFire;//shooting bursts (number of shots in the burst)
    public int Volley;//number of bullets in the shot (more than 1 for shotguns) (odd values are recommended (one of the bullets flies straight))
    public static int Ammo;//all ammunition
    public static float Reload;//magazine reload time
    public string ReloadType;//magazine reload type (all/one cartridge)
    public static float weight;//affects the player's speed
    public static float repulsion;//enemy repulsion on bullet hits
    public float accuracy;//max accuracy (deviation from the center in degrees)
    public float shoot_range;//shot recoil
    public float destabilization;//affects accuracy in motion
    public float aiming;//aiming speed
    public float max_range;//min accuracy (deviation from the center in degrees) (during continuous shooting)

    private float Range;//current accuracy (deviation from the center in degrees)
    private float curStabil;//
    public static float curRateOfFire;//current timer of interval between shots
    public static int curMagazine;//current bullets in magazine
    public static int curBurstFire;//current
    public static int curAmmo;//current ammunition
    public static float curReload;//current time until the end of reload

    public static bool turret = true;//has turret?
    public static int mines = 3;//max count of mines
    public static int curmines;//current count of mines

	void Start () {
        BurstFire = 0;
        ExplosionRange = 0;
        Volley = 1;
        ReloadType = "all";
        SelectedCase = null;
        
        #region Guns
        #region Pistol
        if (GUN == 0)//Pistol specifications
        {
            Damage = 10;
            Bleeding = 1;
            BulletSpeed = 8;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot5;
            accuracy = 5;
            RateOfFire = 0.2f;
            shoot_range = 4.0f;
            destabilization = 3;
            aiming = 15f;
            max_range = 16;
            Magazine = 12;
            Ammo = 150;
            Reload = 0.7f;
            weight = 1.0f;
            repulsion = 5.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region AK
        if (GUN == 1)//AK specifications
        {
            Damage = 12;
            Bleeding = 1;
            BulletSpeed = 8;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot1;
            accuracy = 4;
            RateOfFire = 0.12f;
            shoot_range = 2.0f;
            destabilization = 5;
            aiming = 9f;
            max_range = 17;
            Magazine = 30;
            Ammo = 120;
            Reload = 2;
            weight = 3.0f;
            repulsion = 6.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region Mashine gun
        if (GUN == 2)//Mashine gun specifications
        {
            Damage = 10;
            Bleeding = 1;
            BulletSpeed = 8;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot1;
            accuracy = 8;
            RateOfFire = 0.1f;
            shoot_range = 1.4f;
            destabilization = 4;
            aiming = 8f;
            max_range = 18;
            Magazine = 100;
            Ammo = 200;
            Reload = 6;
            weight = 6.5f;
            repulsion = 6.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region Snipe
        if (GUN == 3)//Snipe specifications
        {
            Damage = 150;
            Bleeding = 1;
            BulletSpeed = 9;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot4;
            accuracy = 2;
            RateOfFire = 1.2f;
            BurstFire = 1;
            shoot_range = 10.0f;
            destabilization = 12;
            aiming = 6.5f;
            max_range = 20;
            Magazine = 3;
            Ammo = 20;
            Reload = 2.5f;
            weight = 6.0f;
            repulsion = 25.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region Shotgun
        if (GUN == 4)//Shotgun specifications
        {
            Damage = 15;
            Bleeding = 1;
            BulletSpeed = 6;
            SelectedBullet = shot_1;
            SelectedShoot = shoot3;
            accuracy = 15;
            RateOfFire = 0.5f;
            Volley = 5;
            shoot_range = 4.0f;
            destabilization = 3;
            aiming = 7f;
            max_range = 20;
            Magazine = 2;
            Ammo = 40;
            Reload = 1.2f;
            weight = 1.5f;
            repulsion = 7.0f;
        }
        #endregion
        #region Crossbow
        if (GUN == 5)//Crossbow specifications
        {
            Damage = 30;
            Bleeding = 3;
            BulletSpeed = 8;
            SelectedBullet = bolt_1;
            SelectedShoot = shoot6;
            accuracy = 3;
            RateOfFire = 0.3f;
            BurstFire = 1;
            shoot_range = 3.0f;
            destabilization = 4;
            aiming = 6f;
            max_range = 10;
            Magazine = 12;
            Ammo = 60;
            Reload = 0.5f;
            ReloadType = "one";
            weight = 2.0f;
            repulsion = 20.0f;
        }
        #endregion
        #region Shotgun 2
        if (GUN == 6)//Shotgun 2 specifications
        {
            Damage = 15;
            Bleeding = 1;
            BulletSpeed = 6;
            SelectedBullet = shot_1;
            SelectedShoot = shoot3;
            accuracy = 12;
            RateOfFire = 0.6f;
            Volley = 7;
            shoot_range = 3.0f;
            destabilization = 4;
            aiming = 6f;
            max_range = 20;
            Magazine = 5;
            Ammo = 30;
            Reload = 0.8f;
            ReloadType = "one";
            weight = 3.0f;
            repulsion = 7.0f;
            SelectedCase = Case_2;
        }
        #endregion
        #region Uzi
        if (GUN == 7)//Uzi
        {
            Damage = 8;
            Bleeding = 1.5f;
            BulletSpeed = 8;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot1;
            accuracy = 10;
            RateOfFire = 0.08f;
            //BurstFire = 4;
            shoot_range = 1.8f;
            destabilization = 3;
            aiming = 10f;
            max_range = 16;
            Magazine = 20;
            Ammo = 250;
            Reload = 1;
            weight = 1.5f;
            repulsion = 4.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region Famas
        if (GUN == 8)//Famas
        {
            Damage = 10;
            Bleeding = 1;
            BulletSpeed = 9;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot1;
            accuracy = 3;
            RateOfFire = 0.09f;
            BurstFire = 3;
            shoot_range = 2.0f;
            destabilization = 4;
            aiming = 10f;
            max_range = 17;
            Magazine = 25;
            Ammo = 120;
            Reload = 1.8f;
            weight = 3.6f;
            repulsion = 7.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region Rifle
        if (GUN == 9)//Rifle
        {
            Damage = 75;
            Bleeding = 1;
            BulletSpeed = 9;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot4;
            accuracy = 2.3f;
            RateOfFire = 1.0f;
            BurstFire = 1;
            shoot_range = 7.0f;
            destabilization = 8;
            aiming = 7.0f;
            max_range = 20;
            Magazine = 5;
            Ammo = 25;
            Reload = 0.7f;
            ReloadType = "one";
            weight = 4.5f;
            repulsion = 25.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region RPG
        if (GUN == 10)//RPG
        {
            Damage = 130;
            ExplosionRange = 1.5f;
            Bleeding = 1;
            BulletSpeed = 5;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot7;
            accuracy = 10;
            RateOfFire = 0.0f;
            shoot_range = 10.0f;
            destabilization = 7;
            aiming = 4f;
            max_range = 25;
            Magazine = 1;
            Ammo = 10;
            Reload = 2.5f;
            ReloadType = "one";
            weight = 9.0f;
            repulsion = 30.0f;
        }
        #endregion
        #region Launcher
        if (GUN == 11)// Launcher
        {
            Damage = 100;
            ExplosionRange = 1.2f;
            Bleeding = 1;
            BulletSpeed = 6;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot7;
            accuracy = 7;
            RateOfFire = 1.2f;
            shoot_range = 5.0f;
            destabilization = 5;
            aiming = 6f;
            max_range = 20;
            Magazine = 6;
            Ammo = 15;
            Reload = 0.8f;
            ReloadType = "one";
            weight = 6.0f;
            repulsion = 15.0f;
        }
        #endregion
        #region VSS
        if (GUN == 12)//VSS
        {
            Damage = 17;
            Bleeding = 1;
            BulletSpeed = 8;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot5;
            accuracy = 2.5f;
            RateOfFire = 0.15f;
            shoot_range = 1.8f;
            destabilization = 6;
            aiming = 8f;
            max_range = 18;
            Magazine = 10;
            Ammo = 100;
            Reload = 1.7f;
            weight = 2.7f;
            repulsion = 7.0f;
            SelectedCase = Case_1;
        }
        #endregion
        #region Shotgun USAS-12
        if (GUN == 13)//Shotgun USAS-12
        {
            Damage = 10;
            Bleeding = 1;
            BulletSpeed = 7;
            SelectedBullet = shot_1;
            SelectedShoot = shoot3;
            accuracy = 16;
            RateOfFire = 0.5f;
            Volley = 5;
            shoot_range = 2.8f;
            destabilization = 3;
            aiming = 7f;
            max_range = 20;
            Magazine = 10;
            Ammo = 40;
            Reload = 3.5f;
            weight = 6.2f;
            repulsion = 6.0f;
            SelectedCase = Case_2;
        }
        #endregion
        #region ASh 12
        if (GUN == 14)//ASh 12
        {
            Damage = 20;
            Bleeding = 1.5f;
            BulletSpeed = 9;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot1;
            accuracy = 3.6f;
            RateOfFire = 0.14f;
            shoot_range = 2.9f;
            destabilization = 4.8f;
            aiming = 10f;
            max_range = 17;
            Magazine = 20;
            Ammo = 90;
            Reload = 2;
            weight = 4.5f;
            repulsion = 10.0f;
            SelectedCase = Case_1;
        }
        #endregion

        #region NewGun !!!!!!!!!!!!!
        /*if (GUN == 15)//
        {
            Damage = 5;
            Bleeding = 1.5f;
            BulletSpeed = 7;
            SelectedBullet = bullet_1;
            SelectedShoot = shoot1;
            accuracy = 5.6f;
            RateOfFire = 0.15f;
            Volley = 5;
            shoot_range = 2.9f;
            destabilization = 4.8f;
            aiming = 10f;
            max_range = 17;
            Magazine = 20;
            Ammo = 90;
            Reload = 2;
            weight = 4.5f;
            repulsion = 10.0f;
            SelectedCase = Case_1;
        }*/
        #endregion
        #endregion

        Reload = Reload * GlobalReload;//multiplying by the coefficients for all guns
        weight = weight * GlobalWeight;
        repulsion = repulsion * GlobalRepulsion;
        accuracy = accuracy * GlobalAccuracy;
        shoot_range = shoot_range * GlobalShoot_range;
        destabilization = destabilization * GlobalDestabilization;
        aiming = aiming * GlobalAiming;
        max_range = max_range * GlobalMax_range;

        audio = GetComponent<AudioSource>();

        //resets:
        curMagazine = Magazine;
        curAmmo = Ammo;
        curReload = 0;
        curmines = mines;
        turret = true;
	}
	

	void Update () {
        //a ray from the player to the bullets spawn point
        RaycastHit2D hit = Physics2D.Raycast(Player.Player_Pos, (Vector2)transform.position - Player.Player_Pos, Vector3.Distance(transform.position, Player.Player_Pos), 513);
        if (hit.collider != null) {
            if (hit.collider.gameObject.CompareTag("Wall"))//ray touches the walls
                PointBlank = "Wall";
            if (hit.collider.gameObject.CompareTag("Enemy"))//ray touches the enemies
                PointBlank = "Enemy";
        } else PointBlank = "";

        if (Player.go)//accuracy calculation in motion
            if (curStabil < destabilization) curStabil += destabilization * Time.deltaTime * 1.5f;
            else curStabil = destabilization;
        else curStabil = 0;

        if (curRateOfFire > 0) { curRateOfFire -= Time.deltaTime; }//decrease current timer of interval between shots, if needed
        if (curReload > 0) {
            curReload -= Time.deltaTime;//decrease current time until the end of reload, if needed
            if (curReload <= 0 & ReloadType == "one" & curMagazine < Magazine & curAmmo>0) {//completion of one cartridge reload
                curMagazine += 1; curAmmo -= 1; audio.PlayOneShot(OneReload, MainMenu.volume);
                if (curMagazine < Magazine & curAmmo > 0) curReload = Reload; 
            }
            if (curReload <= 0 & ReloadType == "all") {//completion of all cartridges reload
                int R = Magazine - curMagazine;//how many cartridges are required to fully reload the magazine
                if (curAmmo < R) R = curAmmo;//if it is impossible to reload the full magazine - reload as much as there is
                curMagazine += R; 
                curAmmo -= R;
            }
        }

        if (curBurstFire > 0) { Shoot(); }//burst fire

        if (Range > accuracy + curStabil) { Range -= Time.deltaTime * aiming; }//aiming (decrease deviation from the center in degrees)
        else { Range = accuracy + curStabil;//max accuracy
        }

        //RAYS DEACTIVATION (accuracy visualization)
        /*if (PlayerAIM.HasTarget == false)
        {
            RangeLineLeft.SetActive(false);//disable visual boundary of current accuracy, if player hasn't target
            RangeLineRight.SetActive(false);
        }*/
        if (curMagazine <= 0 & curAmmo > 0) Rld();//start reloading on picking up AmmoBonus (or AmmoBox) with empty ammunition

        #region Keyboard
        if (MainMenu.ControlMode == "Mouse") {
            if (BurstFire == 0 & (Input.GetKey(KeyCode.KeypadEnter) | Input.GetKey(KeyCode.P) | Input.GetKey(KeyCode.Space) | Input.GetMouseButton(0))) { Shoot(); }//auto shots
            else if (BurstFire > 0 & curBurstFire <= 0 & curRateOfFire <= 0 & (curReload <= 0 | ReloadType == "one") & (Input.GetKeyDown(KeyCode.KeypadEnter) | Input.GetKeyDown(KeyCode.P) | Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0)))
            { curBurstFire = BurstFire; Shoot(); }//semi-automatic shots

            if (Input.GetKeyDown(KeyCode.R)) { Rld(); }//reload

            if (Input.GetKeyDown(KeyCode.E) & turret){//turret
                Instantiate(Turret_1, Player.Player_Pos, Quaternion.Euler(0, 0, 0));
                turret = false;
            }
            if (Input.GetKeyDown(KeyCode.Q) & curmines > 0){//mine
                Instantiate(Mine, Player.Player_Pos, Quaternion.Euler(0, 0, 0));
                curmines -= 1;
            }
        }

        #endregion
        #region Touch
        for (int i = 0; i < Input.touchCount; ++i)//touch control
        {
            Vector2 pos = new Vector2(Input.GetTouch(i).position.x / Screen.width, Input.GetTouch(i).position.y / Screen.height);//touch position(x,y) (0-1)
            if (BurstFire == 0 & pos.x > 0.8f & pos.y < 0.3f)//stay touch
            {
                Shoot();//auto shots
            }
            else if (BurstFire > 0 & curBurstFire <= 0 & curRateOfFire <= 0 & (curReload <= 0 | ReloadType == "one") & pos.x > 0.8f & pos.y < 0.3f & Input.GetTouch(i).phase == TouchPhase.Began)//one touch
            {
                curBurstFire = BurstFire;//semi-automatic shots
                Shoot();
            }

            if (Input.GetTouch(i).phase == TouchPhase.Began)//one touch
            {
                if (pos.x > 0.8f & pos.y > 0.3f & pos.y < 0.5f)
                {
                    Rld();//reload
                }
                if (pos.x > 0.7f & pos.x < 0.8f & pos.y < 0.15f & turret)//turret (if has turret)
                {
                    Instantiate(Turret_1, Player.Player_Pos, Quaternion.Euler(0, 0, 0));//spawn turret
                    turret = false;//remove the turret
                }
                if (pos.x > 0.6f & pos.x < 0.7f & pos.y < 0.15f & curmines > 0)//mine (if has mine)
                {
                    Instantiate(Mine, Player.Player_Pos, Quaternion.Euler(0, 0, 0));//spawn mine
                    curmines -= 1;//decrease mines count
                }
            }
        }
        #endregion

        //set angle of visual boundary of accuracy
        RangeLineLeft.transform.localRotation = Quaternion.AngleAxis(Range, Vector3.forward);
        RangeLineRight.transform.localRotation = Quaternion.AngleAxis(-Range, Vector3.forward);
	}

    void Shoot()
    {
        RangeLineLeft.SetActive(true); //make visible at a shot (if the rays were deactivated)
        RangeLineRight.SetActive(true);

        if ((PlayerAIM.HasTarget | MainMenu.ControlMode == "Mouse" & PointBlank != "Wall") & curMagazine > 0 & curRateOfFire <= 0 & (ReloadType == "one" | curReload <= 0))//shot if has: target(or mouse control), bullet, bullet in magazine and if interval has elapsed
        {
            if (ReloadType == "one" & curReload > 0) curReload = 0;//stopping one cartridge reload
            audio.PlayOneShot(SelectedShoot, MainMenu.volume);//shot sound (float in parenthesis indicates the volume)

            //one bullet
            if (Volley == 1) {
                BulletSettings((GameObject)Instantiate(SelectedBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-Range, Range))));
            }
            //several bullets (shotguns)
            else {
                float ang = (Range * 2) / (Volley - 1);//angle between bullets
                for (int i = 0; i < Volley; i++) {
                    BulletSettings((GameObject)Instantiate(SelectedBullet, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (-Range + ang * i))));
                }
            }

            //cartridge case spawn
            if (SelectedCase != null) CaseSettings((GameObject)Instantiate(SelectedCase, new Vector2(ExtractorPoint.position.x, ExtractorPoint.position.y), Quaternion.Euler(0, 0, ExtractorPoint.rotation.eulerAngles.z + Random.Range(-15, 15))));

            if (GUN != 5) Flash.Play(true);//flash at a shot, except a crossbow
            curMagazine -= 1;//decrease bullets in magazine
            curBurstFire -= 1;//decrease bullets in burst
            curRateOfFire = RateOfFire;//reset current interval between shots
            Range += shoot_range;//add recoil to the current accuracy
            if (Range > max_range + accuracy) Range = max_range + accuracy;//deviation shall not exceed the maximum
            if (curMagazine <= 0 & curReload <= 0 & curAmmo>0) {//start reload, if bullets ended
                curReload = Reload;
                curBurstFire = 0;//stopping BurstFire
                if (ReloadType == "one") curReload += RateOfFire;//(one cartridge reload) reload time for an empty magazine includes the interval between shots
                audio.PlayOneShot(reload, MainMenu.volume); 
            }
        }
        
    }

    void BulletSettings(GameObject bul) {//bullet characteristics setting
        var B = bul.GetComponent<Bullet>();
        B.damage = Damage;
        B.explosionRange = ExplosionRange;
        B.bleeding = Bleeding;
        B.speed = BulletSpeed;
        B.repulsion = repulsion;
    }

    void CaseSettings(GameObject c) {
        if (PointBlank == "Enemy") c.GetComponent<BoxCollider2D>().isTrigger = true;//bugfix (strong repulsion of enemies with cartridge cases)
    }

    void Rld()
    {
        curBurstFire = 0;//stop the burst
        if (curMagazine < Magazine & curAmmo > 0 & curReload <= 0)//if magazine isn't full, ammo isn't empty and reload hasn't started yet
        {
            audio.PlayOneShot(reload, MainMenu.volume);//reload sound
            curReload = Reload;//reset reload timer
        }
    }
}
