using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode left;
    public KeyCode right;
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode Sprint;
    public KeyCode jump;
    public KeyCode sit;
    public KeyCode act1;
    public KeyCode act2;
    public GameObject CRotate;
    public GameObject Body;
    public GameObject WallMove;
    public int JumpCount;
    public int JumpLimit;
    public float MouseSpeed=50f;
    public float MoveSpeed;
    private bool GCheck;//gournd check
    private bool WCheck;//wall check
    private bool Dash;
    private Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        MoveSpeed=5f;
        Dash=false;
        WCheck = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(transform.rotation.eulerAngles);
        WGCheck();
     MouseMove();
        StartCoroutine(DashCheck());
         KeyMove();
        //if(!WCheck)
        //Gravity();
    }
    //void Gravity()
    //{
    //  transform.Translate(Vector3.down*Time.deltaTime* 10);
    //}
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag.Equals("Wall"))
        {
            WCheck = true;
            Quaternion lookatwall = WallMove.transform.rotation;
            lookatwall.y =Quaternion.LookRotation(col.contacts[0].point - transform.position).y;
            if (Dash && WCheck)
            {
                //transform.position += (col.transform.forward+new Vector3(0,1))
                
                if (Input.GetKey(forward))
                {



                    //float cx = contact.point.x;
                    //float cy = contact.point.z;
                    //float px = transform.position.x;
                    //float py = transform.position.z;
                    //float pz = transform.position.y;
                   
                        Vector3 a= (col.contacts[0].point - transform.position);
                   // Vector3 b = new Vector3(-1 / a.x, 0, -1 / a.z);
                        //transform.position -= (b + Vector3.up)*Time.deltaTime*MoveSpeed;
                        transform.position += (col.transform.forward + (Vector3.up/2)) * (MoveSpeed + 2.3f) * Time.deltaTime;
                    
                    //transform.position += (transform.forward+Vector3.up) * (MoveSpeed+ 2.3f) * Time.deltaTime; 
                }
                if (Input.GetKeyDown(jump))
                {
                    foreach (ContactPoint contact in col.contacts)
                    {
                        Debug.Log(contact.point + "wall jump!");
                        rb.AddExplosionForce(20, contact.point, 0.7f,2);
                    }
                }
            }
            Debug.Log("wall is ture");
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag.Equals("Wall"))
        {
            WCheck = false;
            Debug.Log("wall is false");
        }
    }
    void MouseMove()
    {
      float RotY=Input.GetAxisRaw("Mouse X")*MouseSpeed*Time.deltaTime;
      float RotX=Input.GetAxisRaw("Mouse Y")*MouseSpeed*Time.deltaTime;
      CRotate.transform.localEulerAngles -= new Vector3(RotX,0,0);
      Body.transform.localEulerAngles += new Vector3(0,RotY,0);
      // rb.MoveRotation(rb.rotation);
      Mathf.Clamp(CRotate.transform.localEulerAngles.x, -76, 85);

    }
    void WGCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f)) {
            GCheck = true;
            if (JumpCount < JumpLimit)
            {
                JumpCount = JumpLimit;
            }
            
        }
        else GCheck = false;
       // Debug.Log("ground check" + GCheck);
    }
    IEnumerator DashCheck()
    {
        if (Input.GetKey(Sprint))
        {
            Dash = true;
            if(MoveSpeed<11)MoveSpeed++; yield return new WaitForSeconds(0.1f);
        }
        else MoveSpeed = 5f; Dash = false;

      //{
      //  if(!Dash)
      //  {
      //    Dash=true;
      //    MoveSpeed=11f;
      //  }
      //  else
      //  {
      //    Dash=false;
      //    MoveSpeed=5f;
      //  }
      //}
    }
    void KeyMove()
    {
      if(Input.GetKey(key:left))
      {
       transform.Translate(Vector3.left*Time.deltaTime*(MoveSpeed*3/4));
      }
      if(Input.GetKey(key:right))
      {
        transform.Translate(Vector3.right*Time.deltaTime*(MoveSpeed*3/4));
      }
      if(Input.GetKey(key:forward))
      {
        transform.Translate(Vector3.forward*Time.deltaTime*MoveSpeed);
      }
      if(Input.GetKey(key:backward))
      {
        transform.Translate(Vector3.back*Time.deltaTime*(MoveSpeed*3/4));
      }
        if (Input.GetKeyDown(key: jump) && (JumpCount > 0))
        {
            //StartCoroutine(Jump());
            if ((Dash) && (Input.GetKey(forward)))
            {
                rb.AddForce((this.gameObject.transform.forward * 5 + this.gameObject.transform.up * 4), ForceMode.VelocityChange);
                JumpCount--;
                Debug.Log("long jump!");
            }
            else { rb.AddForce(new Vector3(0, 5, 0), ForceMode.VelocityChange); JumpCount--; Debug.Log("jump!"); }

        }
    }
    
    //IEnumerator Jump()
    //{ rb.AddForce(new Vector3(0, 5, 0), ForceMode.VelocityChange);
    //    GCheck = false;
    //    yield return new WaitForSeconds(0.1f);
    //}
}
