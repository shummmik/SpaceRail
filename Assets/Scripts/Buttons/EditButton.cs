using UnityEngine;
using UnityEngine.UI;

public class EditButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StateMachine.Instance.ChangeState(new EditState()));
    }
}
