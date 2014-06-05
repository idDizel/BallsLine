using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
            GUI.Box(new Rect(0, 0, 200, 170), GUIContent.none);
            GUI.Label(new Rect(10, 10, 180, 25), "High score: ");
            if(GUI.Button(new Rect(10, 40, 180, 30), "Play"))
            {
                Application.LoadLevel("Level");
            }
            GUI.Button(new Rect(10, 80, 180, 30), "Options");

            if(GUI.Button(new Rect(10, 120, 180, 30), "Exit"))
            {
                Application.Quit();
            }
        GUI.EndGroup();
    }
}
