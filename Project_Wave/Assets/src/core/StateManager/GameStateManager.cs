using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameState;


public class GameStateManager{

	// ---- PRIVATE ----
	private GAME_STATE m_currentState;

	// ---- PUBLIC ----
	public void SetState(State state){ m_currentState = state.GetSate(); }
	public GAME_STATE GetState() { return m_currentState; }
}
