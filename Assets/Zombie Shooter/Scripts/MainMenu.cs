using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public static int MONEY;
    public static int LEVELS;//number of the last unlocked level
    public static float joystickSize;//setting the size of the joystick
    public static float volume;//the volume of all sounds
    public static string ControlMode;//Mouse/Touch
    public Image joy;//joystick button (resizes when setting)
    public Image vol;//volume button (changes color when setting)

    public int slide = 2;//tab number - settings(1), weapons(2), levels(3)
    private int gunsPage = 0;//gun number offset
    private int levelsPage = 0;//level number offset

    public GameObject canvas1;//settings screen
    public GameObject canvas2;//weapons screen
    public GameObject canvas3;//levels screen

    //tab selection buttons:
    public Image s1;
    public Image s2;
    public Image s3;

    public Image selected;//illuminated background of the selected slot (weapon, level)
    public GameObject lines;//common for weapons and levels decor. Is disabled on the settings tab

    public GameObject moneytext;
    public AudioClip select;//clicking sound
    public AudioClip select2;//clicking sound(slides)

    //Slots
    public Image S1;//icons
    public Image S2;
    public Image S3;
    public Image S4;
    public Image S5;
    public Text Name1;//names
    public Text Name2;
    public Text Name3;
    public Text Name4;
    public Text Name5;
    public Text Price1;//prices/level numbers
    public Text Price2;
    public Text Price3;
    public Text Price4;
    public Text Price5;

    public Sprite[] GunSprites;//array of gun icons
    public Sprite[] LevelSprites;//array of level icons

    public static int SelectedLevel = 1;//selected level
    private float x;//screen size
    private float y;
    private AudioSource audio;
    public GUIStyle Lite;//invisible buttons

    int Gunslength = 15;//number of guns
    Gun[] Guns;//array of gun classes

    int Levelslength = 6;//number of levels
    Level[] Levels;//array of level classes

    #region How to add a new gun or level! -----------------------------------------------------------------------------------------------------
    /*
     To add a new GUN you need to:
     -increase Gunslength (above);
     -increase GunSprites array and add new sprite (in MainMenu level, GameObject MainCamera) (or use the available icons);
     -specify Guns.GunName, Guns.GunSprite and Guns.price (in Guns region below);
     -specify gun characteristics in PlayerShooting script, Guns region (example are commented out);
     -in weaponSkin script create new GameObject and activation condition (examples are commented out);
     -on the any game scene in the GameObject Player/Skin/weapon add new gun skin GameObject, add it to weaponSkin script and Apply changes.
     
     To add a new LEVEL you need to:
     -open the scene of any level and save as a new level
     -open Build Settings and Add open scenes
     -increase Levelslength (above);
     -increase LevelSprites array and add new sprite (in MainMenu level, GameObject MainCamera) (or use the available icons);
     -specify Levels.LevelName, Levels.LevelSprite and Levels.LevelNumber (in Levels region below);
    */
    #endregion

    class Gun
    {
        public string GunName;
        public Sprite GunSprite;
        public int price;
    }

    class Level
    {
        public string LevelName;
        public Sprite LevelSprite;
        public int LevelNumber;
    }

	void Start () {
        x = Screen.width;//screen size
        y = Screen.height;
        audio = GetComponent<AudioSource>();

        #region Guns
        Guns = new Gun[Gunslength];
        for (int i = 0; i < Gunslength; ++i) { Guns[i] = new Gun(); }

        int num = 0;
        num = 0;
        Guns[num].GunName = "Pistol";
        Guns[num].GunSprite = GunSprites[0];
        Guns[num].price = 0;

        num = 1;
        Guns[num].GunName = "AK";
        Guns[num].GunSprite = GunSprites[1];
        Guns[num].price = 100;

        num = 2;
        Guns[num].GunName = "Mashine gun";
        Guns[num].GunSprite = GunSprites[2];
        Guns[num].price = 200;

        num = 3;
        Guns[num].GunName = "Snipe";
        Guns[num].GunSprite = GunSprites[3];
        Guns[num].price = 300;

        num = 4;
        Guns[num].GunName = "Shotgun";
        Guns[num].GunSprite = GunSprites[4];
        Guns[num].price = 400;

        num = 5;
        Guns[num].GunName = "Crossbow";
        Guns[num].GunSprite = GunSprites[5];
        Guns[num].price = 500;

        num = 6;
        Guns[num].GunName = "Shotgun 2";
        Guns[num].GunSprite = GunSprites[6];
        Guns[num].price = 600;

        num = 7;
        Guns[num].GunName = "Uzi";
        Guns[num].GunSprite = GunSprites[7];
        Guns[num].price = 500;

        num = 8;
        Guns[num].GunName = "Famas";
        Guns[num].GunSprite = GunSprites[8];
        Guns[num].price = 400;

        num = 9;
        Guns[num].GunName = "Rifle";
        Guns[num].GunSprite = GunSprites[9];
        Guns[num].price = 500;

        num = 10;
        Guns[num].GunName = "RPG";
        Guns[num].GunSprite = GunSprites[10];
        Guns[num].price = 700;

        num = 11;
        Guns[num].GunName = "Launcher";
        Guns[num].GunSprite = GunSprites[11];
        Guns[num].price = 800;

        num = 12;
        Guns[num].GunName = "VSS";
        Guns[num].GunSprite = GunSprites[12];
        Guns[num].price = 600;

        num = 13;
        Guns[num].GunName = "USAS-12";
        Guns[num].GunSprite = GunSprites[13];
        Guns[num].price = 700;

        num = 14;
        Guns[num].GunName = "ASh 12";
        Guns[num].GunSprite = GunSprites[14];
        Guns[num].price = 1000;

        //new gun
        /*
        num = 15;
        Guns[num].GunName = "New Gun";
        Guns[num].GunSprite = GunSprites[0];
        Guns[num].price = 0;
        */
        #endregion

        #region Levels
        Levels = new Level[Levelslength];
        for (int i = 0; i < Levelslength; ++i) { Levels[i] = new Level(); }

        num = 0;
        Levels[num].LevelName = "FAQ";
        Levels[num].LevelSprite = LevelSprites[0];
        Levels[num].LevelNumber = num+1;

        num = 1;
        Levels[num].LevelName = "Arena";
        Levels[num].LevelSprite = LevelSprites[1];
        Levels[num].LevelNumber = num + 1;

        num = 2;
        Levels[num].LevelName = "Corridors";
        Levels[num].LevelSprite = LevelSprites[2];
        Levels[num].LevelNumber = num + 1;

        num = 3;
        Levels[num].LevelName = "Defence";
        Levels[num].LevelSprite = LevelSprites[3];
        Levels[num].LevelNumber = num + 1;

        num = 4;
        Levels[num].LevelName = "Turret";
        Levels[num].LevelSprite = LevelSprites[4];
        Levels[num].LevelNumber = num + 1;

        num = 5;
        Levels[num].LevelName = "Maze";
        Levels[num].LevelSprite = LevelSprites[5];
        Levels[num].LevelNumber = num + 1;

        //new level

        #endregion

        
        ControlMode = "Mouse";//The code below, on the PC, for some reason, it does not choose "Mouse" control

#if UNITY_ANDROID || UNITY_IOS //control mode selection
    ControlMode = "Touch";
#else
    ControlMode = "Mouse";
#endif

        if (PlayerPrefs.HasKey("money"))//load money
            MONEY = PlayerPrefs.GetInt("money");
        else MONEY = 0;//default money=0

        if (PlayerPrefs.HasKey("levels"))//load levels
            LEVELS = PlayerPrefs.GetInt("levels");
        else LEVELS = 1;//default levels=1

        if (PlayerPrefs.HasKey("sound"))//load volume
            volume = PlayerPrefs.GetFloat("sound");
        else volume = 1;//default volume=1

        if (PlayerPrefs.HasKey("joystick"))//load joystick Size
            joystickSize = PlayerPrefs.GetFloat("joystick");
        else joystickSize = 0.3f;//default joystickSize=0.3f

        moneytext.GetComponent<Text>().text = MONEY.ToString();//show money
        Colorise();//set buttons colors
	}
	
    void OnGUI() {

        //START
        if (GUI.Button(new Rect(x * 0.8f, y * 0.8f, x * 0.2f, y * 0.2f), "", Lite))//button position & size (X,Y) (0-1)
        {
            Application.LoadLevel(SelectedLevel);//load selected level
        }

        //tab selection buttons:
        if (GUI.Button(new Rect(x * 0.2f, y * 0.8f, x * 0.2f, y * 0.2f), "", Lite))//settings
        {
            slide = 1;
            Colorise();
            audio.PlayOneShot(select2, volume);//play sound
        }
        if (GUI.Button(new Rect(x * 0.4f, y * 0.8f, x * 0.2f, y * 0.2f), "", Lite))//weapons
        {
            slide = 2;
            Colorise();
            audio.PlayOneShot(select2, volume);//play sound
        }
        if (GUI.Button(new Rect(x * 0.6f, y * 0.8f, x * 0.2f, y * 0.2f), "", Lite))//levels
        {
            slide = 3;
            Colorise();
            audio.PlayOneShot(select2, volume);//play sound
        }

        if (slide == 1)
        {
            //Delete saves
            if (GUI.Button(new Rect(x * 0.6f, y * 0.3f, x * 0.2f, y * 0.2f), "", Lite))
            {
                PlayerPrefs.DeleteAll();//delete
                Application.LoadLevel(0);//restart level
                SelectedLevel = 1;//reset selected level
                PlayerShooting.GUN = 1;//reset selected gun
            }
            //Sounds
            if (GUI.Button(new Rect(x * 0.4f, y * 0.3f, x * 0.2f, y * 0.2f), "", Lite))
            {
                volume += 0.2f;
                if (volume > 1) volume = 0;

                Colorise();
                audio.PlayOneShot(select, volume);//play sound
                PlayerPrefs.SetFloat("sound", volume);//SAVE
            }
            //Joystick size
            if (GUI.Button(new Rect(x * 0.2f, y * 0.3f, x * 0.2f, y * 0.2f), "", Lite))
            {
                joystickSize += 0.05f;
                if (joystickSize > 0.5f) joystickSize = 0.2f;

                Colorise();
                audio.PlayOneShot(select, volume);//play sound
                PlayerPrefs.SetFloat("joystick", joystickSize);//SAVE
            }
        }
        if (slide == 2)
        {
            S1.sprite = Guns[0 + gunsPage].GunSprite;//setting gun icons
            S2.sprite = Guns[1 + gunsPage].GunSprite;
            S3.sprite = Guns[2 + gunsPage].GunSprite;
            S4.sprite = Guns[3 + gunsPage].GunSprite;
            S5.sprite = Guns[4 + gunsPage].GunSprite;

            //Right
            if (GUI.Button(new Rect(x * 0.9f, y * 0.3f, x * 0.1f, y * 0.2f), "", Lite))
            {
                if (gunsPage <= Guns.Length - 10) { gunsPage += 5; }
                else { gunsPage += Guns.Length - gunsPage - 5; }
                Colorise();//set buttons colors
                audio.PlayOneShot(select, volume);//play sound
            }
            //Left
            if (GUI.Button(new Rect(x * 0.0f, y * 0.3f, x * 0.1f, y * 0.2f), "", Lite))
            {
                if (gunsPage >= 5) { gunsPage -= 5; }
                else { gunsPage -= gunsPage; }
                Colorise();//set buttons colors
                audio.PlayOneShot(select, volume);//play sound
            }
            //GUNS
            if (GUI.Button(new Rect(x * 0.1f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & MONEY >= Guns[0 + gunsPage].price)
            {
                PlayerShooting.GUN = 0 + gunsPage;//selecting the gun number in the first slot
                Colorise();//set buttons colors
                audio.PlayOneShot(select, volume);//play sound
            }
            if (GUI.Button(new Rect(x * 0.26f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & MONEY >= Guns[1 + gunsPage].price)
            {
                PlayerShooting.GUN = 1 + gunsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
            if (GUI.Button(new Rect(x * 0.42f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & MONEY >= Guns[2 + gunsPage].price)
            {
                PlayerShooting.GUN = 2 + gunsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
            if (GUI.Button(new Rect(x * 0.58f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & MONEY >= Guns[3 + gunsPage].price)
            {
                PlayerShooting.GUN = 3 + gunsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
            if (GUI.Button(new Rect(x * 0.74f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & MONEY >= Guns[4 + gunsPage].price)
            {
                PlayerShooting.GUN = 4 + gunsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
        }
        if (slide == 3)
        {
            S1.sprite = Levels[0 + levelsPage].LevelSprite;//setting level icons
            S2.sprite = Levels[1 + levelsPage].LevelSprite;
            S3.sprite = Levels[2 + levelsPage].LevelSprite;
            S4.sprite = Levels[3 + levelsPage].LevelSprite;
            S5.sprite = Levels[4 + levelsPage].LevelSprite;

            //Right
            if (GUI.Button(new Rect(x * 0.9f, y * 0.3f, x * 0.1f, y * 0.2f), "", Lite))
            {
                if (levelsPage <= Levels.Length - 10) { levelsPage += 5; }
                else { levelsPage += Levels.Length - levelsPage - 5; }
                Colorise();//set buttons colors
                audio.PlayOneShot(select, volume);//play sound
            }
            //Left
            if (GUI.Button(new Rect(x * 0.0f, y * 0.3f, x * 0.1f, y * 0.2f), "", Lite))
            {
                if (levelsPage >= 5) { levelsPage -= 5; }
                else { levelsPage -= levelsPage; }
                Colorise();//set buttons colors
                audio.PlayOneShot(select, volume);//play sound
            }
            //LEVELS
            if (GUI.Button(new Rect(x * 0.1f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & LEVELS >= 1 + levelsPage)
            {
                SelectedLevel = 1 + levelsPage;//selecting the level number in the first slot
                Colorise();//set buttons colors
                audio.PlayOneShot(select, volume);//play sound
            }
            if (GUI.Button(new Rect(x * 0.26f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & LEVELS >= 2 + levelsPage)
            {
                SelectedLevel = 2 + levelsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
            if (GUI.Button(new Rect(x * 0.42f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & LEVELS >= 3 + levelsPage)
            {
                SelectedLevel = 3 + levelsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
            if (GUI.Button(new Rect(x * 0.58f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & LEVELS >= 4 + levelsPage)
            {
                SelectedLevel = 4 + levelsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
            if (GUI.Button(new Rect(x * 0.74f, y * 0.3f, x * 0.15f, y * 0.2f), "", Lite) & LEVELS >= 5 + levelsPage)
            {
                SelectedLevel = 5 + levelsPage;
                Colorise();
                audio.PlayOneShot(select, volume);
            }
        }
    }
    
    void Colorise() {
        canvas1.SetActive(false);//deactivate all
        canvas2.SetActive(false);
        canvas3.SetActive(false);
        s1.color = new Color(0, 1, 0, 0.3f);//DARK GREEN tab selection buttons (not selected)
        s2.color = new Color(0, 1, 0, 0.3f);
        s3.color = new Color(0, 1, 0, 0.3f);
        if (slide == 1) { s1.color = new Color(0, 1, 0, 0.9f); canvas1.SetActive(true); lines.SetActive(false); }//settings screen
        if (slide == 2) { s2.color = new Color(0, 1, 0, 0.9f); canvas2.SetActive(true); lines.SetActive(true);//weapons screen
            selected.rectTransform.anchorMin = new Vector2((PlayerShooting.GUN - gunsPage +1) * 0.16f - 0.06f, 0.41f);// (min pos) illuminate background of the selected weapon button
            selected.rectTransform.anchorMax = new Vector2((PlayerShooting.GUN - gunsPage +1) * 0.16f + 0.1f, 0.7f);// (max pos)
        }
        if (slide == 3) { s3.color = new Color(0, 1, 0, 0.9f); canvas3.SetActive(true); lines.SetActive(true);//levels screen
        selected.rectTransform.anchorMin = new Vector2((SelectedLevel - levelsPage) * 0.16f - 0.06f, 0.41f);// (min pos) illuminate background of the selected level button
        selected.rectTransform.anchorMax = new Vector2((SelectedLevel - levelsPage) * 0.16f + 0.1f, 0.7f);// (max pos)
        }

        joy.rectTransform.localScale = new Vector2(joystickSize * 2, joystickSize * 2);//resize joystick button for demonstration

        if (volume == 0) vol.color = new Color(1, 0, 0, 0.3f);//change volume button color for demonstrate the volume
        else vol.color = new Color(0, 1, 0, volume);

        //DARK GREEN (not selected)
        S1.color = new Color(0, 1, 0, 0.3f);//Red, Green, Blue, Alfa
        S2.color = new Color(0, 1, 0, 0.3f);
        S3.color = new Color(0, 1, 0, 0.3f);
        S4.color = new Color(0, 1, 0, 0.3f);
        S5.color = new Color(0, 1, 0, 0.3f);

        if (slide == 2)
        {
            //RED GUN (locked)
            if (MONEY < Guns[0 + gunsPage].price) S1.color = new Color(1, 0, 0, 0.3f);
            if (MONEY < Guns[1 + gunsPage].price) S2.color = new Color(1, 0, 0, 0.3f);
            if (MONEY < Guns[2 + gunsPage].price) S3.color = new Color(1, 0, 0, 0.3f);
            if (MONEY < Guns[3 + gunsPage].price) S4.color = new Color(1, 0, 0, 0.3f);
            if (MONEY < Guns[4 + gunsPage].price) S5.color = new Color(1, 0, 0, 0.3f);

            //GREEN GUN (selected)
            if (PlayerShooting.GUN == gunsPage) S1.color = new Color(0, 1, 0, 1);
            if (PlayerShooting.GUN == 1 + gunsPage) S2.color = new Color(0, 1, 0, 1);
            if (PlayerShooting.GUN == 2 + gunsPage) S3.color = new Color(0, 1, 0, 1);
            if (PlayerShooting.GUN == 3 + gunsPage) S4.color = new Color(0, 1, 0, 1);
            if (PlayerShooting.GUN == 4 + gunsPage) S5.color = new Color(0, 1, 0, 1);

            Name1.text = Guns[0 + gunsPage].GunName;//setting gun names
            Name2.text = Guns[1 + gunsPage].GunName;
            Name3.text = Guns[2 + gunsPage].GunName;
            Name4.text = Guns[3 + gunsPage].GunName;
            Name5.text = Guns[4 + gunsPage].GunName;

            Price1.text = Guns[0 + gunsPage].price.ToString();//setting gun prices
            Price2.text = Guns[1 + gunsPage].price.ToString();
            Price3.text = Guns[2 + gunsPage].price.ToString();
            Price4.text = Guns[3 + gunsPage].price.ToString();
            Price5.text = Guns[4 + gunsPage].price.ToString();
        }
        if (slide == 3)
        {
            //RED LEVEL (locked)
            if (LEVELS < 1 + levelsPage) S1.color = new Color(1, 0, 0, 0.3f);
            if (LEVELS < 2 + levelsPage) S2.color = new Color(1, 0, 0, 0.3f);
            if (LEVELS < 3 + levelsPage) S3.color = new Color(1, 0, 0, 0.3f);
            if (LEVELS < 4 + levelsPage) S4.color = new Color(1, 0, 0, 0.3f);
            if (LEVELS < 5 + levelsPage) S5.color = new Color(1, 0, 0, 0.3f);

            //GREEN LEVEL (selected)
            if ((SelectedLevel - levelsPage) == 1) S1.color = new Color(0, 1, 0, 1);
            if ((SelectedLevel - levelsPage) == 2) S2.color = new Color(0, 1, 0, 1);
            if ((SelectedLevel - levelsPage) == 3) S3.color = new Color(0, 1, 0, 1);
            if ((SelectedLevel - levelsPage) == 4) S4.color = new Color(0, 1, 0, 1);
            if ((SelectedLevel - levelsPage) == 5) S5.color = new Color(0, 1, 0, 1);

            Name1.text = Levels[0 + levelsPage].LevelName;//setting level names
            Name2.text = Levels[1 + levelsPage].LevelName;
            Name3.text = Levels[2 + levelsPage].LevelName;
            Name4.text = Levels[3 + levelsPage].LevelName;
            Name5.text = Levels[4 + levelsPage].LevelName;

            Price1.text = Levels[0 + levelsPage].LevelNumber.ToString();//setting level numbers
            Price2.text = Levels[1 + levelsPage].LevelNumber.ToString();
            Price3.text = Levels[2 + levelsPage].LevelNumber.ToString();
            Price4.text = Levels[3 + levelsPage].LevelNumber.ToString();
            Price5.text = Levels[4 + levelsPage].LevelNumber.ToString();
        }
    }
}
