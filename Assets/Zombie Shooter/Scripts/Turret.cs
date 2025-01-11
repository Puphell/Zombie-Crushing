using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    public float maxdist = 10;//detection distance
    public GameObject Marker;//the allocation of the enemy-target
    public float cooldown;//interval between shots
    public int ammo;//ammo limit
    public float accuracy;//+- degrees
    public AudioClip shoot;//sound of shot
    public Transform shootpoint_1;//can be NULL
    public Transform shootpoint_2;//can be NULL
    //described in detail below (in SHO...OT)

    //Bullet
    public GameObject Bullet;//bullet
    public float Damage;
    public float Bleeding;//bleeding coefficient (multiplied by damage)
    public float Speed;//(6-12)
    public float Repulsion;//enemy repulsion on bullet hits
    
    private float mindist;//used in cycle
    private GameObject nearest = null;//nearest enemy
    private bool HasTarget = false;
    private RaycastHit2D hit;//for the visibility system
    private float cur_cooldown;//current
    private AudioSource audio;
    
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (cur_cooldown > 0) { cur_cooldown -= Time.deltaTime; }//cooldown

        mindist = 100;//reset distance the nearest target
        nearest = null;//reset targets

        #region Finding nearest enemy
        //making all enemies list
        GameObject[] List;
        List = GameObject.FindGameObjectsWithTag("Enemy");
        //find the nearest Enemy
        foreach (GameObject go in List) {
            hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)go.transform.position - (Vector2)transform.position, 20, 513);//a ray from the turret to the enemy
            if (hit.collider != null) {
                if (!hit.collider.gameObject.CompareTag("Wall")) {//ray does not touch the walls
                    float tmp = Vector3.Distance(transform.position, go.transform.position);
                    if (tmp < mindist & tmp < maxdist) {//if the distance is minimal and is included in the range
                        mindist = tmp;
                        nearest = go;
                    }
                    //show rays:
                    //Debug.DrawLine((Vector2)transform.position, (Vector2)go.transform.position);
                }
            }
        }
        #endregion

        //rotating to target
        if (nearest != null) {//if has target
            HasTarget = true;
            Marker.SetActive(true);//marker activation

            //follow the target
            Vector3 moveDirection = nearest.transform.position - transform.position;
            if (moveDirection != Vector3.zero) {
                float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }

            if (cur_cooldown <= 0 & ammo > 0) {//SHOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOT
                //if used spawn point
                if (shootpoint_1 != null) Shoot(shootpoint_1.transform);
                //if NOT used spawn point's (using turret position to shoot)
                else Shoot(transform);
                //if used 2 spawn point
                if (shootpoint_2 != null) Shoot(shootpoint_2.transform);
                
                audio.PlayOneShot(shoot, MainMenu.volume*0.6f);//play shot sound
                cur_cooldown = cooldown;//reset cooldown
                ammo -= 1;//decrease ammo
            }
        } else {
            HasTarget = false;
            Marker.SetActive(false);//marker deactivation
        }
	}
    void LateUpdate() {
        if (Marker.activeInHierarchy & nearest != null) Marker.transform.position = nearest.transform.position;//moving marker to target pos
    }

    void Shoot(Transform pos) {//bullet characteristics setting
        var B = ((GameObject)Instantiate(Bullet, new Vector2(pos.position.x, pos.position.y), Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-accuracy, accuracy)))).GetComponent<Bullet>();
        B.damage = Damage;
        B.bleeding = Bleeding;
        B.speed = Speed;
        B.repulsion = Repulsion;
    }
}
