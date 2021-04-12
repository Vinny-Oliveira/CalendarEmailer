using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class EmailSender {

    /// <summary>
    /// Composes an email from a given calendar event
    /// </summary>
    /// <param name="calendarEvent">Calendar Event</param>
    /// <param name="fromEmail">Email address used to send from</param>
    /// <returns>Return the full Mail Message</returns>
    public MailMessage ComposeEmail(CalendarEvent calendarEvent, string fromEmail) {
        MailMessage mailMessage = new MailMessage() {
            IsBodyHtml = true,
            From = new MailAddress(fromEmail)
        };

        // Build attendee list on BCC
        var attendees = calendarEvent.EventItem.Attendees;

        foreach (var attendee in attendees) {
            mailMessage.Bcc.Add($"{attendee.Email}");
        }

        // Build message subject
        mailMessage.Subject = $"Reminder: {calendarEvent.EventItem.Summary} - {calendarEvent.GetEventTime()}";

        // Email body
        mailMessage.Body = calendarEvent.EventItem.Description;

        return mailMessage;
    }

    /// <summary>
    /// Send an email about the Google Calendar Event to the attendees
    /// </summary>
    /// <param name="mailMessage">The message to be sent</param>
    /// <param name="email">Sender's email address</param>
    /// <param name="password">Password of the sender's email address</param>
    public void SendEmail(MailMessage mailMessage, string email, string password) {
        SmtpClient client = new SmtpClient("smtp-mail.outlook.com") {
            Port = 587,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true
        };

        NetworkCredential networkCredential = new NetworkCredential(email, password);
        client.Credentials = networkCredential;
        client.Send(mailMessage);
    }

}
