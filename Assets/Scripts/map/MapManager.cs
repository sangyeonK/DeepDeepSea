using UnityEngine;

public enum GAMESTAGETYPE
{
    GameStart,
    GamePlay,
    GameEnd
}
public class MapManager : MonoBehaviour
{

    public GameObject[] Maptype;
    private GameObject screenObject;
    private GameObject player;

    public bool gameover;
    private float waitTime;
    private int creatDistance;

    public int rand;
    private float time = 0;
    public int count = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        gameover = false;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 2)
        {
            count++;
            creatDistance = -28;
            int random = Random.Range(0, Maptype.Length);

            Debug.Log("MapType : " + random);

            GameObject maptype = Instantiate(Maptype[random], new Vector3(0, count * creatDistance), Quaternion.identity);

            time = 0;
        }
    }
}
