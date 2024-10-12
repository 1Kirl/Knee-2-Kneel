using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager_t : MonoBehaviour
{

    private string sceneName = "Sample_Office1";
    public void LoadNewScene(){
        SceneManager.LoadScene(sceneName);
    }
}
