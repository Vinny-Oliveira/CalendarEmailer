using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarEvent : MonoBehaviour {

    public Google.Apis.Calendar.v3.Data.Event EventItem { get; set; }

    private bool isEmailSent;
    
    [SerializeField]
    private Text eventSummaryText;
    
    [SerializeField]
    private Text eventStartTimeText;

    [SerializeField]
    private Button btnSendEmail;

    [SerializeField]
    private Text textButton;

    public void DisplayEventDetails() {
        eventSummaryText.text = EventItem.Summary;

        // Build start time
        string eventDateTime = EventItem.Start.DateTime.ToString();
        int.TryParse(eventDateTime.Substring(10, 2), out int hour);
        string minute = eventDateTime.Substring(13, 2);

        eventStartTimeText.text = hour + ":" + minute;
    }

}
