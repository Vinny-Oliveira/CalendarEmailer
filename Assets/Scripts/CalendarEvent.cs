using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarEvent : MonoBehaviour {

    public Event EventItem { get; set; }
    public string EventSummary { get; set; }
    public string EventStartTime { get; set; }

    private bool isEmailSent;

    [SerializeField]
    private Button btnSendEmail;

}
