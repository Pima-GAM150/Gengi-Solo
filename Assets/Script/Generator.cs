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
    public int speed = 12;
    Vector3 step = new Vector3();
    bool beat = false;

    void Awake() {
        for (int ss = 0; ss < 4; ss++) {
            newDecor(8,true,ss);
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
    private void newDecor(int style = 8, bool intro = false, int space = 0) { 
        Vector3 pos = new Vector3();
        //Quaternion rot = new Quaternion();
        GameObject Tile;

        if (style == 8) style = rng.Next(0, 3);
        int asset;
        int depth = rng.Next(-5, 5);
        switch (style) {
            case 1:asset = rng.Next(0, Building.Length); Tile = (GameObject)Instantiate(Building[asset], parentObject); break;
            case 2:asset = rng.Next(0, Car.Length); Tile = (GameObject)Instantiate(Car[asset], parentObject); break;
            default: asset = rng.Next(0, Object.Length); Tile = (GameObject)Instantiate(Object[asset], parentObject); break;
        }
        aLittleSpace = rng.Next(1, 4);
        if (intro) pos.x = rng.Next(1, 12) - (6 * space); //random position on screen
        else pos.x = 0-(Tile.transform.localScale.x * 0.5f)-12; //accurate position just off screen
        pos.z += (float)depth/10;
        Tile.transform.position += pos;
        aLittleSpace = -12 + (Tile.transform.localScale.x * 1.5f) + Tile.transform.position.x;

        WindowStuff.Add(Tile);
    }
    public void Update() {
        foreach(GameObject thing in WindowStuff){
            /*if (beat) {
                step.y += 0.01f;
                if (step.y >= 0.03) beat = false;
            }
            else {
                step.y -= 0.01f;
                if (step.y < 0) step.y = 0;
            }*/
            /*switch (rng.Next(0,3)) {
                case 1: step.y = Time.deltaTime; break;
                case 2: step.y -= Time.deltaTime; break;
                case 3: step.y = 0.01f;break;
                default: break;
            }*/          
            step.x = speed * Time.deltaTime;
            thing.transform.position += step;
        }
        aLittleSpace += step.x;
        if (aLittleSpace > -2) {
            newDecor();
        }
    }

    public void Kill(GameObject obj) {
        //Debug.Log("K");
        //foreach (GameObject tile in WindowStuff) {
            WindowStuff.Remove(GameObject.Find(obj.name));
            Destroy(obj);
       // }
    }
    public void Pole() {
        newDecor(3);

    }
    public void Beat() {
        beat = true;
        newDecor();
    }
    public void Rando() {
        newDecor();
    }
}