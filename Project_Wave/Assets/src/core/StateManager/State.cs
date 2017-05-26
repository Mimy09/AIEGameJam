using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {
	public enum GAME_STATE{
		InitState,
		MenuState,
		GameRunningState,
		PauesState,
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
		public GameRunningState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.GameRunningState; }
	}



	public class PauesState : State{
		public PauesState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.PauesState; }
	}
}