using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;

public class TestPanelLogic : MonoBehaviour
{
    [Header("玩家数值")]
    public TextMeshProUGUI healthTMP;
    public TextMeshProUGUI spiritTMP;
    public TextMeshProUGUI moneyTMP;
    public TextMeshProUGUI reputationTMP;
    public Button nextRoundBtn;
    public GameObject showPanelObj;
    public TextMeshProUGUI infoTMP;
    public Button interactButton1;
    public Button interactButton2;
    public Button interactButton3;

    public GameObject maskObj;

    private RoundCard nowCard;

    private async void Awake() 
    {
        maskObj.gameObject.SetActive(true);
        await UniTask.WaitUntil(()=>GameManager.Instance.init);
        nextRoundBtn.gameObject.SetActive(true);
        showPanelObj.SetActive(false);

        nextRoundBtn.onClick.AddListener(() =>
        {
           GameManager.Instance.StartNewTurn(); 
           showPanelObj.gameObject.SetActive(true);
           nextRoundBtn.gameObject.SetActive(false); 
        });

        interactButton1.onClick.AddListener(() =>
        {
            if (nowCard.IsActionCard)
            {
                GameManager.Instance.SelectActionCardOption(nowCard.instanceId,ActionCardOption.Work);
            }
            else if (nowCard.IsEventCard)
            {
                GameManager.Instance.SelectEventCardOption(nowCard.instanceId,EventCardOption.Accept);
            }
        });
        interactButton2.onClick.AddListener(() =>
        {
            if (nowCard.IsActionCard)
            {
                GameManager.Instance.SelectActionCardOption(nowCard.instanceId,ActionCardOption.Rest);
            }
            else if (nowCard.IsEventCard)
            {
                GameManager.Instance.SelectEventCardOption(nowCard.instanceId,EventCardOption.Refuse);
            }
        });
        interactButton3.onClick.AddListener(() =>
        {
            if (nowCard.IsActionCard)
            {
                GameManager.Instance.SelectActionCardOption(nowCard.instanceId,ActionCardOption.Prepare);
            }
            else if (nowCard.IsEventCard)
            {
                GameManager.Instance.SelectEventCardOption(nowCard.instanceId,EventCardOption.Delay);
            }
        });

        EventCenter.Instance.AddEventListener(E_EventType.E_RoundFinished, () =>
        {
           showPanelObj.gameObject.SetActive(false);
           nextRoundBtn.gameObject.SetActive(true); 
        });
        EventCenter.Instance.AddEventListener<RoundCard>(E_EventType.E_ShowNextCard,(card) =>
        {
            nowCard = card;
            if (nowCard.IsActionCard)
            {
                var config1 = ActionCardManager.Instance.GetCard(nowCard.actionCard.restConfigId);
                var config2 = ActionCardManager.Instance.GetCard(nowCard.actionCard.workConfigId);
                if(config1 == null)Debug.Log("1");
                if(config2 == null)Debug.Log("2");
                infoTMP.text = config1.mCommon + "/" + config2.mCommon;
            }
            else if (nowCard.IsEventCard)
            {
                var config = EventCardManager.Instance.GetCard(nowCard.eventCard.configId);
                infoTMP.text = config.mCommon;
            }
        });

        maskObj.gameObject.SetActive(false);
    }

    private void Update()
    {
        var player = GameManager.Instance.playerInfo;
        healthTMP.text = $"体力: {player.GetValue(NumericType.Health)}";
        spiritTMP.text = $"精神: {player.GetValue(NumericType.Spirit)}";
        moneyTMP.text = $"金钱: {player.GetValue(NumericType.Money)}";
        reputationTMP.text = $"名声: {player.GetValue(NumericType.Reputation)}";
    }
}
