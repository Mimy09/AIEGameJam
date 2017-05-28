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
		PlayerUpgradeState,
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
			if (GUI.Button(new Rect(0, 0, 100, 100), "Start")){
				Debug.Log ("MENU");
				GameStateManager.SetState (new GameRunningState ());
			}
		}
	}



	public class GameRunningState : State{
		public GameRunningState(PlayerStats ps){ m_ps = ps; }
		public GameRunningState(){ }
		public override GAME_STATE GetSate(){ return GAME_STATE.GameRunningState; }

		private GUIStyle style;
		public PlayerStats m_ps;
		private GUISkin skin;
		private Texture texture;

		public override void Update (){
			if (Input.GetKeyDown (KeyCode.E)) {
				GameStateManager.SetState (new PlayerUpgradeState ());
			}
		}

		public override void OnBegin(){
			style = new GUIStyle ();
			style.normal.textColor = Color.black;
			style.fontSize = 24;
			texture = Resources.Load("textures/blank") as Texture2D;
		}

		public override void GUIUpdate (){

			GUI.skin = skin;


			GUI.color = new Color(1, 1, 1, 1);
			GUI.DrawTexture(new Rect (20, 5, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 45, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 85, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 125, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);

			/*GUI.Label (new Rect (15, 10, 200, 100), "Health = " + (int)m_ps.m_health, style);
			GUI.Label (new Rect (15, 40, 200, 100), "Armor = " + (int)m_ps.m_armor, style);
			GUI.Label (new Rect (15, 70, 200, 100), "Speed = " + m_ps.m_speed, style);
			GUI.Label (new Rect (15, 100, 200, 100), "Food = " + (int)m_ps.m_food, style);*/

			// Health
			GUI.color = new Color(1 - 1.0f / 100.0f * (float)m_ps.m_health, 1.0f / 100.0f * (float)m_ps.m_health, 0, 0.6f);
			GUI.DrawTexture(new Rect(60, 10, 150.0f / 100.0f * m_ps.m_health, 20), texture, ScaleMode.StretchToFill , true, 10.0F);

			// Armor
			GUI.color = new Color(1 - 1.0f / 10.0f * (float)m_ps.m_armor, 1.0f / 10.0f * (float)m_ps.m_armor, 0, 0.6f);
			GUI.DrawTexture(new Rect(60, 50, 150.0f / 10.0f * m_ps.m_armor, 20), texture, ScaleMode.StretchToFill , true, 10.0F);

			// Speed
			GUI.color = new Color(1, 1, 1, 0.6f);
			//GUI.DrawTexture(new Rect(60, 50, 150.0f / 10.0f * m_ps.m_armor, 20), texture, ScaleMode.StretchToFill , true, 10.0F);
			//GUI.DrawTexture(new Rect(60, 50, 150.0f / 10.0f * m_ps.m_armor, 20), texture, ScaleMode.StretchToFill , true, 10.0F);

			GUI.Label (new Rect (60, 90, 200, 100), "Food = " + (int)m_ps.m_food, style);
			//GUI.Label (new Rect (15, 100, 200, 100), "Food = " + (int)m_ps.m_food, style);*/

			GUI.color = new Color(1, 1, 1, 1);
			GUI.DrawTexture(new Rect(30, 10, 20, 20), Resources.Load("textures/UI/health") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);

			if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 100, 100), "Upgrade")){
				GameStateManager.SetState (new PlayerUpgradeState ());
			}
		}


	}



	public class PauesState : State{
		public PauesState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.PauesState; }
	}



	public class PlayerUpgradeState : State {
		public PlayerUpgradeState(){}
		public override GAME_STATE GetSate(){ return GAME_STATE.PlayerUpgradeState; }

		public override void GUIUpdate (){
			
		}
	}



	public class IslandGUIState : State{
		public IslandGUIState(float food, int parts, float speed){
			timer = 3 / speed;
			m_food = food; m_parts = parts;
		}
		public override GAME_STATE GetSate(){ return GAME_STATE.IslandGUIState; }

		public override void Update(){
			timer -= Time.deltaTime;
		}

		public override void GUIUpdate (){
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 50), (timer + 1).ToString() + "s");
			if (timer <= 0) {
				GameStateManager.SetState (new GameRunningState ());
			}
		}

		private float m_food;
		private int m_parts;

		private float timer;
	}
}