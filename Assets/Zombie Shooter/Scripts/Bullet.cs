using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float damage;
    public float bleeding;//bleeding coefficient (multiplied by damage)
    public float speed;//(6-12)
    public float repulsion;//enemy repulsion on bullet hits
    public float lifetime;
    public float explosionRange;//for non-explosive bullets: 0
    
    public GameObject DestroyParticle;//on collision with the wall
    public GameObject Explosion;

	void Start () {
        Destroy(gameObject, lifetime);//destroy over lifetime
    }

	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * speed);//bullet movement
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {//bullets do not cause damage to the player
            EXPLOSION();
            Destroy(gameObject);//bullet destroy
        }
        if (col.gameObject.CompareTag("Wall")) {
            EXPLOSION();
            Destroy(gameObject);//bullet destroy and instantiate sparks effect
            Instantiate(DestroyParticle, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 0));
        }
        if (col.gameObject.CompareTag("Enemy")) EXPLOSION();//explosion on collision with enemy
    }

    void EXPLOSION() {
        if (explosionRange > 0) {//explosion characteristics setting
            var E = ((GameObject)Instantiate(Explosion, new Vector2(transform.position.x, transform.position.y), Quaternion.Inverse(transform.rotation))).GetComponent<Explosion>();
            E.damage = damage;
            E.explosionRange = explosionRange;
            E.repulsion = repulsion;
            Destroy(gameObject);
        }
    }
}
