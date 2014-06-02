using UnityEngine;
using System.Collections;
using GameLevel;
using Game1;
using System.Linq;

public class GameState : MonoBehaviour {

    public Transform ball;
    Core core;
    Ray ray;
    RaycastHit hit;
    Transform selectedBall;

	// Use this for initialization
	void Start () {
        core = new Core();
        this.UpdateState();
	}
	
    void UpdateState()
    {
        foreach(var a in core.Generate())
        {
            Transform el = (Transform)Instantiate(ball, new Vector3(a.X * 1.1f, a.Y * 1.1f, -1), Quaternion.identity);
            el.renderer.material.color = core.ColorTransformer(core.LevelArray[a.X, a.Y]);
            el.GetComponent<Values>().X = a.X;
            el.GetComponent<Values>().Y = a.Y;
            el.GetComponent<Values>().cellType = core.LevelArray[a.X, a.Y];
        }
    }
	// Update is called once per frame
	void Update () {
        

	    if(Input.GetKeyDown(KeyCode.Space))
        {
            
            UpdateState();
        }
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag=="Ball")
                if (this.selectedBall != null)
                {
                    if (this.selectedBall.Equals(hit.transform))
                    {
                        this.selectedBall.transform.Translate(0, 0, 1.0f);
                        this.selectedBall = null;
                    }
                    else
                    {
                        this.selectedBall.transform.Translate(0, 0, 1.0f);
                        this.selectedBall = hit.transform;
                        this.selectedBall.transform.Translate(new Vector3(0, 0, -1.0f));
                    }
                }
                else
                {
                    this.selectedBall = hit.transform;
                    this.selectedBall.transform.Translate(new Vector3(0, 0, -1.0f));
                }

                Debug.Log(string.Format("SelectedBall {0}", selectedBall));

                if(hit.transform.tag=="Cell")
                {
                    Debug.Log(string.Format("Cell X:{0} Y:{1} Value:{2}", hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y, core.LevelArray[hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y]));

                    if (this.selectedBall != null && core.LevelArray[hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y] == CellType.Empty)
                    {
                        core.LevelArray[hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y] =
                            core.LevelArray[this.selectedBall.GetComponent<Values>().X, this.selectedBall.GetComponent<Values>().Y];
                        core.LevelArray[this.selectedBall.GetComponent<Values>().X, this.selectedBall.GetComponent<Values>().Y] = CellType.Empty;

                        this.selectedBall.transform.position = new Vector3(hit.transform.GetComponent<CellValues>().X*1.1f, hit.transform.GetComponent<CellValues>().Y*1.1f, -1);
						this.selectedBall.GetComponent<Values>().X = hit.transform.GetComponent<CellValues>().X;
						this.selectedBall.GetComponent<Values>().Y = hit.transform.GetComponent<CellValues>().Y;

                        var clearList = core.CheckLine(hit.transform.GetComponent<CellValues>().X, hit.transform.GetComponent<CellValues>().Y, this.selectedBall.GetComponent<Values>().cellType);
                        foreach (var element in clearList)
                        {
                            foreach (var desEl in GameObject.FindGameObjectsWithTag("Ball").Where(x => x.GetComponent<Values>().X == element.X && x.GetComponent<Values>().Y == element.Y))
                            {
                                Destroy(desEl);
                            }
                        }
                        if(clearList.Count()==0) this.UpdateState();
                        this.selectedBall = null;
                    }
                }
                //Debug.Log(string.Format("X:{0}, Y{1}", this.selectedBall.GetComponent<Values>().X, this.selectedBall.GetComponent<Values>().Y));
            }
        }
	}
}
