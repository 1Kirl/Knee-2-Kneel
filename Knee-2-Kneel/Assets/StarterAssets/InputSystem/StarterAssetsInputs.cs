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
		public int roomId;
		public string dataName;
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool kick;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		//Must not be Serialized by JsonUtility
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
		[NonSerialized]
		public bool analogMovement_t;
		[NonSerialized]
		public bool cursorLocked_t = true;
		[NonSerialized]
		public bool cursorInputForLook_t = true;


		private GameObject udp_manager;
		private UDPManager udp_manager_cs;
		private string SAInput_out_json;
		private JObject SAInput_out_json_nt;
		private string jumpbool;
		private string kickbool;
		private string sprintbool;
		private string cIFLbool;
		void Start(){
			udp_manager = GameObject.Find("udp_manager");
			udp_manager_cs = udp_manager.GetComponent<UDPManager>();
			SAInput_out_json_nt = new JObject();

		}
		void Update(){
			SAInput_out_json = udp_manager_cs.receivedMessage;
			SAInput_out_json_nt =  JObject.Parse(SAInput_out_json);
			move_t.x = (float)(SAInput_out_json_nt["move"]["x"]);
			move_t.y = (float)(SAInput_out_json_nt["move"]["y"]);
			// look.x = (float)(SAInput_out_json_nt["look"]["x"]);
			// look.y = (float)(SAInput_out_json_nt["look"]["y"]);

			jumpbool = (string)SAInput_out_json_nt["jump"];
			jump_t = jumpbool[0] == 'T' ? true : false;
	
			kickbool = (string)SAInput_out_json_nt["kick"];
			kick_t = kickbool[0] == 'T' ? true : false;

			sprintbool = (string)SAInput_out_json_nt["sprint"];
			sprint_t = sprintbool[0] == 'T' ? true : false;

			// cIFLbool = (string)SAInput_out_json_nt["cursorInputForLook"];
			// cursorInputForLook_t = cIFLbool[0] == 'T' ? true : false;

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
			Debug.Log(sprint);
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}