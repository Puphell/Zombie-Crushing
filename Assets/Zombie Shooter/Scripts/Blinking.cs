using UnityEngine;
using System.Collections;

public class Blinking : MonoBehaviour {//for mines and bleeding marker
    public GameObject go;
    public float time;
    private float timer;//current time
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            if(go.activeInHierarchy)go.SetActive(false);
            else go.SetActive(true);
            timer = time;
        }
	}
}
