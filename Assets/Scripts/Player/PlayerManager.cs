using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)] private float walkSpeed;
    [SerializeField, Range(1f, 50f)] private float sprintSpeed;
    [SerializeField, Range(1f, 50f)] private float jumpSpeed;
    private bool CanMove { get; set; }
    private bool IsSprinting { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
