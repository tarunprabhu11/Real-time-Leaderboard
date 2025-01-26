# LeaderBoard API
This project is a backend system for a real-time leaderboard service that ranks users based on their scores in various games or activities, built with RESTful APIs. The system provides user authentication, score submission, real-time leaderboard updates, and user rankings, using Redis sorted sets to efficiently manage and query the leaderboard.

## Features
- **User Authentication**: Users can register and log in using JWT tokens for secure API access.
- **Score Submission**: Users can submit their scores for different games. Scores are stored in both SQL Server and Redis, with Redis used for managing real-time leaderboard updates.
- **Leaderboard Updates**: The system provides a global leaderboard showing the top users across all games. Real-time updates are handled by Redis sorted sets.
- **User Rankings**: Users can query their ranking on the leaderboard based on their submitted scores.
- **Top Players Report**: Generate reports for viewing the top players in specific games or for a given period.
- **Leaderboard Storage**: Redis Sorted Sets are used to store and rank scores in real-time, offering fast retrieval and update times for leaderboard data.
- **Admin Role**: Role-based access control allows admins to manage users, games, and scores.

## Technologies Used
- **ASP.NET Core 9**: Framework for building the web API.
- **JWT Authentication**: For securing API endpoints and enabling user login.
- **Redis**: Used for storing and managing real-time leaderboards with sorted sets.
- **Entity Framework Core**: For database operations and handling SQL Server.
- **SQL Server**: For data persistence (users, games, scores).
- **Swagger**: For API documentation and testing.

## API Endpoints

### Authentication

- **POST** `/api/auth/login`  
  **Description**: Login with username and password to receive a JWT token.  
  **Headers**: None  

- **POST** `/api/users/signup`  
  **Description**: Register a new user.  
  **Headers**: None  

### Users

- **GET** `/api/users`  
  **Description**: Get a list of all users (Admin required).  
  **Headers**: Authorization: Bearer `<token>`

- **GET** `/api/users/{id}`  
  **Description**: Get a specific user by ID.  
  **Headers**: Authorization: Bearer `<token>`

- **PUT** `/api/users/{id}`  
  **Description**: Update a user's details.  
  **Headers**: Authorization: Bearer `<token>`

- **DELETE** `/api/users/{id}`  
  **Description**: Delete a user (Admin required).  
  **Headers**: Authorization: Bearer `<token>`

### Games

- **GET** `/api/games`  
  **Description**: Get all available games (Admin required).  
  **Headers**: Authorization: Bearer `<token>`

- **GET** `/api/games/{id}`  
  **Description**: Get a specific game by ID.  
  **Headers**: Authorization: Bearer `<token>`

- **POST** `/api/games/add`  
  **Description**: Add a new game (Admin required).  
  **Headers**: Authorization: Bearer `<token>`

- **DELETE** `/api/games/{id}`  
  **Description**: Delete a game (Admin required).  
  **Headers**: Authorization: Bearer `<token>`

### Scores

- **GET** `/api/scores`  
  **Description**: Get all scores.  
  **Headers**: Authorization: Bearer `<token>`

- **GET** `/api/scores/{id}`  
  **Description**: Get a specific score.  
  **Headers**: Authorization: Bearer `<token>`

- **POST** `/api/scores`  
  **Description**: Submit a new score for a user.  
  **Headers**: Authorization: Bearer `<token>`

- **PUT** `/api/scores/{id}`  
  **Description**: Update an existing score.  
  **Headers**: Authorization: Bearer `<token>`

- **DELETE** `/api/scores/{id}`  
  **Description**: Delete a score.  
  **Headers**: Authorization: Bearer `<token>`

- **GET** `/api/scores/leaderboard/{gameId}`  
  **Description**: Get the leaderboard for a specific game.  
  **Headers**: Authorization: Bearer `<token>`

### Real-Time Leaderboard with Redis

- Redis Sorted Sets are used to store and rank scores for users.
- Leaderboards are updated and queried in real-time, allowing efficient retrieval of top scores.

## Installation & Setup

1. **Clone the repository**  
   ```bash
   git clone https://github.com/yourusername/leaderboard-api.git
   cd leaderboard-api
2. **Install Dependencies**
   - Run the following command to restore the required dependencies:
   ```bash
   dotnet restore
3. **Configure the Database and Redis**  
   - Update the appsettings.json file with your SQL Server connection string under ConnectionStrings:DefaultConnection.
   - Configure Redis connection details under Redis in appsettings.json.
   - Set up JWT secrets under Jwt in appsettings.json.
4. ** Execute the SQL script**
   - execute the SQLquery4.sql against your database
5. ## Running the Application
   - To run the application, use the following command:
   ```bash
   dotnet run

## Swagger API Documentation

  You can access the Swagger UI for testing and exploring the API by navigating to:

  [https://localhost:5001/swagger](https://localhost:5001/swagger)

## Error Handling

  The API uses the following HTTP status codes for error handling:

  - **400 Bad Request**: Invalid request data.
  - **401 Unauthorized**: Missing or invalid JWT token.
  - **403 Forbidden**: Insufficient permissions to access the endpoint.
  - **404 Not Found**: Resource not found.
  - **500 Internal Server Error**: Server error.


