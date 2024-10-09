using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonUtilityExample : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;    
    void Start()
    {
        // JsonTestClass jTest1 = new JsonTestClass();
        // string jsonData = JsonConvert.SerializeObject(jTest1);
       // string playerJsonData = JsonUtility.ToJson(player);
        //Debug.Log(playerJsonData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class JsonTestClass
    {
        public int i;
        public bool b;
        public JsonTestClass()
        {
            i = 10;
            b = true;
        }
        public void Print()
        {
            Debug.Log("iii = " + i);
            Debug.Log("bbb =  " + b);
        }
    }
}
