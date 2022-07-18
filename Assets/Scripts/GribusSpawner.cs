using UnityEngine;

public class GribusSpawner : MonoBehaviour
{
    public int defaultHp = 1;
    public int currentGribsAmount;
    public float currentSpeed = 3.5f;
    public bool clearLevel;
    [SerializeField] private GameObject _gribusPrefab;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private GameObject _freezePrefab;
    private AudioSource _spawnAudio;
    private float _spawnPointRangeZ = 6f;
    private float _spawnPointRangeX = 8.8f;
    private float _spawnTimer;
    private float _spawnRate = 5f;
    private float _boosterTimer;
    private float _boosterRate;
    private void Start()
    {
        _spawnAudio = GameObject.FindGameObjectWithTag("SpawnAudio").GetComponent<AudioSource>();
        _boosterRate = Random.Range(10f, 30f);
    }
    private void Update()
    {
        ClearLevel();
        if (UIButtons.isPlaying)
        {
            GribusProcess();
            BoosterProcess();
            Gameover();
        }
    }
    public void BombEffect()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Gribus").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Gribus")[i].GetComponent<GribusAI>().hp = 0;
        }
    }
    public void IcecreamEffect()
    {
        currentSpeed = 3.5f;
        _spawnRate += 0.5f;
        _spawnTimer = -3f;
    }
    public void ClearLevel()
    {
        if(clearLevel)
        {
            BombEffect();
            if (currentGribsAmount <= 0)
            {
                UIButtons.isPlaying = true;
                _spawnRate = 5f;
                defaultHp = 1;
                currentSpeed = 3.5f;
                GribusAI.score = 0;
                clearLevel = false;
            }
        }
    }
    private void SpawnGribus()
    {
        Vector3 _gribusSpawnLocation = new Vector3(Random.Range(-_spawnPointRangeX, _spawnPointRangeX), 0,
Random.Range(-_spawnPointRangeZ, _spawnPointRangeZ));
        _spawnAudio.pitch = Random.Range(0.9f, 1.1f);
        _spawnAudio.Play();
        defaultHp++;
        currentGribsAmount++;
        currentSpeed++;
        Instantiate(_gribusPrefab, _gribusSpawnLocation, Quaternion.identity);
    }
    private void GribusProcess()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnRate)
        {
            SpawnGribus();
            if (_spawnRate >= 0.1f)
            {
                if (_spawnRate >= 2f)
                    _spawnRate -= 0.1f;
                else
                    _spawnRate -= 0.05f;
            }
            _spawnTimer = 0;
        }
    }    
    private void SpawnBooster()
    {
        Vector3 _boosterSpawnLocation = new Vector3(Random.Range(-_spawnPointRangeX, _spawnPointRangeX), 0,
Random.Range(-_spawnPointRangeZ, _spawnPointRangeZ));
        int _boosterRandom = Random.Range(0, 2);
        if(_boosterRandom == 0)
            Instantiate(_bombPrefab, _boosterSpawnLocation, Quaternion.identity);
        if(_boosterRandom == 1)
            Instantiate(_freezePrefab, _boosterSpawnLocation, Quaternion.identity);
    }
    private void BoosterProcess()
    {
        _boosterTimer += Time.deltaTime;
        if(_boosterTimer >= _boosterRate)
        {
            SpawnBooster();
            _boosterTimer = 0f;
            _boosterRate = Random.Range(10f, 30f);
        }
    }
    private void Gameover()
    {
        if (currentGribsAmount >= 10)
        {
            UIButtons.isPlaying = false;
            if(GribusAI.score > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", GribusAI.score);
            }
        }
    }
}
