using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using TMPro;



#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		//packet's list
		public int playerIndex;
		public int roomId;
		public string dataName;
		[Header("Character Input Values")]
		public Vector2 move;
		public bool jump;
		public bool sprint;
		public bool kick;

		//datas which are not in packet
		[NonSerialized]
		public Vector2 look;
		[Header("Movement Settings")]
		[NonSerialized]
		public bool analogMovement = false;

		[Header("Mouse Cursor Settings")]
		[NonSerialized]
		public bool cursorLocked = true;
		[NonSerialized]
		public bool cursorInputForLook = true;

		//Must not be Serialized by JsonUtility, also not be in packet
		[NonSerialized]
		public Vector2 move_t;
		[NonSerialized]
		public Vector2 look_t;
		[NonSerialized]
		public bool jump_t;
		[NonSerialized]
		public bool sprint_t;
		[NonSerialized]
		public bool kick_t;
		//private GameObject role_manager;
		private GameObject udp_manager;
		private UDPManager udp_manager_cs;
		private string SAInput_out_json;
		private JObject SAInput_out_json_nt;
		private string jumpbool;
		private string kickbool;
		private string sprintbool;
		private string cIFLbool;
		void Start()
		{
			//role_manager = GameObject.Find("roleManager");
			udp_manager = GameObject.Find("udp_manager");
			udp_manager_cs = udp_manager.GetComponent<UDPManager>();
			SAInput_out_json_nt = new JObject(); //nt for newton
			//Reference sigleton object's variable
			Debug.Log("received role: "+ playerIndex);
		}
		void Update(){
			SAInput_out_json = udp_manager_cs.receivedMessage;
			Debug.Log("input SAI received from UDPManager: " + SAInput_out_json);
			SAInput_out_json_nt =  JObject.Parse(SAInput_out_json);
			
			if((int)SAInput_out_json_nt["playerIndex"] == playerIndex)
			{
				move_t.x = (float)SAInput_out_json_nt["move"]["x"];
				move_t.y = (float)SAInput_out_json_nt["move"]["y"];
				
				jumpbool = (string)SAInput_out_json_nt["jump"];
				jump_t = jumpbool[0] == 'T' ? true : false;
		
				kickbool = (string)SAInput_out_json_nt["kick"];
				kick_t = kickbool[0] == 'T' ? true : false;

				sprintbool = (string)SAInput_out_json_nt["sprint"];
				sprint_t = sprintbool[0] == 'T' ? true : false;
			}
		}
#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnKick(InputValue value)
		{
			KickInput(value.isPressed);
		}

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void KickInput(bool newKickState)
		{
			kick = newKickState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}