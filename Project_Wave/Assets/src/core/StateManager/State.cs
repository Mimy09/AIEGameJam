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

		private GUIStyle title_style;
		private GUIStyle title_sub_style;

		private GUIStyle button_style; 

		public override void OnBegin(){
			title_style = new GUIStyle ();
			title_style.fontSize = 72;
			title_style.alignment = TextAnchor.MiddleCenter;

			title_sub_style = new GUIStyle ();
			title_sub_style.fontSize = 32;
			title_sub_style.alignment = TextAnchor.MiddleCenter;

			button_style = new GUIStyle ();
			button_style.fontSize = 46;
			button_style.alignment = TextAnchor.MiddleCenter;

		}

		public override void GUIUpdate (){
			GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), Resources.Load("textures/UI/menu") as Texture2D, ScaleMode.StretchToFill);

			GUI.Label (new Rect (0, (Screen.height / 2) - 97, Screen.width, 100), "Away've", title_style);
			GUI.Label (new Rect (0, (Screen.height / 2) - 35, Screen.width, 100), "Tidal Terror", title_sub_style);

			if (GUI.Button(new Rect(0, Screen.height  - 160, Screen.width, 100), "Start", button_style)){
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
		public PlayerStats m_ps_ui;

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
			m_ps_ui = m_ps;
			GUI.skin = skin;
			if (m_ps_ui.m_health > 100) m_ps_ui.m_health = 100;
			if (m_ps_ui.m_armor > 10) m_ps_ui.m_armor = 10;
			if (m_ps_ui.m_food > 100) m_ps_ui.m_food = 100;
			

			GUI.color = new Color(1, 1, 1, 1);
			GUI.DrawTexture(new Rect (20, 5, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 45, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 85, 200, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 125, 100, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect (20, 165, 100, 30), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);


			// Health
			GUI.color = new Color(1 - 1.0f / 100.0f * (float)m_ps.m_health, 1.0f / 100.0f * (float)m_ps.m_health, 0, 0.6f);
			GUI.DrawTexture(new Rect(60, 10, 150.0f / 100.0f * m_ps_ui.m_health, 20), texture, ScaleMode.StretchToFill , true, 10.0F);

			// Armor
			GUI.color = new Color(1 - 1.0f / 10.0f * (float)m_ps.m_armor, 1.0f / 10.0f * (float)m_ps.m_armor, 0, 0.6f);
			GUI.DrawTexture(new Rect(60, 50, 150.0f / 10.0f * m_ps_ui.m_armor, 20), texture, ScaleMode.StretchToFill , true, 10.0F);

			// Food
			GUI.color = new Color(1 - 1.0f / 100.0f * (float)m_ps.m_food, 1.0f / 10.0f * (float)m_ps.m_food, 0, 0.6f);
			GUI.DrawTexture(new Rect(60, 90, 150.0f / 100.0f * m_ps_ui.m_food, 20), texture, ScaleMode.StretchToFill , true, 10.0F);


			GUI.color = new Color(1, 1, 1, 1);
			GUI.Label (new Rect (60, 7, 200, 100), ((int)m_ps.m_health).ToString(), style);
			GUI.Label (new Rect (60, 47, 200, 100), ((int)m_ps.m_armor).ToString(), style);
			GUI.Label (new Rect (60, 87, 200, 100), ((int)m_ps.m_food).ToString(), style);


			GUI.Label (new Rect (60, 125, 200, 100), ((int)m_ps.m_speed).ToString(), style);
			GUI.Label (new Rect (60, 165, 200, 100), ((int)m_ps.m_parts).ToString(), style);

			GUI.DrawTexture(new Rect(30, 10, 20, 20), Resources.Load("textures/UI/health") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);
			GUI.DrawTexture(new Rect(30, 50, 20, 20), Resources.Load("textures/UI/armour") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);
			GUI.DrawTexture(new Rect(30, 90, 20, 20), Resources.Load("textures/UI/food") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);
			GUI.DrawTexture(new Rect(30, 130, 20, 20), Resources.Load("textures/UI/speed") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);
			GUI.DrawTexture(new Rect(30, 170, 20, 20), Resources.Load("textures/UI/parts") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);




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

		public override void Update (){
			if (Input.GetKeyDown (KeyCode.E)) {
				GameStateManager.SetState (new GameRunningState ());
			}
		}
	}



	public class IslandGUIState : State{
		public IslandGUIState(float food, int parts, float speed){
			timer = 3 / (speed - 1);
			m_food = food; m_parts = parts;
		}
		public override GAME_STATE GetSate(){ return GAME_STATE.IslandGUIState; }

		public override void Update(){
			timer -= Time.deltaTime;
		}

		public override void GUIUpdate (){
			GUI.color = new Color(1, 1, 1, 1);
			GUI.DrawTexture(new Rect (Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 50), Resources.Load("textures/UI/progressbar") as Texture2D, ScaleMode.StretchToFill);

			GUI.color = new Color(1, 0.8f, 0.23921f, 0.9f);
			GUI.DrawTexture(new Rect(Screen.width / 2 - 140, Screen.height / 2 - 40, 280.0f / 3.0f * timer, 30), Resources.Load("textures/blank") as Texture2D, ScaleMode.StretchToFill , true, 10.0F);
			//GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 50), ((int)timer + 1).ToString() + "s");

			GUI.color = new Color(1, 1, 1, 1);

			if (timer <= 0) {
				GameStateManager.SetState (new GameRunningState ());
			}
		}

		private float m_food;
		private int m_parts;

		private float timer;
	}
}