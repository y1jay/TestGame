using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
  // static = MovingObject가 적용된 모든 객체들은 변수값을 공유한다 
  static public MovingObject instance;
 public string currentMapName; // transferMap 스크립트에 있는 transferMapName  변수의 값을 저장

    //벽에 부딫히면 멈추게 
    private BoxCollider2D boxCollider;

    //통과가 불가능한 레이어를 설정해주는것
    public LayerMask layerMask;


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
      if(instance ==null){
        DontDestroyOnLoad(this.gameObject);// 이 게임오브젝트를 다른씬으로 갈 때 파괴하지말라
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        instance =this;
      }else{
        Destroy(this.gameObject);
      }
       
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

          RaycastHit2D hit;
          // A지점 B지점이 있을때 레이저를 쏴서 
          // B지점까지 무사히 도달한다면 hit==null
          // 무사히 도달하지 못하면 hit==방해물이 리턴

          Vector2 start = transform.position; // A지점  캐릭터의 현재 위치값
          Vector2 end = start+new Vector2(vector.x*speed*walkCount,vector.y*speed*walkCount); // B지점  캐릭터가 이동하고자 하는 위치값
         
         // 캐릭터가 자기본연의값 boxcollider를 가지고 있는데 본인이쏘고 본인이 맞을수가 있어서 잠시 해제해준다
          boxCollider.enabled = false;
         //레이저를 쏘는것
           hit = Physics2D.Linecast(start,end,layerMask);
         // 캐릭터 자기 본연의값을 다시 세팅해준다 
         boxCollider.enabled = true;
         
         if(hit.transform !=null)
         break;

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
