using UnityEngine;
using System.Collections;
using BallsLine.Implementation;

public class ScoreBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelState.Instance.RegisterScoreContext(this);
	}
}
