using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public Rigidbody2D Body;//Enemy body
    private Transform Target;//Player transform
    private Vector2 PlayerLastPos = new Vector2(0, 0);//player position where he was visible to the enemy for the last time
    private bool follow = false;//If the enemy has ceased to see the player, he will follow on PlayerLastPos

    public float HP;// Horse Power, of course
    public float Speed;
    public int reward;//money reward for killing

    //this for the rifleman ("Enemy 2") ((zombie with a gun))
    public bool rifleman = false;//("Enemy 2")
    public GameObject bullet;
    public Transform ShootPoint;// bullets spawn point
    public float RateOfFire = 0.5f;//interval between shots
    private float curRateOfFire;//current interval between shots
    public int Magazine = 5;//Bulets in magazine (or in shooting bursts)
    private int curMagazine = 5;//counter
    public float Reload = 1;//magazine reload time (or interval between bursts)
    private float curReload;//current time until the end of reload
    private AudioSource audio;
    public AudioClip shoot;//shot sounds

    //blood sprites (GameObjects)
    public GameObject Blood1;// <20 damage
    public GameObject Blood2;// 20+ damage
    public GameObject BloodDead;//here you can use the corpse
    public GameObject BleedMarker;//bleeding indicator
    public SpriteRenderer HPMarker;//HP indicator

    public GameObject BloodParticle;//for all bullets

    private RaycastHit2D hit;//for the visibility system
    public float curHP;//current HP
    private float Bleeding = 0;// (0-10), bleeding timer
    private float maxBleeding = 10;//(10), bleeding timer
    void Start () {
        curHP = HP;
        audio = GetComponent<AudioSource>();
	}
	
	void Update () {
        //Reduction of weapon timers and loading magazine
        if (rifleman) { curRateOfFire -= Time.deltaTime; curReload -= Time.deltaTime; if (curReload <= 0 & curMagazine <= 0) curMagazine = Magazine; }

        if (Bleeding > 0) Bleeding -= Time.deltaTime; else Bleeding = 0;//reduction of bleeding timer
        if (Bleeding > maxBleeding / 2) {//if bleeding time more than half
            curHP -= Bleeding * Time.deltaTime; BleedMarker.SetActive(true); HPcolor();//bleeding damage
        } else BleedMarker.SetActive(false);

        if (curHP <= 0) Death();//death check

        //check of reaching the last pos 
        if(follow & Vector2.Distance(PlayerLastPos, transform.position) <= 0.1f) follow = false;

        Target = GameObject.FindWithTag("Player").transform;//find player
        hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)Player.Player_Pos - (Vector2)transform.position, 5, 1025);//a ray from the enemy to the player
        if (hit.collider != null) {
            if (!hit.collider.gameObject.CompareTag("Wall")) {//ray does not touch the walls
                //follow the target
                Vector3 moveDirection = Target.transform.position - transform.position;
                if (moveDirection != Vector3.zero) {
                    float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                    PlayerLastPos = Target.position;//update last position when the enemy sees the player
                    follow = true;

                    if (rifleman & curRateOfFire <= 0 & curMagazine > 0) {//shot if all conditions are met
                        audio.PlayOneShot(shoot, MainMenu.volume);//shot sound (float in parenthesis indicates the volume)
                        Instantiate(bullet, ShootPoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-2, 2)));//bullet spawn
                        curRateOfFire = RateOfFire;//reset current interval between shots
                        curMagazine -= 1;//decrease bullets in magazine
                        if (curMagazine <= 0) curReload = Reload;//start reload, if bullets ended
                    }
                }
                //show rays:
                //Debug.DrawLine((Vector2)transform.position, (Vector2)Player.Player_Pos);
            }
            else if(follow) {//follow the last target position
                Vector3 moveDirection = new Vector3 (PlayerLastPos.x, PlayerLastPos.y, 0) - transform.position;
                if (moveDirection != Vector3.zero) {
                    float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                }
            }
        }
	}

    void FixedUpdate() {
        if (hit.collider != null) {
            if (hit.collider.gameObject.CompareTag("Player")) {//going, if the enemy sees the player
                Body.AddRelativeForce(new Vector2(Speed * curHP / HP, 0));//the speed depends on the current hp
            }
        }
        if (follow) {//going, if the enemy dont sees the player (twice slower)
            Body.AddRelativeForce(new Vector2(Speed*0.5f, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet")) {//on collision with any bullet
            var BulletScript = col.gameObject.GetComponent<Bullet>();
            if (BulletScript.explosionRange == 0) {//non-explosive bullets
                Instantiate(BloodParticle, new Vector2(col.transform.position.x, col.transform.position.y), Quaternion.Euler(0, 0, col.transform.rotation.eulerAngles.z + 180));
                if (curHP >= BulletScript.damage) {
                    Destroy(col.gameObject);//bullet destroy
                    curHP -= BulletScript.damage;//damage
                    Bleeding += 0.1f * BulletScript.damage * BulletScript.bleeding;//bleeding multiplied by 10% from bullet damage
                    if (Bleeding > maxBleeding) Bleeding = maxBleeding;
                } else {
                    BulletScript.damage -= curHP;
                    curHP = 0;
                }
                //blood sprite which depends on the damage
                if (BulletScript.damage < 20) Instantiate(Blood1, new Vector2(col.transform.position.x, col.transform.position.y), Quaternion.Euler(0, 0, col.transform.rotation.eulerAngles.z + Random.Range(-15, 15) + 180));
                else Instantiate(Blood2, new Vector2(col.transform.position.x, col.transform.position.y), Quaternion.Euler(0, 0, col.transform.rotation.eulerAngles.z + Random.Range(-15, 15) + 180));
                //repulsion on collision with bullets:
                Body.AddForce(col.transform.right.normalized * BulletScript.repulsion);
                HPcolor();
            }
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag.Equals("Player")) {//collision stay with player
            Player.curHP -= 1;//damage to the player
            Player.delayTimer = Player.delay;//Update delay for regeneration
        }
    }

    public void HPcolor() {
        //HP COLOR
        float R;
        float G;

        if (curHP / HP > 0.5f) {//red color setting
            R = 1 - ((curHP - 0.5f * HP) / (0.5f * HP));
        }
        else R = 1;

        if (curHP / HP <= 0.5f) {//green color setting
            G = ((curHP) / (0.5f * HP));
        }
        else G = 1;

        HPMarker.color = new Color(R, G, 0, 1 - curHP / HP);//color setting to the hp marker
    }
    public void Death() {
        //DEATH
        if (curHP <= 0)//checking health points
        {
            Destroy(gameObject);//destroy enemy
            //instantiation death blood sprite
            Instantiate(BloodDead, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
            Finish.currentKills += 1;//counter dead enemies
            MainMenu.MONEY += reward;//reward accruing
        }
    }
}
