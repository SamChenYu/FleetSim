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

        for (int i = 0; i < starCount; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.transform.position = new Vector3(
                RandomOutsideRange(-200f, 200f, -20f, 20f),
                RandomOutsideRange(-100f, 100f, -20f, 20f),
                RandomOutsideRange(-200f, 200f, -20f, 20f)
            );
            stars[i] = star;

            starDrift[i] = new Vector3(Random.Range(-driftSpeed, driftSpeed), Random.Range(-driftSpeed, driftSpeed), Random.Range(-driftSpeed, driftSpeed));
        }
    }



    float RandomOutsideRange(float min, float max, float excludeMin, float excludeMax)
    {
        // Random.value 0 = lower range, 1 = upper range
        if (Random.value < 0.5f)
        {
            return Random.Range(min, excludeMin);   // e.g. -200 to -50
        }
        else
        {
            return Random.Range(excludeMax, max);   // e.g. 50 to 100
        }
    }

}
