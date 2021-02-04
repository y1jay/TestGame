using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 얼마나 빠른속도로 대상을 쫒을건지
    private Vector3 targetPosition; // 대상의 현재 위치값


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null){
           targetPosition.Set(target.transform.position.x, 
           target.transform.position.y, this.transform.position.z);// 카메라는 타겟과 값이 똑같아지면 안보인다 그래서 자기값을 가지고 있어야해서 this로 설정(생략가능)
           this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
           //러프는 A와 B의 사이에서 중간값을 리턴 델타타임은 1초에 moveSpeed 만큼 이동
        }
    }
}
