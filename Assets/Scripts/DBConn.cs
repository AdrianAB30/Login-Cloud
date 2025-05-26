using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DBConn : MonoBehaviour
{
    [SerializeField] private string url = "http://localhost/prueba.php";
    //[SerializeField] private Data userdata;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;

    public void SendUserData()
    {
        Data userdata = new Data
        {
            username = usernameInput.text,
            email = emailInput.text,
            password = passwordInput.text
        };

        StartCoroutine(SendRequest(userdata));
    }

    private IEnumerator SendRequest(Data userdata)
    {
        string jsonData = JsonUtility.ToJson(userdata);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
        }
    }
}
// testeo mandar d unity a la base datos
[System.Serializable]
public class Data
{
    public string username;
    public string email;
    public string password;
}