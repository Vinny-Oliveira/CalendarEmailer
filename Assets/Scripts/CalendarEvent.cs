using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarEvent : MonoBehaviour {

    public Google.Apis.Calendar.v3.Data.Event EventItem { get; set; }
    public string EventSummary {
        get => EventItem.Summary;
    }
    public string EventStartTime {
        get => EventItem.Start.DateTime.ToString();
    }

    private bool isEmailSent;

    [SerializeField]
    private Button btnSendEmail;
    
    [SerializeField]
    private Text eventSummaryText;
    
    [SerializeField]
    private Text eventStartTimeText;

}
