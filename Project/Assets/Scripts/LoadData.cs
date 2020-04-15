using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SimpleJSON;

public class LoadData : MonoBehaviour
{

    public string[] dataFileName;
    public Text Titulo;
    public float space;
    public float nextSpace = 25;
    public GameObject TextoPrefab;
    public GameObject TextoPrefabDato;
    public GameObject panelResultado;

    JSONNode Result;
    JSONArray arr;

    GameObject[] gameObjects;
    GameObject txtPrefab;
    GameObject txtPrefabdato;

    public void GetData()
    {

        DestroyAllObjects();

        int num = Random.Range(0, dataFileName.Length);
        string filePath = Path.Combine(Application.streamingAssetsPath, dataFileName[num]);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Result = JSON.Parse(dataAsJson);
        }

        if (Result["ColumnHeaders"].Count > 0)
        {
            Titulo.text = Result["Title"].Value;

            arr = Result["ColumnHeaders"].AsArray;

           
            //add header 
            for (int i = 0; i < arr.Count; i++)
            {
                //Debug.Log(arr[i]);
                txtPrefab = Instantiate(TextoPrefab, panelResultado.transform);
                txtPrefab.name = arr[i];
                txtPrefab.GetComponent<RectTransform>().SetParent(panelResultado.transform);
                txtPrefab.GetComponent<Text>().text = arr[i];
                
            }
        }

        if (Result["Data"].Count > 0)
        {


            foreach (Transform child in panelResultado.transform)
            {

                space = nextSpace;
                string key = child.name;

                for (int i = 0; i < Result["Data"].Count; i++)
                {
                    txtPrefabdato = Instantiate(TextoPrefabDato, child.transform);
                    txtPrefabdato.transform.position = new Vector2(txtPrefabdato.transform.position.x, txtPrefabdato.transform.position.y - space);
                    txtPrefabdato.GetComponent<Text>().text = Result["Data"][i][key].Value;
                    space += nextSpace;
                }

            }


        }


    }


    void DestroyAllObjects()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("TextoDato");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
