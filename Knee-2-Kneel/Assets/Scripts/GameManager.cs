using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    //player infos
    public int[] playerIndex = new int[2];
    //[0] is me, [1] is other

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    public static GameManager Instance
    {  
        get
        {
            if(instance == null)
            {
                return null;    
            }
            return instance;
        }
    }
}
