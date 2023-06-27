using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void StartGame(){
        Debug.Log("게임시작");
        SceneManager.LoadScene("GameMap");
    }
    
    public void GoToStart(){
        Debug.Log("시작으로");
        SceneManager.LoadScene("Start");
    }
}
