using BallsLine.Enums;
using BallsLine.Implementation;
using BallsLine.Interfaces;
using UnityEngine;


public class LevelBehaviour : MonoBehaviour {

    LevelState levelState;

    public MonoBehaviour RedBall;
    public MonoBehaviour GreenBall;
    public MonoBehaviour YellowBall;
    public MonoBehaviour BlueBall;
    public MonoBehaviour PurpleBall;
    public MonoBehaviour OrangeBall;
    public GameObject Cell;

    private GameObject Ball;

	// Use this for initialization
	void Start () {
        this.levelState = LevelState.Instance;
        this.levelState.GenerateLevel();
        this.levelState.GenerateCells(Cell);
        
        this.levelState.MapPrefab((IElementNotifier)RedBall, ElementType.Red);
        this.levelState.MapPrefab((IElementNotifier)BlueBall, ElementType.Blue);
        this.levelState.MapPrefab((IElementNotifier)GreenBall, ElementType.Green);
        this.levelState.MapPrefab((IElementNotifier)YellowBall, ElementType.Yellow);
        this.levelState.MapPrefab((IElementNotifier)OrangeBall, ElementType.Orange);
        this.levelState.MapPrefab((IElementNotifier)PurpleBall, ElementType.Purple);

        this.levelState.GenerateBalls();
	}
}
