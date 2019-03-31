using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnFood : MonoBehaviour
{
    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 4);
    }

    void Spawn()
    {
        int x = (int)Random.Range(borderLeft.position.x +1, borderRight.position.x -1);
        int y = (int)Random.Range(borderBottom.position.y +1, borderTop.position.y -1);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

}
