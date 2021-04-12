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
    private InputField dateField;

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

    // Email info
    readonly string email = "extraedinc@gmail.com";
    readonly string sender = "vini@extraed.ca";
    readonly string password = "Vini349?$vjeL";

    public void OnResetEventsButtonPressed() {
        try {
            // List of events
            Events events = GetEvents();

            if (events.Items != null && events.Items.Count > 0) {
                DisplayEvent(events);

            //    EmailSender emailSender = new EmailSender();
            //    List<MailMessage> messages = new List<MailMessage>();

            //    foreach (var eventItem in events.Items) {
            //        messages.Add(emailSender.ComposeEmail(eventItem, sender));
            //    }

            //    Console.WriteLine("\nAre you sure you want to send these emails? Press 'y' to send all or any other key to abort");
            //    string input = Console.ReadLine();

            //    if (input.Equals("Y", StringComparison.OrdinalIgnoreCase)) {
            //        foreach (var message in messages) {
            //            emailSender.SendEmail(message, sender, password);
            //        }
            //        Console.WriteLine("Emails sent successfully!");
            //    } else {
            //        Console.WriteLine("Operation aborted.");
            //    }

            //} else {
            //    Console.WriteLine("No upcoming events found.");
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
            Console.WriteLine("Credential file saved to: " + credPath);
        }

        // Create Google Calendar API service.
        var service = new CalendarService(new BaseClientService.Initializer() {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        // Define parameters of request.
        CalendarEventsGetter calendarEventsGetter = new CalendarEventsGetter();
        DateTime startDate = DateTime.Parse(dateField.text);

        // List of events
        Events events = calendarEventsGetter.GetEvents(email, service, startDate);
        return events;
    }


    private void DisplayEvent(Events events) {
        CalendarEvent calendarEvent = Instantiate(calendarEventPrefab, contentView).GetComponent<CalendarEvent>();
        calendarEvent.EventItem = events.Items[0];
        calendarEvent.DisplayEventDetails();
    }

}
