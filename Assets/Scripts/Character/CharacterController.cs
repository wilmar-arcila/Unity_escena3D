using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character.State
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(StatsManager))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Movimiento del personaje")]
        [Tooltip("Velocidad de desplazamiento del personaje")]
        [Range (0.0f, 5.0f)]
        [SerializeField] private float speed = 2f;

        [Tooltip("Velocidad de rotación del personaje")]
        [Range (20.0f, 45.0f)]
        [SerializeField] private float rot = 30f;

        [Tooltip("Fuerza de salto del personaje")]
        [Range (5f, 10f)]
        [SerializeField] private float force = 5f;

        [Space (50)]
        [Header("Sonidos de personaje")]
        [Tooltip("Personaje aburrido")]
        [SerializeField] private AudioSource bored_SFX;

        public StateMachine stateMachine {get; private set;}
        public bool onTheGround {get; private set;}
        private GameManager manager;
        private Rigidbody rb;
        private Animator animator;
        private StatsManager stats;

        private float fallSensibility = 0.5f; // Alcanzada esta velocidad descendente se considera que el personaje está cayendo

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            stats = GetComponent<StatsManager>();
            stateMachine = new StateMachine(this);
        }
        
        void Start()
        {
            manager = GameManager.getInstance();
            stateMachine.Initialize(stateMachine.idleState);
            onTheGround = false;
        }
        
        void Update()
        {
            stateMachine.Update();
        }

        private void OnCollisionEnter(Collision other) {
            if(other.collider.CompareTag("Ground")){
                onTheGround = true;
            }
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("TriggerEnter: " + other.gameObject.name);
            if(other.CompareTag("Collectable")){
                stats.RaiseScore(1);
            }
        }

        private void OnParticleCollision(GameObject other){
            Debug.Log("Particle HIT (collision): " + other.name);
        }

        public void idle(){
            animator.SetFloat("speed_LR", 0);
            animator.SetFloat("speed_FR", 0);
            animator.SetTrigger("idle");
        }

        public void move(float speedLR, float speedFR, float rotDir){
            animator.SetTrigger("move");
            animator.SetFloat("speed_LR", speedLR);
            animator.SetFloat("speed_FR", speedFR);
            //transform.Translate(moveDirection*moveIntensity*speed*Time.deltaTime,0,moveIntensity*speed*Time.deltaTime);
            transform.Rotate(0,rotDir*rot*Time.deltaTime,0);
        }

        public void jump(){
            animator.SetTrigger("jump");
            rb.AddForce(new Vector3(0,force*rb.mass,0), ForceMode.Impulse);
            onTheGround = false;
        }

        // ** Detector de movimiento descendente **
        public bool verifyFalling(){
            /* if(rb.velocity.y < -fallSensibility){
                animator.SetTrigger("fall");
                return true;
            }
            else{
                return false;
            } */
            return false;
        }
    }
}
