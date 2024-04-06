using System;


class Address {
    private readonly string streetAddress;
    private readonly string city;
    private readonly string stateOrProvince;
    private readonly string country;

    public Address(string streetAddress, string city, string stateOrProvince, string country) {
        this.streetAddress = streetAddress;
        this.city = city;
        this.stateOrProvince = stateOrProvince;
        this.country = country;
    }

    public string GetFormattedAddress() {
        return $"{streetAddress}, {city}, {stateOrProvince}, {country}";
    }
}


abstract class Event {
    protected string title;
    protected string description;
    protected DateTime date;
    protected string time;
    protected Address address;

    public Event(string title, string description, DateTime date, string time, Address address) {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string StandardDetails() {
        return $"{title}, {description}, on {date.ToShortDateString()} at {time}, Location: {address.GetFormattedAddress()}";
    }

    public abstract string FullDetails();

    public string ShortDescription() {
        return $"Event: {this.GetType().Name}, {title}, Date: {date.ToShortDateString()}";
    }
}


class Lecture : Event {
    private readonly string speakerName;
    private readonly int capacity;

    public Lecture(string title, string description, DateTime date, string time, Address address, string speakerName, int capacity)
    : base(title, description, date, time, address) {
        this.speakerName = speakerName;
        this.capacity = capacity;
    }

    public override string FullDetails() {
        return $"{StandardDetails()}, Speaker: {speakerName}, Capacity: {capacity}";
    }
}


class Reception : Event {
    private readonly string rsvpEmail;

    public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
    : base(title, description, date, time, address) {
        this.rsvpEmail = rsvpEmail;
    }

    public override string FullDetails() {
        return $"{StandardDetails()}, RSVP at: {rsvpEmail}";
    }
}

class OutdoorGathering : Event {
    private readonly string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weatherForecast)
    : base(title, description, date, time, address) {
        this.weatherForecast = weatherForecast;
    }

    public override string FullDetails() {
        return $"{StandardDetails()}, Weather Forecast: {weatherForecast}";
    }
}

class Program {
    static void Main() {
        Address address1 = new Address("123 Main St", "Anytown", "Anystate", "USA");
        Event lecture = new Lecture("Lecture on failth", "Deep dive into Faith", new DateTime(2024, 10, 15), "2:00 PM", address1, "Andres  Montero", 100);
        Event reception = new Reception("Mision preparation", "New kind of missioneries", new DateTime(2024, 10, 20), "6:00 PM", address1, "rsvp@example.com");
        Address address2 = new Address("456 Park Ave", "Anytown", "Anystate", "USA");
        Event outdoor = new OutdoorGathering("Celestial Music", "An evening of music", new DateTime(2024, 11, 5), "5:00 PM", address2, "Celestial band");

        Event[] events = { lecture, reception, outdoor };

        foreach (Event ev in events) {
            Console.WriteLine(ev.FullDetails());
            Console.WriteLine(ev.ShortDescription());
            Console.WriteLine(); 
        }
    }
}
