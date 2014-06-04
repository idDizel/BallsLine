using UnityEngine;
using BallsLine.Implementation;
using BallsLine.Entities;

public class BallBehaviour : MonoBehaviour {
    [HideInInspector]
    public Position position;
	// Use this for initialization
	void Start () {
	
	}
	
    void OnMouseDown()
    {
        LevelState.Instance.SelectBall(this.gameObject);
    }
}
