using HelicopterPhysics.Inputs;
using TMPro;
using UnityEngine;


public class GaguePanel : MonoBehaviour
{
    [Header("UI Properties")]
    public TextMeshProUGUI RawThrottle;
    public TextMeshProUGUI StickyThrottle;
    public TextMeshProUGUI Collective;
    public TextMeshProUGUI StickyCollective;
    public TextMeshProUGUI Cyclic;
    public TextMeshProUGUI Pedal;


    [Header("Other References")]
    public KeyboardHeliInput Input;

    private void Update()
    {
        RawThrottle.text = Input.ThrottleInput.ToString();
        StickyThrottle.text = Input.StickyThrottle.ToString();
        Collective.text = Input.CollectiveInput.ToString();
        StickyCollective.text = Input.StickyCollective.ToString();
        Cyclic.text = "(" + Input.CyclicInput.x + "," + Input.CyclicInput.y + ")";
        Pedal.text = Input.PedalInput.ToString();
    }

}
