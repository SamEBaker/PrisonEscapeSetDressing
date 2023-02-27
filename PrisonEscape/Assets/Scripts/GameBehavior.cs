using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour
{
	private string _state;
	public string State 
	{
		get {return _state; }
		set { _state = value; }
	}

    public bool showWinScreen = false;
    public float moveSpeed = 10f;
    public bool showLossScreen = false;
	public string labelText = "Find a disguise and hijack a car!";
    public int maxItems = 5;
	private int _itemsCollected = 0;
	public int Items
    {
	    get { return _itemsCollected; }
		set {
		    _itemsCollected = value;
			if(_itemsCollected >= maxItems)
			{
				labelText = "You stole a car and left without a trace!";
				showWinScreen = true;
				Time.timeScale = 0f;
			}
			else
			{
				labelText = "Item found, only " + (maxItems - _itemsCollected) 
					+ " more to go!";
			}
		}
    }

    private int _playerHP = 10;
    public int HP
    {	
	    get {return _playerHP; }
		set {
			_playerHP = value;
			if(_playerHP <= 0)
			{
				labelText = "You been caught and thrown back in your cell! Maybe try avoiding them!";
				showLossScreen = true;
				Time.timeScale = 0;
			}
			else if(_playerHP > 10)
			{
				labelText = "Lookin' Nice!";
			}
			else
			{
				labelText = "Ouch...that's gotta hurt.";
			}
		}
	}
	void Start()
	{
		Initialize();
	}

	public void Initialize()
	{
		_state = "Manager initialized..";
		_state.FancyDebug();
		Debug.Log(_state);
	}
    void OnGUI()
    {
	    GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
		GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
		GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

		if (showWinScreen)
		{
			 if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "Drive Away!"))
			 {	
			     Cursor.lockState = CursorLockMode.None;
				 Cursor.visible = true;
				 Utilities.RestartLevel(0);
			 }
		}
		if (showLossScreen)
		{
		    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 -50, 200, 100), "Try Again!"))
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			    Utilities.RestartLevel();
			}
		}
    }
}
