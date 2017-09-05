using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    private System.Random rng = new System.Random();
    public GameObject[] Building;
    public GameObject[] Car;
    public GameObject[] Object;
    private List<GameObject> WindowStuff = new List<GameObject>();
    private float buffer = 20;
    private float aLittleSpace = 1;
    Transform parentObject;
    int speed = 12;
    Vector3 step = new Vector3();

    void Awake() {
        for (int ss = 0; ss < 4; ss++) {
            newDecor(true,ss);
        }
    }

    public void Scroll(float delta, float totalDistance) {
        Vector3 offset = Vector3.forward * delta;
        aLittleSpace -= offset.z;

        if (WindowStuff.Count > 0) {
            for (int xx = WindowStuff.Count - 1; xx >= 0; xx--) {
                GameObject House = WindowStuff[xx];
                House.transform.position -= offset;
                if (House.transform.position.z <= -30) {
                    WindowStuff.Remove(House);
                    Destroy(House);
                }
            }
        }

        if (WindowStuff.Count < 26 && aLittleSpace <= 0) {
            newDecor();
        }
    }

    // - / +31 + 1/2 of scale length = left / right margin
    private void newDecor(bool intro = false, int space = 0) { 
        Vector3 pos = new Vector3();
        //Quaternion rot = new Quaternion();
        GameObject Tile;

        int style = 0;
        style = rng.Next(1, 3);
        int asset;
        switch (style) {
            case 1:asset = rng.Next(0, Building.Length); Tile = (GameObject)Instantiate(Building[asset], parentObject); break;
            case 2:asset = rng.Next(0, Car.Length); Tile = (GameObject)Instantiate(Car[asset], parentObject); break;
            default: asset = rng.Next(0, Object.Length); Tile = (GameObject)Instantiate(Object[asset], parentObject); break;
        }
        aLittleSpace = rng.Next(1, 4);
        if (intro) pos.x = rng.Next(21, 31) - (20 * space); //random position on screen
        else pos.x = 0-(Tile.transform.localScale.x * 0.5f)-31; //accurate position just off screen
        Tile.transform.position += pos;
        aLittleSpace = -31 + (Tile.transform.localScale.x * 1.5f) + Tile.transform.position.x;

        WindowStuff.Add(Tile);
    }
    public void Update() {
        foreach(GameObject thing in WindowStuff){
            step.x = speed * Time.deltaTime;
            thing.transform.position += step;
        }
        aLittleSpace += step.x;
        //Debug.Log(aLittleSpace);
    }

}