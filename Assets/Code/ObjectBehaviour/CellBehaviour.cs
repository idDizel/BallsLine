using UnityEngine;
using System.Collections;
using BallsLine.Entities;
using BallsLine.Implementation;

public class CellBehaviour : MonoBehaviour {
    [HideInInspector]
    public Position position;
	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        LevelState.Instance.ChangePosition(position);
    }
}
