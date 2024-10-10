using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class JsonTest : MonoBehaviour
{
    public GameObject player_student;
    private StarterAssetsInputs SAInput;
    void Start()
    {
        //Convert player's SAI script to Json
        SAInput = player_student.GetComponent<StarterAssetsInputs>();
        string jsonSAI = JsonUtility.ToJson(SAInput);
        Debug.Log(jsonSAI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
