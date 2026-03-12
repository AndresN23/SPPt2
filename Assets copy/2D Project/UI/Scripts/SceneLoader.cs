using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadGame(){

        StartCoroutine(_LoadGame());



        IEnumerator _LoadGame(){

        AsyncOperation loadOperation =  SceneManager.LoadSceneAsync("0");
        while (!loadOperation!.isDone) yield return null;
        //Debug.Log("Bruh");

        //WAit till scence is loaded and then find the player 
        GameObject playerObj = GameObject.Find("Player");
         Debug.Log(playerObj.name);
        }
    }
}
