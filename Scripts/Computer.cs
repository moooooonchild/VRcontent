using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Computer : MonoBehaviour
{
    public GameObject MiniGame;

    private void Start() {
        MiniGame = GameObject.Find("FindMini").transform.Find("MiniGame").gameObject;
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.name == "TV 32 inch 1"){
                hit.transform.parent.gameObject.SetActive(false);
                GameManager.Data.isMiniPlaying = true;
            }

        }

    }


}
