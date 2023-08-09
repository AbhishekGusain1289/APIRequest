using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;
public class ApiHandler : MonoBehaviour
{
    public static ApiHandler instance;


    private readonly string uri = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    [SerializeField] GameObject NameButton;
    [SerializeField] GameObject List;
    int Filter=0;
    UnityWebRequest request;
    JSONNode ClientsInfo;


    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Points;
    [SerializeField] TMP_Text Address;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(GettingJson());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleDropDown(int val)
    {
        Filter = val;
        switch (val)
        {
            case 0:
                Debug.Log("All Clients");
                break;
            case 1:
                Debug.Log("Managers Only");
                break;
            case 2:
                Debug.Log("Non Managers");
                break;
        }


    }

    public void onButtonPress()
    {
        StartCoroutine(GetClientsData());
    }

    private IEnumerator GetClientsData()
    {
        DeleteAllChildren();
        request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        ClientsInfo = JSON.Parse(request.downloadHandler.text);

        if (Filter==0)
        {
            AllClients();
            Debug.Log(Filter);
        }
        if(Filter==1)
        {
            ManagersOnly();
            Debug.Log(Filter);
        }
        if(Filter==2)
        {
            NonManager();
            Debug.Log(Filter);
        }
    }

    private void NonManager()
    {
        for (int i = 0, j = ClientsInfo["clients"].Count - 1; i < ClientsInfo["clients"].Count; i++, j--)
        {
            if (ClientsInfo["clients"][i]["isManager"]==false)
            {
                AddingClients(i);
            }
        }
    }

    private void ManagersOnly()
    {
        for(int i=0,j=ClientsInfo["clients"].Count-1;i<ClientsInfo["clients"].Count;i++,j--)
        {
            if(ClientsInfo["clients"][i]["isManager"]==true)
            {
                AddingClients(i);
            }
        }
    }


    void AllClients()
    {
        for (int i = 0; i < ClientsInfo["clients"].Count; i++)
        {
            AddingClients(i);
        }
    }

    void DeleteAllChildren()
    {
        foreach(Transform child in List.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void AddingClients(int i)
    {
        GameObject NameButtonObject = Instantiate(NameButton, List.transform);
        string label = ClientsInfo["clients"][i]["label"];
        string id = (i+1).ToString();
        bool isManager = ClientsInfo["clients"][i]["isManager"];
        string name;
        string address;
        string points;

        if(ClientsInfo["data"][(i + 1).ToString()] != null)
        {
            name=ClientsInfo["data"][(i + 1).ToString()]["name"];
            address=ClientsInfo["data"][(i + 1).ToString()]["address"];
            points = ClientsInfo["data"][(i + 1).ToString()]["points"];
        }
        else
        {
            name = address = points = "N/A";
        }
        NameButtonObject.GetComponent<InfoButtonScript>().SetData(name,points,label, id, isManager, address);
        //GameObject Label = NameButtonObject.transform.GetChild(0).gameObject;
        //Label.GetComponent<TMP_Text>().text = ClientsInfo["clients"][i]["label"];
        //if (ClientsInfo["data"][(i + 1).ToString()] != null)
        //{
        //    GameObject Points = NameButtonObject.transform.GetChild(1).gameObject;
        //    Points.GetComponent<TMP_Text>().text = ClientsInfo["data"][(i + 1).ToString()]["points"];
        //}
    }


    private IEnumerator GettingJson()
    {
        request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        ClientsInfo = JSON.Parse(request.downloadHandler.text);
    }
}
