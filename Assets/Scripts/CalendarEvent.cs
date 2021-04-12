using System;
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

    /// <summary>
    /// Display Summary and Start Time of the event on the panel
    /// </summary>
    public void DisplayEventDetails() {
        eventSummaryText.text = EventItem.Summary;
        eventStartTimeText.text = GetEventTime();
    }

    /// <summary>
    /// Get the start time of the event as a string
    /// </summary>
    /// <returns>The start time fo the event as a string</returns>
    public string GetEventTime() {
        DateTime eventDate = (DateTime)EventItem.Start.DateTime;
        int hour = eventDate.Hour;
        string minute = eventDate.Minute < 10 ? string.Concat("0", eventDate.Minute) : eventDate.Minute.ToString();

        string ampm = (hour > 11) ? "pm" : "am";

        if (hour > 12) {
            hour -= 12;
        }

        return $"{hour}:{minute} {ampm}";
    }

}
