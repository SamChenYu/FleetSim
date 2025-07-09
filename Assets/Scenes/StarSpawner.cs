using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    private int starCount = 300;

    public GameObject starPrefab;
    public GameObject[] stars;


    void Start()
    {
        stars = new GameObject[starCount];
        for(int i=0; i<starCount; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.transform.position = new Vector3(Random.Range(-300, 300), Random.Range(0, 300), Random.Range(-300, 300));
            stars[i] = star;
        }
    }
    void Update()
    {
        
    }
}
