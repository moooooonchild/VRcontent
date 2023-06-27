using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    Rigidbody rigid;
    public NavMeshAgent nav;

    public AudioSource audioSource; // AudioSource 컴포넌트
    public float maxDistance = 50f; // 최대 허용 거리

    private Vector3 initialPosition;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float volume = 1f - nav.remainingDistance / maxDistance; // 거리에 따라 0~1 사이의 값을 얻음
        audioSource.volume = volume;

        if(target.position.y > 50f){
            nav.SetDestination(initialPosition);
            audioSource.Stop();
        }
        else{
            nav.SetDestination(target.position);
            if(nav.remainingDistance >= 50f){
                nav.isStopped = true;
                //Debug.Log("중지");
                audioSource.Stop();
            }
            else {
                nav.isStopped = false;
                if (!audioSource.isPlaying)
                {
                    Debug.Log("플레이");
                    audioSource.Play();
                }
            }

        }
        
    }

    void FixedUpdate() {
        FreezeVelocity();    
    }

    void FreezeVelocity(){
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
