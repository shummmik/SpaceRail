using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StateMachine.Instance.ChangeState(new PauseState()));
    }
}
