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

		/* ---- GetState ----
		Returns the state as an enum*/
		virtual public GAME_STATE GetSate(){ return GAME_STATE._NULL_; }
	}
	/************************************************************
	*************************************************************/


	public class InitState : State{
		InitState(){  }
		public virtual GAME_STATE GetSate(){ return GAME_STATE.InitState; }
	}

	public class MenuState : State{
		MenuState(){}
		public virtual GAME_STATE GetSate(){ return GAME_STATE.MenuState; }
	}

	public class GameRunningState : State{
		GameRunningState(){}
		public virtual GAME_STATE GetSate(){ return GAME_STATE.GameRunningState; }
	}

	public class PauesState : State{
		PauesState(){}
		public virtual GAME_STATE GetSate(){ return GAME_STATE.PauesState; }
	}
}