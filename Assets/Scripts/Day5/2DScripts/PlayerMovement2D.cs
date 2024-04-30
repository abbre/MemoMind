using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rb;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    [SerializeField] private float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            // 根据水平输入方向计算移动向量
            Vector3 moveDirection = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
            // 应用移动向量到玩家的位置
            transform.Translate(moveDirection);
            
            // 翻转玩家的朝向
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
            
            // 设置动画参数
            _anim.SetBool(IsMoving, true);
        }
        else
        {
            // 当没有输入时停止动画
            _anim.SetBool(IsMoving, false);
        }
    }
}