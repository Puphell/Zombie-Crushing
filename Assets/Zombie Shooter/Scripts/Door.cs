using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public GameObject trigger;//(key) At trigger destroy - the door will open
    public string Pass;// "Red"/"Blue"/"Green" card
    public float openingSpeed = 1f;
    public float dist = 1.5f;//distance to the player to open the door / or 0 if the door is opened immediately if not locked
    public GameObject door1;
    public GameObject door2;//can be null
    public SpriteRenderer PassColor;//pass-card color indicator
    public GameObject L1;//Red lamp (locked)
    public GameObject L2;//Green lamp (unlocked)

    private float openingDist;
    private bool PassAccess = false;

    void Start()
    {
        if (Pass == "Red") PassColor.color = new Color(0.8f, 0.26f, 0.26f);//pass-card color indicator color setting
        if (Pass == "Blue") PassColor.color = new Color(0.26f, 0.37f, 0.8f);
        if (Pass == "Green") PassColor.color = new Color(0.23f, 0.6f, 0.25f);
        if (Pass == "") PassColor.color = new Color(0, 0, 0, 0);

        openingDist = door1.transform.localPosition.x;//the maximum door opening distance is equal to the initial position of the doors (door1)
        door1.transform.localPosition = new Vector2(0, door1.transform.localPosition.y);//at start immediately close the doors
        if (door2 != null) door2.transform.localPosition = new Vector2(0, door2.transform.localPosition.y);
    }
    void Update()
    {
        if (trigger == null & PassAccess)//Trigger(key) destroyed
        {
            L1.SetActive(false);
            L2.SetActive(true);
        }

        GameObject PL = GameObject.FindGameObjectWithTag("Player");
        float PLdist = Vector2.Distance(transform.position, PL.transform.position);//distance to the player

        if (Pass != "")//if pass-card is needed
        foreach (var P in Player.PassCards) {
            if (P == Pass) {
                PassAccess = true;//unlock the door if player have a required card
                PassColor.color = new Color(PassColor.color.r, PassColor.color.g, PassColor.color.b, 0.4f);//make the pass-card color indicator transparent
            }
        }
        else PassAccess = true;//unlock the door if pass-card is not needed

        if ((PLdist < dist | dist == 0) & trigger == null & PassAccess)//door opening
        {
            if (door1.transform.localPosition.x < openingDist)
            {
                door1.transform.Translate(Vector3.right * Time.deltaTime * openingSpeed);
                if (door2!=null) door2.transform.Translate(Vector3.right * Time.deltaTime * openingSpeed);
            }
        } else {
            if (door1.transform.localPosition.x > 0)
            {
                door1.transform.Translate(Vector3.right * Time.deltaTime * -openingSpeed);
                if (door2 != null) door2.transform.Translate(Vector3.right * Time.deltaTime * -openingSpeed);
            }
        }
    }
}
