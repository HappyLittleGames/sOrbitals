using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class QWERTYanim : MonoBehaviour {


    [SerializeField]private List<Button> m_buttons = null;

	void Update ()
    {
        var pointer = new PointerEventData(EventSystem.current);
        if (m_buttons != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ExecuteEvents.Execute(m_buttons[0].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                ExecuteEvents.Execute(m_buttons[0].gameObject, pointer, ExecuteEvents.pointerDownHandler);                
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                ExecuteEvents.Execute(m_buttons[1].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                ExecuteEvents.Execute(m_buttons[1].gameObject, pointer, ExecuteEvents.pointerDownHandler);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ExecuteEvents.Execute(m_buttons[2].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                ExecuteEvents.Execute(m_buttons[2].gameObject, pointer, ExecuteEvents.pointerDownHandler);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ExecuteEvents.Execute(m_buttons[3].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                ExecuteEvents.Execute(m_buttons[3].gameObject, pointer, ExecuteEvents.pointerDownHandler);
            }
            else
            {
                foreach (Button button in m_buttons)
                {
                    ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerUpHandler);                    
                    ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
                }
            }
        }
	}

    public void LoadForm()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("3");
    }
}
