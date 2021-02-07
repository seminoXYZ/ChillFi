using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMenu : MonoBehaviour
{
    CanvasGroup m_CanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleWindow();
        }
    }
    void ToggleWindow()
    {
       if(m_CanvasGroup.alpha == 0)
        {
            m_CanvasGroup.alpha = 1;
            m_CanvasGroup.blocksRaycasts = true;
            m_CanvasGroup.interactable = true;
        }
        else
        {
            m_CanvasGroup.alpha = 0;
            m_CanvasGroup.blocksRaycasts = false;
            m_CanvasGroup.interactable = false;
        }
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
