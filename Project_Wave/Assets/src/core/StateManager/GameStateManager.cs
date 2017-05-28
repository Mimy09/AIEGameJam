using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameState;


public class GameStateManager : MonoBehaviour{
	public Material m_water;

	// Stores the current state as a static for all round access.
	public static State m_currentState;
	public static void SetState(State state){
		// If the state is null, set the current state and end the function
		if (m_currentState == null) { m_currentState = state; return; }
		// If state is not the same as current state, call OnEnd() for the current state,
		// set the current state to state and call OnBegin() for the new current state
		if (state != m_currentState) {
			m_currentState.OnEnd ();
			m_currentState = state;
			m_currentState.OnBegin ();
		}// If state is the same as current state, update current state
		else {
			m_currentState = state;
			m_currentState.Update ();
		}
	}
	// Returns the current state as an Enum
	public static GAME_STATE GetState() { return m_currentState.GetSate(); }

	void Awake(){
		// Sets the current state to InitState
		SetState (new InitState ());
	}
	void Start(){
		// Sets the current state to MenuState
		SetState (new MenuState ());
	}
	void Update(){

		if (GetState () == GAME_STATE.GameRunningState) {
			m_water.mainTextureOffset = new Vector2 (0.005f, 0) + m_water.mainTextureOffset;

			Vector2 bOff = m_water.GetTextureOffset("_BumpMap");
			bOff.x += 0.01f;
			bOff.y += 0.0075f;
			m_water.SetTextureOffset("_BumpMap", bOff);

		}



		// Update the current state
		m_currentState.Update ();
	}
	void OnGUI(){
		// Call the GUI for the current state
		m_currentState.GUIUpdate ();
	}
}
