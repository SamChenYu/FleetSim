using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Scriptable Objects/ShipData")]
public class ShipData : ScriptableObject
{

    // Basic data
    public string name;
    public string type;
    public float baseSpeed;
    public float health;
    public GameObject shipPrefab; // Reference to the ship's prefab
    public Sprite shipIcon; // Reference to the ship's icon for UI

    // Powered utilities
    public int power; // Split between engine, shields, weapons

    public int allocatedEnginePower; // 1.5x speed for each point allocated
    public int allocatedShieldPower; // 1.0 shield for each point allocated
    public int allocatedWeaponPower; // 1.5x faster fire rate for each point allocated

    public float shield;
    public float shieldRegenRate;
    public float shieldRegenDelay;
    public float damage;
    public float fireRate;
    public float fireRateTimer;
    public float range;

    public void Initialize(string shipType, string shipName)
    {
        // Hardcoded ship data for now
        switch (shipType)
        {
            case "Fighter":
                name = shipName;
                type = "Fighter";
                baseSpeed = 10f;
                health = 100f;
                power = 3;
                allocatedEnginePower = 2;
                allocatedShieldPower = 1;
                allocatedWeaponPower = 0;
                shield = 50f;
                shieldRegenRate = 5f;
                shieldRegenDelay = 2f;
                damage = 10f;
                fireRate = 1f;
                range = 15f;
                break;

            case "Cruiser":
                name = shipName;
                type = "Cruiser";
                baseSpeed = 8f;
                health = 200f;
                power = 4;
                allocatedEnginePower = 1;
                allocatedShieldPower = 2;
                allocatedWeaponPower = 1;
                shield = 100f;
                shieldRegenRate = 3f;
                shieldRegenDelay = 3f;
                damage = 20f;
                fireRate = 0.8f;
                range = 20f;
                break;

            case "Battleship":
                name = shipName;
                type = "Battleship";
                baseSpeed = 5f;
                health = 300f;
                power = 5;
                allocatedEnginePower = 0;
                allocatedShieldPower = 3;
                allocatedWeaponPower = 2;
                shield = 150f;
                shieldRegenRate = 2f;
                shieldRegenDelay = 4f;
                damage = 30f;
                fireRate = 0.5f;
                range = 25f;
                break;

            default:
                Debug.LogError("<ShipData> Unknown ship type: " + shipType);
                break;
        }
    }
}
