using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;



public class RoleManager : MonoBehaviour
{
    public GameObject player_student;
    public GameObject player_professor;
    public GameObject udp_manager;
    public GameObject playerFollowCam;
    public int my_playerIndex;
    public int other_playerIndex;
    private CinemachineVirtualCamera whichToFollow;
    private UDPManager udp_manager_sc;
    void Awake()
    {

        my_playerIndex = GameManager.Instance.playerIndex[0];
        other_playerIndex = GameManager.Instance.playerIndex[1];
        udp_manager_sc = udp_manager.GetComponent<UDPManager>();
        whichToFollow = playerFollowCam.GetComponent<CinemachineVirtualCamera>();
        if(my_playerIndex == 0)
        {
            //if I'm Professor, set other as student
            player_professor.GetComponent<StarterAssetsInputs>().playerIndex = my_playerIndex;
            player_student.GetComponent<StarterAssetsInputs>().playerIndex = other_playerIndex;

            //bind client's player to udp_manager
            udp_manager_sc.player_obj = player_professor;

            //bind camera to me!!
            whichToFollow.Follow = player_professor.transform.Find("CameraRoot");

            //enable / disable components
            player_student.GetComponent<PlayerInput>().enabled = false;
            player_professor.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            //vice versa
            player_professor.GetComponent<StarterAssetsInputs>().playerIndex = other_playerIndex;
            player_student.GetComponent<StarterAssetsInputs>().playerIndex = my_playerIndex;
            
            //bind client's player to udp_manager
            udp_manager_sc.player_obj = player_student;

            //bind camera to me!!
            whichToFollow.Follow = player_student.transform.Find("CameraRoot");
            
            //enable / disable components
            player_student.GetComponent<PlayerInput>().enabled = true;
            player_professor.GetComponent<PlayerInput>().enabled = false;
        }
    }
}
