using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    public Transform cameraTransform; // 카메라의 Transform 컴포넌트
    public float shakeAmount = 0.1f; // 흔들림의 강도
    public float shakeSpeed = 1f; // 흔들림의 속도

    private Vector3 initialPosition; // 카메라의 초기 위치
    private Animator characterAnimator; // 캐릭터의 Animator 컴포넌트

    private bool isWalk = false;
    private bool isRun = false;

    private Rigidbody myRigid;

    [SerializeField]
    private float camSensitivity;
    
    [SerializeField]
    private float camRotationLimit;
    private float currentCameraRotationX;
    
    [SerializeField]
    private Camera myCamera;



    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        if (cameraTransform == null)
        {
            // 카메라 Transform 컴포넌트를 찾아서 할당
            cameraTransform = myCamera.transform;
        }

        initialPosition = cameraTransform.localPosition; // 초기 위치 저장

    }

    // Update is called once per frame
    void Update()
    {
        TryRun();
        Move();
        CameraShake();
        //CameraRotation();
        CharacterRotation();
        if (Input.GetKeyDown(KeyCode.Space)){
            ResetView();
        }
    }

    private void TryRun(){
        
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D)){
            isWalk = true;
        }
        else isWalk = false;

        if(Input.GetKey(KeyCode.LeftShift)){
            isRun = true;
            applySpeed = runSpeed;
        }
        else{
            isRun = false;
            applySpeed = walkSpeed;
        }
        //Debug.Log(applySpeed);
    }

    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moverVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moverVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation(){
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * camSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -camRotationLimit, camRotationLimit);

        myCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation(){
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * camSensitivity;
        myRigid.MoveRotation(myRigid.rotation*Quaternion.Euler(_characterRotationY));
    }

    private void CameraShake(){
        if (isWalk)
        {
            float shakeY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2f - 1f;
            Vector3 shakeOffset = new Vector3(0f, shakeY, 0f) * shakeAmount;

            cameraTransform.localPosition = initialPosition + shakeOffset;
        }
        else
        {
            cameraTransform.localPosition = initialPosition;
        }
    }

    private void OnCollisionEnter(Collision col) {
        if(col.gameObject.name == "Door_Left_01"){
            SceneManager.LoadScene("Clear");
        }
        if(col.gameObject.name == "spider"){
            SceneManager.LoadScene("GameOver");
        }   
    }

    private void ResetView(){
 
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.y - cameraTransform.position.y; 

            Vector3 worldMousePosition = myCamera.ScreenToWorldPoint(mousePosition);

            Vector3 lookDirection = worldMousePosition - transform.position;
            lookDirection.y = 0f; 
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = targetRotation;
    }
}
