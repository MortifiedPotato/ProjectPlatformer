using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerMovement : MonoBehaviour, Input.IMoveInput
    {
        [Header("Movement Status")]
        public bool isGrounded;
        public bool isSwinging;
        public bool isJumping;

        bool[] groundColliders = new bool[3];

        [Header("Movement Attributes")]
        public float speed = 3f;
        public float jumpSpeed = 8f;
        public float climbSpeed = 3f;
        public float swingForce = 4f;
        public float yankForce = 7f;

        public float scoreCountDown;
        public int Score;

        public float fGroundedRememberTime = .25f;
        float fCutJumpHeight = .5f;
        public float fGroundedRemember;

        [Header("Object Variables")]
        public LayerMask groundCheckLayer;
        public SpriteRenderer playerSprite;
        GameObject dustParticle;
        Rigidbody2D rigidBody;
        [SerializeField] Animator anim;

        [HideInInspector]
        public Vector2 ropeHook;

        public Vector2 i_moveInput;
        public Vector2 velocity;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            dustParticle = Resources.Load("Particles/DustDirtyPoof") as GameObject;
        }

        private void Update()
        {
            CheckForGround();
            Animate();

            if (Time.frameCount%5==0)
            {
                velocity = rigidBody.velocity;
            }
        }

        private void FixedUpdate()
        {
            if (!PlayerBase.isPaused)
            {
                HandleMovement();
            }
        }

        void Animate()
        {
            if (isGrounded && !PlayerBase.isPaused)
            {
                anim.SetFloat("Speed", Mathf.Abs(GetComponent<PlayerMovement>().i_moveInput.x));
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }

            if (isGrounded && !PlayerBase.isPaused)
            {
                if (i_moveInput.x < 0)
                {
                    playerSprite.flipX = true;
                }
                else if (i_moveInput.x > 0)
                {
                    playerSprite.flipX = false;
                }
            }
            else
            {
                if (rigidBody.velocity.x < 0)
                {
                    playerSprite.flipX = true;
                }
                else if (rigidBody.velocity.x > 0)
                {
                    playerSprite.flipX = false;
                }
            }

        }

        public void HandleMovement()
        {
            if (i_moveInput.x < 0f || i_moveInput.x > 0f)
            {
                if (isSwinging)
                {
                    var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

                    Vector2 perpendicularDirection;
                    if (i_moveInput.x < 0)
                    {
                        perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                        var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                        Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                    }
                    else
                    {
                        perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                        var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                        Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                    }

                    var force = perpendicularDirection * swingForce;
                    rigidBody.AddForce(force, ForceMode2D.Force);
                }
                else
                {
                    if (isGrounded)
                    {
                        var groundForce = speed * 2f;
                        rigidBody.AddForce(new Vector2((i_moveInput.x * groundForce - rigidBody.velocity.x) * groundForce, 0));
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                    }
                }
            }

            if (!isSwinging)
            {
                if (!isGrounded) return;

                if (isJumping)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                }
            }
        }

        public void Yank()
        {
            if (ropeHook != Vector2.zero)
            {
                rigidBody.AddForce((new Vector3(ropeHook.x, ropeHook.y, 0) - transform.position) * (yankForce * 10));
                GetComponent<GrappleSystem>().ResetRope();
            }
        }

        public void CutJump()
        {
            if (rigidBody.velocity.y > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * fCutJumpHeight);
            }

            isJumping = false;
        }

        void CheckForGround()
        {
            var halfHeight = playerSprite.bounds.extents.y;
            groundColliders[0] = Physics2D.OverlapCircle(new Vector2(transform.position.x + 0.4f, transform.position.y - halfHeight), 0.1f, groundCheckLayer);
            groundColliders[1] = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - halfHeight), 0.1f, groundCheckLayer);
            groundColliders[2] = Physics2D.OverlapCircle(new Vector2(transform.position.x - 0.4f, transform.position.y - halfHeight), 0.1f, groundCheckLayer);

            fGroundedRemember -= Time.deltaTime;

            for (int i = 0; i < groundColliders.Length; i++)
            {
                if (groundColliders[i])
                {
                    fGroundedRemember = fGroundedRememberTime;
                    isGrounded = true;

                    return;
                }

                if (fGroundedRemember < 0 && !groundColliders[i])
                {
                    isGrounded = false;
                }
            }

            if (isJumping)
            {
                fGroundedRemember = -1;
            }
        }

        public void HandleMoveInput(Vector2 input)
        {
            i_moveInput = input;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Ground Impact Particle
            if (velocity.y < -5)
            {
                Instantiate(dustParticle, new Vector3(transform.position.x, transform.position.y - playerSprite.bounds.extents.y, transform.position.z + 1) , dustParticle.transform.rotation);
            }
            
            // Ground Impact Shake
            if (velocity.y < -8)
            {
                CameraManager.Instance.ShakeCamera(1, 0, 0);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            var halfHeight = playerSprite.bounds.extents.y;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0.4f, transform.position.y - halfHeight, -2), 0.1f);
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - halfHeight, -2), 0.1f);
            Gizmos.DrawWireSphere(new Vector3(transform.position.x - 0.4f, transform.position.y - halfHeight, -2), 0.1f);
        }
    }
}