using UnityEngine;

namespace SoulHunter.Player
{
    public class PlayerMovement : MonoBehaviour, Input.IMoveInput
    {
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

        bool[] groundColliders = new bool[3];

        [Header("Object Variables")]
        public LayerMask groundCheckLayer;
        public SpriteRenderer playerSprite;
        GameObject dustParticle;
        Rigidbody2D rigidBody;

        [HideInInspector]
        public Vector2 ropeHook;

        public Vector2 i_moveInput;
        public Vector2 velocity;

        private void Awake()
        {
            dustParticle = Resources.Load("Particles/DustDirtyPoof") as GameObject;
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckForGround();

            if (Time.frameCount%5==0)
            {
                velocity = rigidBody.velocity;
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        public void HandleMovement()
        {
            if (PlayerBase.isPaused)
            {
                return;
            }

            if (i_moveInput.x < -0.01f || i_moveInput.x > 0.01f)
            {
                if (PlayerBase.isSwinging)
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
                    if (PlayerBase.isGrounded)
                    {
                        var groundForce = speed * 2f;
                        rigidBody.AddForce(new Vector2((i_moveInput.x * groundForce - rigidBody.velocity.x) * groundForce, 0));
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                    }
                }
            }

            if (!PlayerBase.isSwinging)
            {
                if (!PlayerBase.isGrounded) return;

                if (PlayerBase.isJumping)
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

            PlayerBase.isJumping = false;
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
                    PlayerBase.isGrounded = true;

                    return;
                }

                if (fGroundedRemember < 0 && !groundColliders[i])
                {
                    PlayerBase.isGrounded = false;
                }
            }

            if (PlayerBase.isJumping)
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
                if (velocity.y < -12)
                {
                    CameraManager.Instance.ShakeCamera(2, 6, 0);
                }
                else
                {
                    CameraManager.Instance.ShakeCamera(1, 0, 0);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            var halfHeight = playerSprite.bounds.extents.y;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0.4f, transform.position.y - halfHeight, -2), 0.1f);
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - halfHeight, -2), 0.1f);
            Gizmos.DrawWireSphere(new Vector3(transform.position.x - 0.4f, transform.position.y - halfHeight, -2), 0.1f);
        }
    }
}