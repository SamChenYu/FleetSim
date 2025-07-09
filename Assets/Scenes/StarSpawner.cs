using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    private int starCount = 100;

    public GameObject starPrefab;
    public GameObject[] stars;
    public Vector3[] starDrift;

    private float driftSpeed = 0.005f;

    void Start()
    {
        stars = new GameObject[starCount];
        starDrift = new Vector3[starCount];

        for(int i=0; i<starCount; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.transform.position = new Vector3(Random.Range(-150, 150), Random.Range(-100, 100), Random.Range(-150, 150));
            stars[i] = star;

            starDrift[i] = new Vector3(Random.Range(-driftSpeed, driftSpeed), Random.Range(-driftSpeed, driftSpeed), Random.Range(-driftSpeed, driftSpeed));
        }
    }
    void Update()
    {        
        // Every star drifts a little bit
        for(int i=0; i<starCount; i++)
        {
            stars[i].transform.position += starDrift[i];
        }

    }
}
