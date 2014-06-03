using UnityEngine;
using System.Collections;
using GameLevel;
using Game1;
using System.Linq;
using BallsLine.Implementation;
using System.Collections.Generic;
using BallsLine.Entities;
using BallsLine.Enums;

public class GameState : MonoBehaviour {

    public Transform ball;
    Core core;
    Ray ray;
    RaycastHit hit;
    Transform selectedBall;
    LevelState levelState;

    public GameObject RedBall;
    public GameObject GreenBall;
    public GameObject YellowBall;
    public GameObject BlueBall;
    public GameObject PurpleBall;
    public GameObject OrangeBall;
    private GameObject Ball;

	// Use this for initialization
	void Start () {
        this.levelState = LevelState.Instance;
        this.levelState.GenerateLevel();
        
        this.levelState.MapPrefab(RedBall, BallType.Red);
        this.levelState.MapPrefab(BlueBall, BallType.Blue);
        this.levelState.MapPrefab(GreenBall, BallType.Green);
        this.levelState.MapPrefab(YellowBall, BallType.Yellow);
        this.levelState.MapPrefab(OrangeBall, BallType.Orange);
        this.levelState.MapPrefab(PurpleBall, BallType.Purple);
	}
	
    void UpdateState(IEnumerable<BallEntity> balls)
    {
        foreach(var a in balls)
        {
            this.levelState.GetBallByType(a.BallType, out Ball);
            a.gameObject = (GameObject)Instantiate(Ball, new Vector3(a.BallPosition.X * 1.1f, a.BallPosition.Y * 1.1f, -1), Quaternion.identity);
        }
    }
	// Update is called once per frame
	void Update () {
        

	    if(Input.GetKeyDown(KeyCode.Space))
        {
            this.levelState.GenerateBalls(this.UpdateState);
            
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if(hit.transform.tag=="Ball")
                

        //        Debug.Log(string.Format("SelectedBall {0}", selectedBall));

        //        if(hit.transform.tag=="Cell")
        //        {
        //            Debug.Log(string.Format("Cell X:{0} Y:{1} Value:{2}", hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y, core.LevelArray[hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y]));

        //            if (this.selectedBall != null && core.LevelArray[hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y] == CellType.Empty)
        //            {
        //                core.LevelArray[hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y] =
        //                    core.LevelArray[this.selectedBall.GetComponent<BallBehaviour>().X, this.selectedBall.GetComponent<BallBehaviour>().Y];
        //                core.LevelArray[this.selectedBall.GetComponent<BallBehaviour>().X, this.selectedBall.GetComponent<BallBehaviour>().Y] = CellType.Empty;

        //                this.selectedBall.transform.position = new Vector3(hit.transform.GetComponent<CellValues>().X*1.1f, hit.transform.GetComponent<CellValues>().Y*1.1f, -1);
        //                this.selectedBall.GetComponent<BallBehaviour>().X = hit.transform.GetComponent<CellValues>().X;
        //                this.selectedBall.GetComponent<BallBehaviour>().Y = hit.transform.GetComponent<CellValues>().Y;

        //                var clearList = core.CheckLine(hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y, this.selectedBall.GetComponent<BallBehaviour>().cellType);
        //                foreach (var element in clearList)
        //                {
        //                    foreach (var desEl in GameObject.FindGameObjectsWithTag("Ball").Where(x => x.GetComponent<BallBehaviour>().X == element.X && x.GetComponent<BallBehaviour>().Y == element.Y))
        //                    {
        //                        Destroy(desEl);
        //                    }
        //                }
        //                //if(clearList.Count()==0) this.UpdateState();
        //                this.selectedBall = null;
        //            }
        //        }
        //        //Debug.Log(string.Format("X:{0}, Y{1}", this.selectedBall.GetComponent<Values>().X, this.selectedBall.GetComponent<Values>().Y));
        //    }
        //}
	}
}
