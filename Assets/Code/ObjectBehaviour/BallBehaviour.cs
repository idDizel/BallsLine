using UnityEngine;
using BallsLine.Implementation;
using BallsLine.Entities;
using BallsLine.Code.Interfaces;

public class BallBehaviour : MonoBehaviour, IPosition<Position>
{
    public Position Position{get; set;}
	// Use this for initialization
	void Start () {
	
	}
	
    void OnMouseDown()
    {
        LevelState.Instance.SelectBall(this.gameObject);
    }
}
