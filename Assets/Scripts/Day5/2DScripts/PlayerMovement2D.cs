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


    [CanBeNull] [SerializeField] private GameObject[] currentBgPics;
    [SerializeField] private int currentPicIndex = -1;

    [SerializeField] private float bgPicFadeInTime;
    public UnityEvent TriggerFadeScreen;

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

        if (currentBgPics.Length > 0)
        {
            foreach (var bgPic in currentBgPics)
            {
                bgPic.SetActive(false);
            }
        }
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

        if (other.CompareTag("PictureTriggers"))
        {
            currentPicIndex++;
            other.enabled = false;
            currentBgPics[currentPicIndex].SetActive(true);
            StartCoroutine(fadeInBgPic());
        }

        if (other.CompareTag("SceneTrigger"))
        {
            TriggerFadeScreen.Invoke();
        }
    }

    private IEnumerator fadeInBgPic()
    {
        float startTime = Time.time;
        Color startColor = currentBgPics[currentPicIndex].GetComponent<SpriteRenderer>().color;
        Color targetColor = new Color(1f, 1f, 1f, 0.1f);

        while (Time.time - startTime < bgPicFadeInTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / bgPicFadeInTime);
            currentBgPics[currentPicIndex].GetComponent<SpriteRenderer>().color =
                Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // 设置 Image 的颜色为完全不透明的白色
        currentBgPics[currentPicIndex].GetComponent<SpriteRenderer>().color = targetColor;
    }
}