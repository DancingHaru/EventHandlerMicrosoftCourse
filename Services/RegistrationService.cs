using EventEase;
using Blazored.LocalStorage;

public class RegistrationService
{
    private readonly ILocalStorageService _localStorage; // For storing and retrieving data from local storage
    private const string RegistrationsKey = "registrations"; // Key for storing registrations in local storage
    private List<RegistrationModel> _registrations = new();

    public RegistrationService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    /// <summary>
    /// Retrieves all registrations from local storage.
    /// </summary>
    /// <returns>A list of all registrations.</returns>
    public async Task<List<RegistrationModel>> GetRegistrationsForEvent()
    {
        _registrations = await _localStorage.GetItemAsync<List<RegistrationModel>>(RegistrationsKey) ?? new List<RegistrationModel>();
        return _registrations;
    }

    /// <summary>
    /// Adds a new registration and saves it to local storage.
    /// </summary>
    /// <param name="registration">The registration to add.</param>
    public async Task AddRegistration(RegistrationModel registration)
    {
        //Fetch list from local storage
        _registrations = await _localStorage.GetItemAsync<List<RegistrationModel>>(RegistrationsKey) ?? new List<RegistrationModel>();
        _registrations.Add(registration);

        // Save the updated list back to local storage
        await _localStorage.SetItemAsync(RegistrationsKey, _registrations);
    }

    /// <summary>
    /// Deletes a registration and updates local storage.
    /// </summary>
    /// <param name="registration">The registration to delete.</param>
    public async Task DeleteRegistration(RegistrationModel registration)
    {
        //Fetch list from local storage
        _registrations = await _localStorage.GetItemAsync<List<RegistrationModel>>(RegistrationsKey) ?? new List<RegistrationModel>();
        _registrations.Remove(registration);

        // Save the updated list back to local storage
        await _localStorage.SetItemAsync(RegistrationsKey, _registrations);
    }

    /// <summary>
    /// Gets the attendance count for a specific event.
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>The number of registrations for the event.</returns>
    public async Task<int> GetAttendanceCount(int eventId)
    {
        //Fetch list from local storage
        _registrations = await _localStorage.GetItemAsync<List<RegistrationModel>>(RegistrationsKey) ?? new List<RegistrationModel>();
        return _registrations.Count(r => r.EventId == eventId);
    }
}