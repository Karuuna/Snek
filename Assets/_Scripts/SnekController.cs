using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SnekController : MonoBehaviour
{
    public int Score;
    public Text ScoreText;
    public bool GameOver;


    float timeSinceLastMove = 0f;
    public float TimeToMove = 0.15f;

    Vector2 dir = Vector2.right;
    public Transform SnekPosition;
    List<Transform> tail = new List<Transform>();

    bool eating = false;

    public GameObject tailPrefab;


    // Start is called before the first frame update
    void Update()
    {
        if (GameOver == false)
        {
            InputDirection();
            MovementTimeCheck();
        }
        else if (Input.anyKeyDown)
        {
            Score = 0;
            tail.RemoveRange(0,tail.Count);
            SnekPosition.position = Vector2.zero;
        }

    }

    // Update is called once per frame
    void MovementTimeCheck()
    {
        if (timeSinceLastMove < TimeToMove)
            timeSinceLastMove += Time.deltaTime;
        else
        {
            timeSinceLastMove = 0;
            Move();
        }
    }

    void InputDirection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (dir != Vector2.left)
                dir = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (dir != Vector2.up)
                dir = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (dir != Vector2.right)
                dir = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (dir != Vector2.down)
                dir = Vector2.up;
        }

    }

    void Move()
    {
        Vector2 gapPosition = transform.position;
        transform.Translate(dir);

        if (eating)
        {
            GameObject newTailPiece = (GameObject)Instantiate(tailPrefab, gapPosition, Quaternion.identity);
            tail.Insert(0, newTailPiece.transform);
            eating = false;
        }
        else if (tail.Count > 0)
        {
            tail.Last().position = gapPosition;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "food")
        {
            eating = true;
            Destroy(coll.gameObject);
            Score += 1;
            ScoreText.text = "Score: " + Score;
        }
        else
        {
            GameOver = true;    //doesnt currently work
        }
    }

}
