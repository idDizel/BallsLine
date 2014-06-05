using UnityEngine;
using System.Collections;
using BallsLine.Implementation;

public class FinishLevel : MonoBehaviour {

    void Start()
    {
        Invoke("LoadMenu", 7);
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        GUI.Label(new Rect(10, 10, 100, 100), string.Format("Score: {0}", LevelState.Instance.Score.ToString()));
        if(GUI.Button(new Rect(10, 40, 100, 100), "Start New Game"))
        {
            Application.LoadLevel("Level");
        }
        GUI.EndGroup();
    }

    void LoadMenu()
    {
        Application.LoadLevel("Menu");
    }
}
