using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    // Start is called before the first frame update
    public int Forward_Jump=10;
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
       col.gameObject.GetComponent<Rigidbody>().AddForce((col.transform.forward*Forward_Jump)+new Vector3(0,12), ForceMode.VelocityChange);
    }

    
    void Update()
    {
        
    }
}
