using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
    public static int currentKills;
    public bool KillAll = true;//automatic counting of all enemies at the level
    public int Kills;//a number of kills needed to complete the level (set KillAll=false if you want to manually set the number of kills (use 0 if you want to just reached the finish))
    public TextMesh text;

	void Start () {
        currentKills = 0;//reset
        
        if (KillAll) {//counting of all enemies at the level
            //making all enemies list
            GameObject[] List = GameObject.FindGameObjectsWithTag("Enemy");
            Kills = List.Length;

            //making all spawners list
            GameObject[] List2 = GameObject.FindGameObjectsWithTag("SpawnerPoint");
            foreach (GameObject go in List2)
            {
                Kills += go.GetComponent<SpawnerPoint>().limit;
            }
        }
	}

    void Update() {
        if (Kills - currentKills <= 0) text.text = "";
        else text.text = "remaining enemies: " + (Kills - currentKills).ToString();//printing the remaining enemies
    }


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("Player") & currentKills >= Kills) {//player reached the finish & killed enough enemies
            PlayerPrefs.SetInt("money", MainMenu.MONEY);//saving money
            if (MainMenu.LEVELS <= Application.loadedLevel) {//the last available level complited
                MainMenu.LEVELS = Application.loadedLevel + 1;//the last available level number increase
                PlayerPrefs.SetInt("levels", MainMenu.LEVELS);//saving last available level number
            }
            Player.PassCards.Clear();//pass-cards list clearing
            Application.LoadLevel(0);//go to menu
        }
    }
}
