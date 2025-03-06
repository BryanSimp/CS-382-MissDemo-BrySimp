using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidboySleep : MonoBehaviour
{
    private int sleepCountdown =4;
    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(sleepCountdown > 0){
            rigid.Sleep();
            sleepCountdown--;
        }
    }
}
