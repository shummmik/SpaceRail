using UnityEngine.UI;
using UnityEngine;

public class TransformButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StateMachine.Instance.ChangeState(new TransformState()));
    }

}
