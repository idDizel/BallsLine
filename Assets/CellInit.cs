using UnityEngine;
using System.Collections;

public class CellInit : MonoBehaviour {

    public Transform brick;

	// Use this for initialization
	void Start () {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                Transform obj = (Transform)Instantiate(brick, new Vector3(x*1.1f, y*1.1f, 0), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
