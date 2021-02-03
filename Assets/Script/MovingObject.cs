using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //벽에 부딫히면 멈추게 
    private BoxCollider2D boxCollider2D;

    // 속도값 
    public float speed;
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    public int walkCount;
    private int currentWalkCount;
    // speed = 2.4, walkCount = 20
    // 2.4 * 20 = 48
    // While 문 사용 current 필요
    // currentWalkCount += 1, 20,

    private bool canMove = true;

    private Animator animator;

    // 하나의 변수로 3개의 변수를 갖는것 
    // xy z 좌표가 있다면 동시에 갖는것 vector는 세개를 갖고있어서
    // 세개를 선언하라고 하기 때문
    private Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
   IEnumerator MoveCoroutine(){
       while(Input.GetAxisRaw("Vertical")!=0||Input.GetAxisRaw("Horizontal")!=0){
       if(Input.GetKey(KeyCode.LeftShift)){
             applyRunSpeed=runSpeed;
             applyRunFlag = true;
         }
         else{
          applyRunSpeed=0;
          applyRunFlag=false;
          }
           vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
         if(vector.x!=0)
            vector.y=0;
          // vector.x!=0 0이 아닐시 위아래 정보를 가져올 필요가 없다

          animator.SetFloat("DirX", vector.x);
          animator.SetFloat("DirY", vector.y);
          animator.SetBool("Walking",true);
         // 상하좌우일때 인수를 DirX로 받는것 애니메이터에 입력

         while(currentWalkCount< walkCount){
           // 상하 좌우 모두 움직일 수 있는 기능 구현
           if(vector.x !=0){
               //Translate == 현재 있는값에서 옆에 수치만큼 더해준다
               transform.Translate(vector.x * (speed+applyRunSpeed), 0,0);
               // 움직일 수 있는 다양한 방법이 있다 
               // transfrom.position=vector
           }else if(vector.y !=0){
               transform.Translate(0, vector.y * (speed+applyRunSpeed),0);
           }
          if(applyRunFlag)
           currentWalkCount++;
           currentWalkCount++;
             yield return new WaitForSeconds(0.01f);
         }
         currentWalkCount=0;
   
       }
         animator.SetBool("Walking",false);
       canMove=true;
   }
   //1초간 기다리게 하고 다중처리를 가능하게 한다.

    // Update is called once per frame
    void Update()
    {
        // 방향키가 눌렸을때 처리 방식 
        //Input.GetAxisRaw("Horizontal") 우 방향키가 눌리면 1 리턴
        // 좌 방향키가 눌리면 -1리턴
        //Input.GetAxisRaw("Vertical") 상방향키가 눌리면 1 리턴
        // 하 방향키가 눌리면 -1리턴
        if(canMove){
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") !=0){
         canMove=false;
         StartCoroutine(MoveCoroutine());
        }
        }
    }
}
