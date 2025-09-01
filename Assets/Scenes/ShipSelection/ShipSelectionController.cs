using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ShipSelectionController : MonoBehaviour
{

    public int terminusCount;
    public int eternalCount;
    public int arquitensCount;
    public int harrowerCount;



    public GameObject terminusLabel;
    public GameObject eternalLabel;
    public GameObject arquitensLabel;
    public GameObject harrowerLabel;


    public void GoToMenu()
    {   
        PlayerPrefs.SetInt("TerminusCount", terminusCount);
        PlayerPrefs.SetInt("EternalCount", eternalCount);
        PlayerPrefs.SetInt("ArquitensCount", arquitensCount);
        PlayerPrefs.SetInt("HarrowerCount", harrowerCount);
        SceneManager.LoadScene("Main Menu");
    }

    void Start()
    {
        terminusCount = 2;
        eternalCount = 1;
        arquitensCount = 1;
        harrowerCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLabels() {

        TextMeshProUGUI terminusTMP = terminusLabel.GetComponent<TextMeshProUGUI>();
        terminusTMP.text = terminusCount.ToString();
        TextMeshProUGUI eternalTMP = eternalLabel.GetComponent<TextMeshProUGUI>();
        eternalTMP.text = eternalCount.ToString();
        TextMeshProUGUI arquitensTMP = arquitensLabel.GetComponent<TextMeshProUGUI>();
        arquitensTMP.text = arquitensCount.ToString();
        TextMeshProUGUI harrowerTMP = harrowerLabel.GetComponent<TextMeshProUGUI>();
        harrowerTMP.text = harrowerCount.ToString();
    }

    public void AddTerminus()
    {
        terminusCount++;
        UpdateLabels();
    }

    public void MinusTerminus() 
    {
        if(terminusCount > 0) terminusCount--;
        UpdateLabels();
    }

    public void AddEternal() 
    {
        eternalCount++;
        UpdateLabels();
    }

    public void MinusEternal()
    {
        if(eternalCount > 0) eternalCount--;
        UpdateLabels();
    }

    public void AddArquitens()
    {
        arquitensCount++;
        UpdateLabels();
    }

    public void MinusArquitens()
    {
        if(arquitensCount > 0) arquitensCount--;
        UpdateLabels();
    }

    public void AddHarrower() 
    {
        harrowerCount++;
        UpdateLabels();
    }

    public void MinusHarrower()
    {
        if(harrowerCount > 0) harrowerCount--;
        UpdateLabels();
    }
}
