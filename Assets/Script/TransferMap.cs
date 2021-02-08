using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMap : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름

    public Transform target;
    public BoxCollider2D targetBound;
    private MovingObject thePlayer;
    private CameraManager theCamera;

    // Start is called before the first frame update
    void Start()
    {
    
        theCamera = FindObjectOfType<CameraManager>();

        thePlayer = FindObjectOfType<MovingObject>(); //GetComponent와 비슷하지만 검색범위가 더 넓음
    }
    //아래 내장함수의 기능은 박스컬라이더에 닿으면 바로 즉각적으로 실행되는 함수
    // 박스컬라이더의 온 트리거가 활성화 됐을 경우 실행되는 내장함수이기 때문
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Player"){
            thePlayer.currentMapName = transferMapName;
            theCamera.SetBound(targetBound);
           theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
           thePlayer.transform.position = target.transform.position;
     
        }
    }
}
