using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public float damage;//damage at the epicenter
    public float explosionRange;
    public float repulsion;//enemy repulsion at the epicenter

    void Start() {
        GameObject[] List;//making all enemies list
        List = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in List) {
            float dist = Vector2.Distance(transform.position, go.transform.position);
            if (dist < explosionRange) {//if the distance included in the range
                Rigidbody2D en = go.GetComponent<Rigidbody2D>();//enemy repulsion
                en.AddForce(
                    (explosionRange - dist) / explosionRange * //distance from the epicenter (0-1)
                    repulsion * //repulsive force
                    ((go.transform.position - transform.position) / (dist)));//unit direction vector

                go.GetComponent<Enemy>().curHP -= (damage * (explosionRange - dist) / explosionRange); //damage at the epicenter is multiplied by the distance coefficient (0-1)
                go.GetComponent<Enemy>().Death();
                go.GetComponent<Enemy>().HPcolor();
            }
        }

        GameObject PL = GameObject.FindGameObjectWithTag("Player");//player damage
        float PLdist = Vector2.Distance(transform.position, PL.transform.position);

        float resistance = 2f;//player damage resistance 
        if (PLdist < explosionRange / resistance) Player.HP -= (damage * (explosionRange / resistance - PLdist) / explosionRange);//if the distance included in the range
    }
}
