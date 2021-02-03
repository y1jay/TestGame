using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    // 속도값 
    public float speed;

    // 하나의 변수로 3개의 변수를 갖는것 
    // xy z 좌표가 있다면 동시에 갖는것 vector는 세개를 갖고있어서
    // 세개를 선언하라고 하기 때문
    private Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키가 눌렸을때 처리 방식 
        //Input.GetAxisRaw("Horizontal") 우 방향키가 눌리면 1 리턴
        // 좌 방향키가 눌리면 -1리턴
        //Input.GetAxisRaw("Vertical") 상방향키가 눌리면 1 리턴
        // 하 방향키가 눌리면 -1리턴
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") !=0){
         
           vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Horizontal"), transform.position.z);
         
          // 상하 좌우 모두 움직일 수 있는 기능 구현
           if(vector.x !=0){
               //Translate == 현재 있는값에서 옆에 수치만큼 더해준다
               transform.Translate(vector.x * speed, 0,0);
           }else if(vector.y !=0){
               transform.Translate(0, vector.y * speed,0);
           }

        }
    }
}
