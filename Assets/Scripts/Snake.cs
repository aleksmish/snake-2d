using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private const int WINSTATE = 1127;

    [SerializeField]
    private GameHandler gameHandler;

    Vector2 _direction = Vector2.right;
    [SerializeField]
    private Transform prefabBody;

    [SerializeField]
    private int initialSize;

    // Singleton pattern
    private static Snake instance;

    public static Snake Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public List<Transform> _bodiesOfSnake=new List<Transform>();

    void Start()
    {
        _bodiesOfSnake.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            _bodiesOfSnake.Add(Instantiate(prefabBody));
        }

        transform.position = Vector3.zero;
    }

    void Update()
    {
        // Check for win state

        if(_bodiesOfSnake.Count== WINSTATE)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(2);
        }


        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _direction!=Vector2.down)
        {
            _direction = Vector2.up;
        }
        if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _direction != Vector2.up)
        {
            _direction = Vector2.down;

        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {

        for (int i = _bodiesOfSnake.Count - 1; i > 0; i--)
        {
            _bodiesOfSnake[i].position = _bodiesOfSnake[i - 1].position;
        }

        transform.position += new Vector3(_direction.x, _direction.y, 0.0f);
    }
    
    private void Grow()
    {
        Transform transformOfBody=Instantiate(prefabBody);
        transformOfBody.position = _bodiesOfSnake[_bodiesOfSnake.Count - 1].position;

        _bodiesOfSnake.Add(transformOfBody);
    }

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            Score.score++;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameHandler.GameOver();
        }
    }


}
