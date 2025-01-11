using UnityEngine;
using System.Collections;

public class SpawnerPoint : MonoBehaviour {
    public float delay;//start delay
    public float spawn_time;//spawn interval
    public int limit;//enemies in spawner
    public GameObject trigger;//activating trigger (can be not defined)
    public GameObject Enemy;//if you want to use other objects - remove the spawners tag, otherwise the finish will count these objects as enemies

    private float timer;//current interval

    void Update() {
        if (trigger == null) {// if trigger activated OR not defined
            if (delay > 0) delay -= Time.deltaTime;//start delay
            else if (timer > 0) timer -= Time.deltaTime;//ELSE decrease interval timer
            else if (limit > 0) {//ELSE if spawner has enemies - spawn enemy:
                Instantiate(Enemy, new Vector2(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.1f, 0.1f)), transform.rotation);//spawn
                timer = spawn_time;//reset spawn timer
                limit -= 1;//decrease spawner enemy count 
            }
        }
    }
}
