using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SnakeController : MonoBehaviour {
    
    public AudioClip triggerSoundApple;
    public AudioClip triggerSoundSusu;
    AudioSource audioSource;
    AudioSource audioSource1;

    // Settings
    public float MoveSpeed = 40;
    public float SteerSpeed = 180;
    public float BodySpeed = 8;
    public int Gap = 13;

    public int Score = 0;
    public Text scoreText;

    // References
    public GameObject BodyPrefab;
    public GameObject ApplePrefab;
    public GameObject MilkPrefab;
    public GameObject SwordPrefab;


    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource1 = GetComponent<AudioSource>();
        Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
        Instantiate(ApplePrefab, RandomSpawn, Quaternion.identity);
        GrowSnake();
        GrowSnake();

    }

    // Update is called once per frame
    void Update() {

        // Move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);
            index++;

        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            PlayerManager.isGameOver = true;
            gameObject.SetActive(false);
            Debug.Log("Hit The Wall?");
        }
        else if (other.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(triggerSoundApple, 0.7F);
            GrowSnake();
            Destroy(other.gameObject);
            Score++;

            if (Score % 5 == 0)
            {
                Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                Instantiate(MilkPrefab, RandomSpawn, Quaternion.identity);
                Vector3 RandomSpawn2 = new Vector3(Random.Range(-5, 5), 4, Random.Range(-5, 5));
                Instantiate(ApplePrefab, RandomSpawn2, Quaternion.identity);

            }
            else if (Score % 12 == 0)
            {
                Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                Instantiate(SwordPrefab, RandomSpawn, Quaternion.identity);
            }
            else
            {
                Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                Instantiate(ApplePrefab, RandomSpawn, Quaternion.identity);
                
            }
            scoreText.text = "Score : " + Score;
        }
        else if (other.gameObject.tag == "Milk")
        {
            audioSource.PlayOneShot(triggerSoundSusu, 0.7F);
            GrowSnake();
            GrowSnake();
            GrowSnake();
            Destroy(other.gameObject);
            Score = Score + 3;
            if (Score % 5 == 0)
            {
                Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                Instantiate(MilkPrefab, RandomSpawn, Quaternion.identity);
                
            }
            else if (Score % 12 == 0)
            {
                Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                Instantiate(SwordPrefab, RandomSpawn, Quaternion.identity);
                
            }
            else
            {
                Vector3 RandomSpawn = new Vector3(Random.Range(-10, 10), 4, Random.Range(-10, 10));
                Instantiate(ApplePrefab, RandomSpawn, Quaternion.identity);
                
            }
            scoreText.text = "Score : " + Score;
        }
        else if (other.gameObject.tag == "Sword")
        {
            DestroySnake();
            DestroySnake();
            DestroySnake();
            DestroySnake();
            DestroySnake();
            Score = Score - 5;
            Destroy(other.gameObject);
            scoreText.text = "Score : " + Score;

        }
    }

    void GrowSnake() {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    void DestroySnake()
    {
        GameObject delbody = BodyParts[0];
        BodyParts.Remove(delbody);
        Destroy(delbody);
    }
}