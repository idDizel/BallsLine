using UnityEngine;
using System.Collections;
using Game1;
using BallsLine.Implementation;

public class BallBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnMouseDown()
    {
        LevelState.Instance.SelectBall(this.gameObject);
    }
}
