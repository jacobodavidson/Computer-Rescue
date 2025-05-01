using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.5f;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    CapsuleCollider2D touchingCol;
    Animator animator;

    [SerializeField]
    private bool _isGrounded;

    public bool IsGround
    { 
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        IsGround = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }

}
