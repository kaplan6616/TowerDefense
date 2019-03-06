using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private bool isMoveEnable=true;
    public float speed = 30f;
    public float scrollSpeed=100f;
    public float minYAxis=10f;
    public float maxYAxis=80f;
    Vector3 pos;
    // Update is called once per frame
    void Update()
    {
        if(GameMenager.GameIsOver==true)
        {
            this.enabled = false;
            return;
        }
        pos = transform.position;
        if(!isMoveEnable)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isMoveEnable = !isMoveEnable;
        }
        if(Input.GetKey(KeyCode.W) /*|| Input.mousePosition.y>=Screen.height-10f*/)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) /*|| Input.mousePosition.y < 10f*/)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) /* ||Input.mousePosition.x > Screen.width - 10f*/)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A)/* || Input.mousePosition.x < 10f*/)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if(Input.GetAxis("Mouse ScrollWheel")>0f)
        {
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
            pos.y= Mathf.Clamp(transform.position.y, minYAxis, maxYAxis);
           transform.position = pos;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
            pos.y = Mathf.Clamp(transform.position.y, minYAxis, maxYAxis);
            transform.position = pos;
        }
    }
}
