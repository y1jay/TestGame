using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;
    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 얼마나 빠른속도로 대상을 쫒을건지
    private Vector3 targetPosition; // 대상의 현재 위치값

   public BoxCollider2D bound;
   private Vector3 minBound;
   private Vector3 maxBound;
   // 박스 컬라이더 영역의 최소 최대 xyz 값을 지님

   private float halfWidth;
   private float halfHeght;
   //카메라를 반너비와 반높이 값을 지닐 변수

   private Camera theCamera;

   // 카메라의 반높이 값을 구할 속성을 이용하기 위한 변수.

   // Awake = 앱이 실행되면 바로 실행되는 함수 start 보다 먼저 실행된다
   private void Awake() {
        if(instance !=null){
      Destroy(this.gameObject);
        }
        else{
     
        DontDestroyOnLoad(this.gameObject);
         instance = this;
        }
   }


    // Start is called before the first frame update
    void Start()
    {
       theCamera = GetComponent<Camera>();
       minBound = bound.bounds.min;
       maxBound = bound.bounds.max;
       halfHeght = theCamera.orthographicSize;
       halfWidth = halfHeght * Screen.width / Screen.height;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null){
           targetPosition.Set(target.transform.position.x, 
           target.transform.position.y, this.transform.position.z);// 카메라는 타겟과 값이 똑같아지면 안보인다 그래서 자기값을 가지고 있어야해서 this로 설정(생략가능)
           this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
           //러프는 A와 B의 사이에서 중간값을 리턴 델타타임은 1초에 moveSpeed 만큼 이동
           float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth,maxBound.x-halfWidth);
           float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeght,maxBound.y-halfHeght);
           this.transform.position = new Vector3(clampedX,clampedY,transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound){
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
