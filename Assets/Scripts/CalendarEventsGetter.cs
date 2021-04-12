using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarEventsGetter {

    /// <summary>
    /// Get a list of Google Calendar events that happen in a day provided by the user
    /// </summary>
    /// <param name="email"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public Events GetEvents(string email, CalendarService service, DateTime startDate) {
        EventsResource.ListRequest request = service.Events.List(email);

        DateTime endDate = startDate.AddDays(1.0);

        // Set what events to get
        request.TimeMin = startDate;
        request.TimeMax = endDate;
        request.ShowDeleted = false;
        request.SingleEvents = true;
        request.MaxResults = 100;
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        return request.Execute();
    }

}
