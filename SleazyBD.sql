-- Check if Bookings table exists, and drop it if it does
IF OBJECT_ID('dbo.Bookings', 'U') IS NOT NULL
    DROP TABLE dbo.Bookings;

-- Check if Events table exists, and drop it if it does
IF OBJECT_ID('dbo.Events', 'U') IS NOT NULL
    DROP TABLE dbo.Events;

-- Check if Venues table exists, and drop it if it does
IF OBJECT_ID('dbo.Venues', 'U') IS NOT NULL
    DROP TABLE dbo.Venues;

-- Create Venues table
CREATE TABLE Venues
(
    VenueID INT PRIMARY KEY IDENTITY(1, 1),
    VenueName VARCHAR(255) NOT NULL,
    Location VARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    ImageURL VARCHAR(2048) -- Placeholder for image URL
);

-- Create Events table
CREATE TABLE Events
(
    EventID INT PRIMARY KEY IDENTITY(1, 1),
    EventName VARCHAR(255) NOT NULL,
    EventDate DATE NOT NULL,
    EventTime TIME NOT NULL,
    Description TEXT,
    ImageURL VARCHAR(2048), -- Placeholder for image URL
    VenueID INT NOT NULL,
    FOREIGN KEY (VenueID) REFERENCES Venues(VenueID)
);

-- Create Bookings table
CREATE TABLE Bookings
(
    BookingID INT PRIMARY KEY IDENTITY(1, 1),
    VenueID INT NOT NULL,
    EventID INT NOT NULL,
    BookingDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (VenueID) REFERENCES Venues(VenueID),
    FOREIGN KEY (EventID) REFERENCES Events(EventID)
);

-- Optional: Insert sample data
INSERT INTO Venues
    (VenueName, Location, Capacity, ImageURL)
VALUES
    ('Grand Hall', 'City Center', 500, 'https://via.placeholder.com/300/0000FF/FFFFFF?Text=Grand+Hall'),
    ('The Ballroom', 'Downtown', 300, 'https://via.placeholder.com/300/FF0000/FFFFFF?Text=The+Ballroom');

INSERT INTO Events
    (VenueID, EventName, EventDate, EventTime, Description, ImageURL)
VALUES
    (1, 'Tech Conference 2025', '2025-06-15', '09:00', 'Annual tech conference', 'https://via.placeholder.com/300/008000/FFFFFF?Text=Tech+Conf'),
    (2, 'Summer Gala', '2025-07-20', '19:00', 'Charity gala event', 'https://via.placeholder.com/300/FFA500/FFFFFF?Text=Summer+Gala');

INSERT INTO Bookings
    (VenueID, EventID)
VALUES
    (1, 1),
    (2, 2);
