using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private bool readyToTrigger = false;

    private Animator _anim;
    private Rigidbody _rb;
    private SpriteRenderer _spriteRenderer;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    [SerializeField] private float moveSpeed = 5.0f;
    private readonly int _facingRight = Animator.StringToHash("FacingRight");

    private Collider _collider;
    public CameraSwitcher cameraSwitcher;

    public UnityEvent EnableNextSprite;
   

    [CanBeNull] [SerializeField] private GameObject[] currentSprite;

    [CanBeNull] [SerializeField] private GameObject airWallToRestriction;
    [CanBeNull] private Collider _airWallCollider;


    public void TriggerCurrentFigure()
    {
        readyToTrigger = true;
    }

    public void DisableCurrentSprite()
    {
        if (currentSprite.Length > 0)
        {
            foreach (var sprite in currentSprite)
            {
                sprite.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider>();
        if (airWallToRestriction)
            _airWallCollider = airWallToRestriction.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToTrigger)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            cameraSwitcher.currentCameraIndex++;
            cameraSwitcher.EnableCamera(cameraSwitcher.currentCameraIndex);
            if (cameraSwitcher.currentCameraIndex >= 1)
                cameraSwitcher.DisableCamera(cameraSwitcher.currentCameraIndex - 1);
            EnableNextSprite.Invoke();
            if (airWallToRestriction)
                _airWallCollider.isTrigger = false;
        }

        if (other.CompareTag("Grandma"))
        {
            cameraSwitcher.StartDelayThenEnableNextCamera();
            EnableNextSprite.Invoke();
            if (cameraSwitcher.currentCameraIndex >= 1)
                cameraSwitcher.DisableCamera(cameraSwitcher.currentCameraIndex - 1);
            if (airWallToRestriction)
                _airWallCollider.isTrigger = false;
        }
    }
}