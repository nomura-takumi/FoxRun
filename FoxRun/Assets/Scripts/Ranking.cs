using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;
using System;

public class Ranking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = "";

        NCMBQuery<NCMBObject> query = new("HighScore");

        query.OrderByDescending("Score");

        query.Limit = 5;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null) {

            }
            else {
                int rank = 1;

                foreach (NCMBObject obj in objList) {
                    text.text += rank++.ToString() + ":" + string.Format("{0:D4}", obj["Score"]) + Environment.NewLine;
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
