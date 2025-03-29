using EventEase;
using Blazored.LocalStorage;

namespace EventEaseApp.Services;

public class EventService
{
    private List<EventModel> _events = new();
    private readonly ILocalStorageService _localStorage; // For storing and retrieving data from local storage
    private readonly RegistrationService _registrationService; // For managing event registrations

    private const string EventsKey = "events"; // Key for storing events in local storage
    private const string EventIdKey = "eventIdCounter"; // Key for storing the event ID counter in local storage
    private int _eventIdCounter = 1; // Default starting ID for events

    public EventService(ILocalStorageService localStorage, RegistrationService registrationService)
    {
        _localStorage = localStorage;
        _registrationService = registrationService;
    }

    /// <summary>
    /// Retrieves all events from local storage.
    /// </summary>
    public async Task<List<EventModel>> GetEvents()
    {
        _events = await _localStorage.GetItemAsync<List<EventModel>>(EventsKey) ?? new List<EventModel>();
        return _events;
    }

    /// <summary>
    /// Retrieves events for a specific page with pagination.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="pageSize">The number of events per page.</param>
    /// <returns>A tuple containing the events for the page and the total number of pages.</returns>
    public async Task<Tuple<List<EventModel>, int>> GetEventsByPage(int page, int pageSize)
    {
        _events = await _localStorage.GetItemAsync<List<EventModel>>(EventsKey) ?? new List<EventModel>();

        // Calculate total pages
        int totalPages = (int)Math.Ceiling((double)_events.Count / pageSize);

        // Get events for the requested page
        var pagedEvents = _events.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        // Update attendance for each event
        foreach (var eventItem in pagedEvents)
        {
            eventItem.Attendance = await _registrationService.GetAttendanceCount(eventItem.Id);
        }

        return Tuple.Create(pagedEvents, totalPages);
    }

    /// <summary>
    /// Adds a new event to the list and saves it to local storage.
    /// </summary>
    /// <param name="eventModel">The event to add.</param>
    public async Task AddEvent(EventModel eventModel)
    {
        _events = await _localStorage.GetItemAsync<List<EventModel>>(EventsKey) ?? new List<EventModel>();

        // Assign a unique ID to the new event
        eventModel.Id = await GetNextEventId();

        _events.Add(eventModel);
        await _localStorage.SetItemAsync(EventsKey, _events);
    }

    /// <summary>
    /// Deletes an event from the list and updates local storage.
    /// </summary>
    /// <param name="eventModel">The event to delete.</param>
    public async Task DeleteEvent(EventModel eventModel)
    {
        _events = await _localStorage.GetItemAsync<List<EventModel>>(EventsKey) ?? new List<EventModel>();
        _events.Remove(eventModel);
        await _localStorage.SetItemAsync(EventsKey, _events);
    }

    /// <summary>
    /// Retrieves the next unique ID for a new event.
    /// </summary>
    /// <returns>The next unique ID.</returns>
    private async Task<int> GetNextEventId()
    {
        // Retrieve the current ID counter from local storage
        _eventIdCounter = (await _localStorage.GetItemAsync<int?>(EventIdKey)) ?? 1;

        // Increment the ID counter
        int nextId = _eventIdCounter;
        _eventIdCounter++;

        // Save the updated ID counter back to local storage
        await _localStorage.SetItemAsync(EventIdKey, _eventIdCounter);

        return nextId;
    }
}