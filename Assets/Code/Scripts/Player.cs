using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform pos1, pos2, pos3, startpos;
    public float speed;
    Vector3 nextpos;
    bool _move = false;
    // Start is called before the first frame update
    void Start()
    {
        nextpos = startpos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _move = true;
        }
        if (_move)
        {
            Move1();
        }
    }
    void Move1()
    {
        if (transform.position == pos1.position)
            nextpos = pos2.position;

        if (transform.position == pos2.position)
            nextpos = pos3.position;

        if (transform.position == pos3.position)
            nextpos = pos1.position;        

        transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);

        if(transform.position == nextpos)
        {
            _move = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
        Gizmos.DrawLine(pos2.position, pos3.position);
        Gizmos.DrawLine(pos3.position, pos1.position);
    }
}
