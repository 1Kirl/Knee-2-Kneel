using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    // void Start()
    // {
    //     dropdown.onValueChanged.AddListener(OnDropdownEvent);
    // }

    // Update is called once per frame
    void Awake(){
        //default: professor selected
        OnDropdownEvent(0);
    }
    void Update()
    {
        
    }
    public void OnDropdownEvent(int index)
    {
        Debug.Log("you choose: "+ index);
        GameManager.Instance.playerIndex[0] = index;
        GameManager.Instance.playerIndex[1] = (index-1)*(index-1);
    }
}
