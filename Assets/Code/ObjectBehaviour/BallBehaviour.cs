using UnityEngine;
using BallsLine.Implementation;
using BallsLine.Entities;
using BallsLine.Interfaces;

public class BallBehaviour : MonoBehaviour, IElementNotifier
{
    public Position Position{get; set;}
	// Use this for initialization
	void Start () {
	
	}
	
    void OnMouseDown()
    {
        LevelState.Instance.SelectElement(this);
    }


    public void Selected()
    {
        transform.Translate(new Vector3(0, 0, -1.0f));
    }

    public void Unselected()
    {
        transform.Translate(0, 0, 1.0f);
    }

    public void PositionChanged()
    {
        transform.position = new Vector3(Position.X * 1.1f, Position.Y * 1.1f, -1.0f);
    }

    public void Removed()
    {
        throw new System.NotImplementedException();
    }
}
