using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text clearCnt;
    public GameObject[] computers;
    public GameObject Door;

    public int clear;
    public int bugCatch;
    public GameObject[] Bugs;

    public GameObject Player;
    public GameObject MiniGame;
    public bool isMiniPlaying;

    private Vector3 origPosition;
    private Quaternion origRotation;

    public static GameManager Data;
    private void Awake() {

        if(Data == null){
            Data = this;
        }
        else if(Data != null) return;
    }

    // Start is called before the first frame update
    void Start()
    {
        computers = GameObject.FindGameObjectsWithTag("Computer");
        SelectCom();

    }

    // Update is called once per frame
    void Update()
    {
        clearCnt.text = "CLEAR " + clear + "/7";
        if(clear == 7){
            Door.SetActive(true);
        }

        if(isMiniPlaying && Player.GetComponent<PlayerManager>().enabled == true){

            Player.GetComponent<PlayerManager>().enabled = false;
            origPosition = Player.transform.position;
            origRotation = Player.transform.rotation;
            Debug.Log(origPosition);

            Player.transform.position = new Vector3(-45.41f, 100.7937f, 1.404251f);
            Player.transform.rotation = Quaternion.Euler(0,0,0);
            MiniGame.SetActive(true);
        }
        if(!isMiniPlaying && !Player.GetComponent<PlayerManager>().enabled == true){
            
            Debug.Log("돌아가기");
            Debug.Log(origPosition);

            Player.GetComponent<PlayerManager>().enabled = true;
            Player.transform.position = origPosition;
            Player.transform.rotation = origRotation;
            MiniGame.SetActive(false);

            for(int i=0; i<Bugs.Length; i++){
                Bugs[i].SetActive(true);
            }

        }

        if(bugCatch >= 5){
            Debug.Log("다 잡았음");
            bugCatch = 0;
            clear++;
            isMiniPlaying = false;
        }

    }

    void SelectCom(){

        for(int i=0; i<computers.Length; i++){
            computers[i].SetActive(false);
        }
        int cnt=0;
        while(cnt<7){
            int num = Random.Range(0,computers.Length);
            if(computers[num].activeSelf == false){
                computers[num].SetActive(true);
                cnt++;
            }
        }
    }

    public void ClickCom(){
        Debug.Log("클릭");
        clear++;
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

}
