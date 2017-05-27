using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {
	public enum GAME_STATE{
		InitState,
		MenuState,
		GameRunningState,
		PauesState,
		IslandGUIState,
		_NULL_
	}
	/************************************************************
	*************************************************************/
	public abstract class State {
		/* ---- OnEnd ----
		Called when new state starts */
		virtual public void OnEnd (){}
		/* ---- OnBegin ----
		Called when current state changes to this state*/
		virtual public void OnBegin (){}
		/* ---- Update ----
		Called when state doesn't change*/
		virtual public void Update (){}

		virtual public void GUIUpdate (){}

		/* ---- GetState ----
		Returns the state as an enum*/
		virtual public GAME_STATE GetSate(){ return GAME_STATE._NULL_; }
	}
	/************************************************************
	*************************************************************/


	public class InitState : State{
		public InitState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.InitState; }

		public override void OnBegin (){
			
		}

	}



	public class MenuState : State{
		public MenuState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.MenuState; }

		public override void GUIUpdate (){
			if (GUI.Button(new Rect(0, 0, 100, 100), "START")){
				Debug.Log ("MENU");
				GameStateManager.SetState (new GameRunningState ());
			}
		}
	}



	public class GameRunningState : State{
		public GameRunningState(PlayerStats ps){ m_ps = ps; }
		public GameRunningState(){ }
		public override GAME_STATE GetSate(){ return GAME_STATE.GameRunningState; }

		public override void GUIUpdate (){
			GUI.Label (new Rect (10, 10, 200, 100), "Health = " + m_ps.m_health);
			GUI.Label (new Rect (10, 60, 200, 100), "Armor = " + m_ps.m_armor);
			GUI.Label (new Rect (10, 110, 200, 100), "Speed = " + m_ps.m_speed);
			GUI.Label (new Rect (10, 160, 200, 100), "Food = " + m_ps.m_food);

		}

		public PlayerStats m_ps;
	}



	public class PauesState : State{
		public PauesState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.PauesState; }
	}



	public class IslandGUIState : State{
		public IslandGUIState(float food, int parts){ m_food = food; m_parts = parts; }
		public override GAME_STATE GetSate(){ return GAME_STATE.IslandGUIState; }

		public override void GUIUpdate (){
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "Collect Resources \n food:" + m_food + "\n Parts:"+m_parts)){
				GameStateManager.SetState (new GameRunningState ());
			}
		}

		private float m_food;
		private int m_parts;
	}
}