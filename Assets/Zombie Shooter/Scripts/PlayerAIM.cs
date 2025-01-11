using UnityEngine;
using System.Collections;

public class PlayerAIM : MonoBehaviour {
    public Transform Camera;//main camera
    public static float CamZoom = 6;//Zoom
    public GameObject Marker;//the allocation of the enemy-target
    public float maxdist = 10;//detection distance
    public Transform Laser;//laser line sprite
    public Transform LaserDirection;//laser direction transform
    public Transform Point;//luminous point sprite

    private GameObject nearest = null;//nearest enemy
    public static bool HasTarget = false;//it used in PlayerShooting (shooting only if player has target)
    private RaycastHit2D hit2;//for the visibility system
    public float mindist;//used in cycle
	void Start () {
        Marker.SetActive(false);//marker deactivation
	}
	
	void Update () {
        if (MainMenu.ControlMode == "Touch") {
            mindist = 100;//reset distance the nearest target
            nearest = null;//reset targets

            //making all enemies list
            GameObject[] List;
            List = GameObject.FindGameObjectsWithTag("Enemy");
            //find the nearest Enemy
            foreach (GameObject go in List) {
                hit2 = Physics2D.Raycast((Vector2)transform.position, (Vector2)go.transform.position - (Vector2)transform.position, 20, 513);//a ray from the player to the enemy
                if (hit2.collider != null) {
                    if (!hit2.collider.gameObject.CompareTag("Wall")) {//ray does not touch the walls
                        float tmp2 = Vector3.Distance(transform.position, go.transform.position);
                        if (tmp2 < mindist & tmp2 < maxdist) {//if the distance is minimal and is included in the range
                            mindist = tmp2;
                            nearest = go;
                        }
                        //show rays:
                        //Debug.DrawLine((Vector2)transform.position, (Vector2)go.transform.position);
                    }
                }
            }

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
            } else {//rotating to move direction
                HasTarget = false;
                Marker.SetActive(false);//marker deactivation
                transform.rotation = Quaternion.Euler(0, 0, Player.Move_Angle + 90);
            }

            for (int i = 0; i < Input.touchCount; ++i) {//touch control (Zoom)
                Vector2 pos = new Vector2(Input.GetTouch(i).position.x / Screen.width, Input.GetTouch(i).position.y / Screen.height);//touch position(x,y) (0-1)
                if (Input.GetTouch(i).phase == TouchPhase.Began) {//one touch
                    if (pos.x > 0.1f & pos.x < 0.2f & pos.y > 0.7f & pos.y < 0.85f & CamZoom < 8) CamZoom += 1;
                    if (pos.x <= 0.1f & pos.y > 0.7f & pos.y < 0.85f & CamZoom > 3) CamZoom -= 1;
                }
            }
        }
	}
    void LateUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 & CamZoom < 8) CamZoom += 1;//Zoom mousewheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0 & CamZoom > 3) CamZoom -= 1;

        if (Marker.activeInHierarchy) Marker.transform.position = nearest.transform.position;//moving marker to target pos
        Camera.transform.position = new Vector3(transform.position.x, transform.position.y, -CamZoom);//moving camera to player pos
        if (MainMenu.ControlMode == "Mouse")
        {
            var pos = Input.mousePosition;//Mouse Control
            pos.z = transform.position.z - UnityEngine.Camera.main.transform.position.z;
            pos = UnityEngine.Camera.main.ScreenToWorldPoint(pos);
            var q = Quaternion.FromToRotation(Vector3.right, pos - transform.position);
            transform.rotation = q;
            if (transform.rotation.y != 0) transform.rotation = Quaternion.Euler(0, 0, -180);//bugfix
        }

        //LASER SIGHT
        if (MainMenu.ControlMode == "Mouse")
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)Laser.position, LaserDirection.position - Laser.position);//the ray from the player in sight direction
            if (hit.collider != null)
            {
                Laser.localScale = new Vector2(hit.distance * 25, Laser.localScale.y);
                Point.position = new Vector2(hit.point.x, hit.point.y);

                if (hit.collider.gameObject.CompareTag("Enemy") & PlayerShooting.PointBlank != "Wall") {//color selection
                    Laser.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                    Point.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                }
                else
                {
                    Laser.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                    Point.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                }

                if (PlayerShooting.PointBlank == "Wall") {//point-blank to the walls
                    Laser.localScale = new Vector2(0, Laser.localScale.y);
                    Point.localPosition = new Vector2(0, 0);
                }
            } else {//the ray dоnt touch obstacles
                Laser.localScale = new Vector2(200, Laser.localScale.y);
                Point.localPosition = (LaserDirection.position - Laser.position) * 22.6f;
            }
        }
        else { 
            Laser.localScale = new Vector2(0, 0);
            Point.localScale = new Vector2(0, 0);
        }
    }
}
