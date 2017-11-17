using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoToDB : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //StartCoroutine(SendScore());
	}

    public void ActivatePHP(string targetName, double targetX, double targetY, double targetZ)
    {
        StartCoroutine(SendTargetPos(targetName, targetX, targetY, targetZ));
    }

    public IEnumerator SendScore()
    {
        WWW request = new WWW("http://23973.hosts.ma-cloud.nl/bewijzenmap/Year%202/p2.1/proj/FPS/Database/FPS_DB.php?playerName=" + this.name + "&score=420&tableName=ScoreLog");
        yield return request;
        Debug.Log("Request returned");
        StartCoroutine(WaitTime());
    }

    public IEnumerator SendTargetPos(string targetName, double targetX, double targetY, double targetZ)
    {
        WWW request = new WWW("http://23973.hosts.ma-cloud.nl/bewijzenmap/Year%202/p2.1/proj/FPS/Database/FPS_DB.php?targetName=" + targetName + "&targetX=" + targetX + "&targetY=" + targetY + "&targetZ=" + targetZ + "&tableName=TargetPosLog");
        yield return request;
        Debug.Log("Request returned");
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(SendScore());
    }
}
