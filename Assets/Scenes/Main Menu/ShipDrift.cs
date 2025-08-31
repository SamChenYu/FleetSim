using UnityEngine;

public class ShipDrift: MonoBehaviour
{
    public GameObject ship;

    private float baseX;
    private float baseY;
    private float baseZ;

    public void Start()
    {
        baseX = ship.transform.localEulerAngles.x;
        baseY = ship.transform.localEulerAngles.y;
        baseZ = ship.transform.localEulerAngles.z;
    }

void Update()
{
    float t = Time.time;

    float rollOffset = Mathf.Sin(t) * 0.01f * Mathf.Rad2Deg;  // Z axis
    float pitchOffset = Mathf.Sin(t) * 0.01f * Mathf.Rad2Deg; // X axis


    transform.localEulerAngles = new Vector3(
        baseX + pitchOffset,
        baseY,
        baseZ + rollOffset
    );
}


}