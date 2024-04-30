using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rb;
    private SpriteRenderer _spriteRenderer;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    [SerializeField] private float moveSpeed = 5.0f;
    private readonly int _facingRight = Animator.StringToHash("FacingRight");

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rb.velocity = Vector3.zero;
        if (horizontalInput != 0)
        {
            // 根据水平输入方向计算移动向量
            Vector3 moveDirection = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
           
            var newPos = transform.position + moveDirection;
            _rb.MovePosition(newPos);
            _spriteRenderer.flipX = (horizontalInput < 0);
           
            _anim.SetBool(IsMoving, true);
        }
        else
        {
            // 当没有输入时停止动画
            _anim.SetBool(IsMoving, false);
        }
    }
}