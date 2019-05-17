using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PieGraph : MonoBehaviour {
	public float [] values;
	public Color [] wedgeColors;
	public Image  WedgePrefab;
    private string getUrlTotalClicks = "http://tadeolabhack.com:8081/test/Desmovilizaditos/tablapsicologo.php";
    public int total;
    private int[] escena = new int[12];
    public Text textoTotalClicks;
    public Text textoPosition;

    // Use this for initialization
    void Start () {
        
       StartCoroutine(datos());
        
        MakeGraph ();
	}
	void Update () {}
    public IEnumerator datos()
    {
        yield return new WaitForSeconds(5);

        //Cambiar por el archivo php correspondiente
        /*string clicks = getUrlTotalClicks+"?userID="+id;*/
        WWW totalClicks = new WWW(getUrlTotalClicks);
        yield return totalClicks;
        Debug.Log(totalClicks.text);

        if (!string.IsNullOrEmpty(totalClicks.text))
        {
            //trim le quita los espacios al comienzo y al final
            string[] numeros = totalClicks.text.Split( char.Parse("-"));
           
            for(int i = 0; i < numeros.Length; i++)
            {
               escena[i] = int.Parse (numeros[i]);
            }
            for (int i = 0; i < numeros.Length; i++)
            {
                total += int.Parse (numeros[i]);
            }

            textoTotalClicks.text = totalClicks.text.Trim();
        }


        
    }
    void MakeGraph  () {
		float total = 0f;
		float zRotation = 0f;
		for (int i = 0;i< values.Length;i++)
		{total += values [i];}

		for (int i = 0;i< values.Length;i++){ 
			Image newWedge = Instantiate (WedgePrefab) as Image;
			newWedge.transform.SetParent (transform,false);
			newWedge.color = wedgeColors[i];
			newWedge.fillAmount = values [i] / total;
			newWedge.transform.rotation = Quaternion.Euler (new Vector3(0f,0f,zRotation));
			zRotation -= newWedge.fillAmount * 360f;
		}
			
	}


	// Update is called once per frame

}
