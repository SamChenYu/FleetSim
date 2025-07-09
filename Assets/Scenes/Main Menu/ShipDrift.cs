using UnityEngine;

public class ShipDrift: MonoBehaviour
{
    public GameObject ship;

    private const float baseX = 90;
    private const float baseY = 40f;
    private const float baseZ = 0f;

    public void Start()
    {

    }

void Update()
{
    float t = Time.time;

    float rollOffset = Mathf.Sin(t) * 0.02f * Mathf.Rad2Deg;  // Z axis
    float pitchOffset = Mathf.Sin(t) * 0.02f * Mathf.Rad2Deg; // X axis


    transform.localEulerAngles = new Vector3(
        baseX + pitchOffset,
        baseY,
        baseZ + rollOffset
    );
}


}