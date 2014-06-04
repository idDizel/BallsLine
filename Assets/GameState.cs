using BallsLine.Enums;
using BallsLine.Implementation;
using UnityEngine;


public class GameState : MonoBehaviour {

    LevelState levelState;

    public GameObject RedBall;
    public GameObject GreenBall;
    public GameObject YellowBall;
    public GameObject BlueBall;
    public GameObject PurpleBall;
    public GameObject OrangeBall;
    public GameObject Cell;

    private GameObject Ball;

	// Use this for initialization
	void Start () {
        this.levelState = LevelState.Instance;
        this.levelState.GenerateLevel();
        this.levelState.GenerateCells(Cell);
        
        this.levelState.MapPrefab(RedBall, BallType.Red);
        this.levelState.MapPrefab(BlueBall, BallType.Blue);
        this.levelState.MapPrefab(GreenBall, BallType.Green);
        this.levelState.MapPrefab(YellowBall, BallType.Yellow);
        this.levelState.MapPrefab(OrangeBall, BallType.Orange);
        this.levelState.MapPrefab(PurpleBall, BallType.Purple);

        this.levelState.GenerateBalls();
	}
}
