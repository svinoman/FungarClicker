using UnityEngine;
using UnityEngine.AI;

public class GribusAI : MonoBehaviour
{
    public static int score;
    public int hp = 1;
    public float speed;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _walkPointRange;
    [SerializeField] private Vector3 _walkPoint;
    private AudioSource _defeatAudio;
    private NavMeshAgent _gribusAgent;
    private Animator _gribusAnimator;
    private bool _walkPointSet;
    private float _walkTimer = 0;
    private GribusSpawner _gribusSpawner;

    private void Awake()
    {
        _defeatAudio = GameObject.FindGameObjectWithTag("DefeatAudio").GetComponent<AudioSource>();
        _gribusAgent = GetComponent<NavMeshAgent>();
        _gribusAnimator = GetComponent<Animator>();
        _gribusSpawner = GameObject.FindGameObjectWithTag("GribusManager").GetComponent<GribusSpawner>();
        hp = _gribusSpawner.defaultHp;
    }
    private void Update()
    {
        _gribusAgent.speed = _gribusSpawner.currentSpeed;
        Patrol();
        if(hp <= 0)
        {
            _gribusAnimator.SetBool("isDefeated", true);
        }
    }
    private void Patrol()
    {
        if (!_walkPointSet)
            SearchWalkPoint();

        if (_walkPointSet)
            _gribusAgent.SetDestination(_walkPoint);

        Vector3 _distanceToWalkPoint = transform.position - _walkPoint;

        if (_distanceToWalkPoint.magnitude < 2f)
            _walkPointSet = false;

        _walkTimer += Time.deltaTime;
        if (_walkTimer >= 10f)
        {
            _walkPointSet = false;
            _walkTimer = 0f;
        }

    }
    private void SearchWalkPoint()
    {
        float _randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float _randomX = Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(_randomX, transform.position.y, _randomZ);
        if (Physics.Raycast(_walkPoint, -transform.up, 5f, _whatIsGround))
        {
            _walkPointSet = true;
        }
    }
    private void DestroyAnimationEvent()
    {
        Destroy(gameObject);
    }
    private void RemoveOneGribus()
    {
        _defeatAudio.pitch = Random.Range(0.9f, 1.1f);
        _defeatAudio.Play();
        _gribusSpawner.currentGribsAmount--;
        if(UIButtons.isPlaying == true)
        {
            score += 5;
        }
    }    
}
