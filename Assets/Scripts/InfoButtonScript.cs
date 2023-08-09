using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InfoButtonScript : MonoBehaviour
{
    public static InfoButtonScript instance;

    [SerializeField] GameObject nameText;
    [SerializeField] GameObject pointsText;
    [SerializeField] GameObject addressText;
    [SerializeField] GameObject popUpWindow;
    [SerializeField] GameObject ExitButton;



    string name;
    string points;
    string label;
    string id;
    bool isManager;
    string address;

    private void Awake()
    {
        nameText = GameObject.Find("Name");
        pointsText = GameObject.Find("PointsPopUp");
        addressText = GameObject.Find("Address");
        popUpWindow = GameObject.Find("PopUp");
        ExitButton = GameObject.Find("Exit");
        this.gameObject.GetComponent<Button>().onClick.AddListener(onClicking);
        ExitButton.gameObject.GetComponent <Button>().onClick.AddListener(onExit);
        
    }


    private void Start()
    {
        
        instance = this;

    }


    public void SetData(string name, string points, string label, string id, bool isManager,string address)
    {
        this.name = name;
        this.points = points;
        this.label = label;
        this.id = id;
        this.isManager = isManager;
        this.address = address;


        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = label;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = points;
    }

    public void onClicking()
    {
        popUpWindow.transform.DOScale(new Vector3(1,1,1),0.3f);
        nameText.GetComponent<TMP_Text>().text = name;
        pointsText.GetComponent<TMP_Text>().text = points;
        addressText.GetComponent<TMP_Text>().text = address;
    }

    public void onExit()
    {

        popUpWindow.transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }



}
