using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    [SerializeField]
    GameObject prefabPopup;
    [SerializeField] private Canvas _ui;

    public void TryBuyItem()
    {
        GameObject newPopup = Instantiate(prefabPopup,_ui.transform);
        SomePopup popupScript = newPopup.GetComponent<SomePopup>();
        popupScript.OnClose += CompletePurhase;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;
        popupScript.ActivatePopup(ct);
    }

    void CompletePurhase(bool completed)
    {
        if (completed)
        {
            Debug.Log("������� ���������!");
        }
        else
        {
            Debug.Log("������� ��������!");
        }
    }
}
