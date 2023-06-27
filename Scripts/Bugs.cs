using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bugs : MonoBehaviour
{
    public float speed = 0.3f; // 오브젝트의 이동 속도
    public float stopDistance = 0.1f; // 멈출 거리

    public RectTransform canvasRect; // 캔버스의 RectTransform 컴포넌트
    private Vector3 targetPosition; // 오브젝트가 이동할 목표 위치

    private bool isMoving; // 이동 중인지 여부를 나타내는 플래그

    private void Start()
    {
        // 초기 목표 위치 설정
        SetRandomTargetPosition();
        SetRandomSpeed();
    }

    private void Update()
    {
        if (isMoving)
    {
        // 현재 위치에서 목표 위치까지 이동
        Vector3 direction = targetPosition - transform.position; // 이동 방향 계산
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달했으면 새로운 목표 위치 설정
        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            isMoving = false;
            SetRandomTargetPosition();
        }
        else
        {
            // 이동 방향에 90도 회전
            Quaternion rotation = Quaternion.LookRotation(direction);
            Quaternion modifiedRotation = Quaternion.Euler(rotation.eulerAngles + new Vector3(90f, 0f, 0f));
            transform.rotation = modifiedRotation;
        }
    }
    }

    private void SetRandomTargetPosition()
    {
        // 캔버스의 너비와 높이를 기준으로 랜덤한 위치 생성
        float x = Random.Range(-canvasRect.rect.width / 2f, canvasRect.rect.width / 2f);
        float y = Random.Range(-canvasRect.rect.height / 2f, canvasRect.rect.height / 2f);

        // 캔버스 내에서의 위치를 월드 좌표로 변환
        targetPosition = canvasRect.TransformPoint(new Vector3(x, y, canvasRect.position.z));

        isMoving = true;
    }

    private void SetRandomSpeed()
    {
        speed = Random.Range(0.1f, 0.5f);
    }

        private void OnMouseDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1f))
            {
                Debug.Log(hit.transform.name);
                hit.transform.gameObject.SetActive(false);
                GameManager.Data.bugCatch++;
            }

        }
}
