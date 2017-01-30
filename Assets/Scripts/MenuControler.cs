using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public Button button;

    // Use this for initialization
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonStartPressed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnButtonStartPressed()
    {
        SceneManager.LoadScene("Scene");
    }
}
