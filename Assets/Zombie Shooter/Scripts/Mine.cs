using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
    public float damage;
    public float explosionRange;
    public float repulsion;//repulsion of enemies by explosion

    public GameObject Explosion;

    void OnTriggerEnter2D(Collider2D col) {
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
