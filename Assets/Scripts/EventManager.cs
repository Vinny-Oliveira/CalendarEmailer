using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    [SerializeField]
    private InputField dateInputField;

    [SerializeField]
    private Button resetEventsButton;

    [SerializeField]
    private Button sendAllEmailsButtn;

    [SerializeField]
    private GameObject calendarEventPrefab;
    
    [SerializeField]
    private Transform contentView;

    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/calendar-dotnet-quickstart.json
    static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
    static string ApplicationName = "Google Calendar API .NET Quickstart";

    /// <summary>
    /// Run on the first frame
    /// </summary>
    private void Start() {
        DisplayTomorrowsDefaultDate();
    }

    /// <summary>
    /// Display tomorrow's date as the default of the date input field
    /// </summary>
    private void DisplayTomorrowsDefaultDate() {
        DateTime defaultDate = DateTime.Today.AddDays(1);
        int year = defaultDate.Year;
        string month = defaultDate.Month < 10 ? string.Concat("0", defaultDate.Month.ToString()) : defaultDate.Month.ToString();
        string day = defaultDate.Day < 10 ? string.Concat("0", defaultDate.Day.ToString()) : defaultDate.Day.ToString();


        dateInputField.text = $"{year}-{month}-{day}";
    }

    /// <summary>
    /// When the Reset Button is pressed, use the date that was input to desplay the list of events
    /// </summary>
    public void OnResetEventsButtonPressed() {
        try {
            // Eliminate the existing events
            foreach (var eventPanel in contentView.GetComponentsInChildren<CalendarEvent>()) {
                Destroy(eventPanel.gameObject);
            }
            
            // List of events
            Events events = GetEvents();

            if (events.Items != null && events.Items.Count > 0) {
                // Display each event
                foreach (var eventItem in events.Items) {
                    DisplayEvent(eventItem);
                }

            } else {
                throw new Exception("No upcoming events found.");
            }

        } catch (Exception ex) {
            Debug.Log("ERROR: " + ex.Message);
        }
    }

    /// <summary>
    /// Get all events of the date entered in the input field
    /// </summary>
    /// <returns></returns>
    private Events GetEvents() {
        UserCredential credential;

        using (var stream = new FileStream("Assets/StreamingAssets/credentials.json", FileMode.Open, FileAccess.Read)) {
            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        // Create Google Calendar API service.
        var service = new CalendarService(new BaseClientService.Initializer() {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        // List of events
        DateTime startDate = DateTime.Parse(dateInputField.text);
        Events events = CalendarEventsGetter.GetEvents(CalendarConstants.calendarName, service, startDate);
        return events;
    }

    /// <summary>
    /// Display an event on the event list
    /// </summary>
    /// <param name="events"></param>
    private void DisplayEvent(Google.Apis.Calendar.v3.Data.Event eventItem) {
        CalendarEvent calendarEvent = Instantiate(calendarEventPrefab, contentView).GetComponent<CalendarEvent>();
        calendarEvent.EventItem = eventItem;
        calendarEvent.DisplayEventDetails();
    }

}
