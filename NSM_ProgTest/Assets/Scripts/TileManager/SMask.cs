using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMask : MonoBehaviour
{
    [SerializeField] SpriteMask outMask;
    public Transform target;
    private void Awake()
    {
        outMask.alphaCutoff = 0.0f;
    }
    public void Update()
    {
        if ( target!=null)
        transform.position = Vector3.MoveTowards(transform.position, target.position, 20 * Time.deltaTime);            
    }

}
