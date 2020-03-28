# Backend of heldnodig

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
Download and install docker from https://www.docker.com/get-started
```
### Installing

A step by step series of examples that tell you how to get a development env running

```
1. Clone the repository with modules git clone --recursive
2. Run with docker `docker-compose up -d`
```

To check if the application is running go to `http://localhost/`
* Swagger is available at `http://localhost/swagger`

* The application will run migrations when it stars, and seed dummy data in Development

### Using Rider

Since the Rider debugger is not available when using docker-compose.
Running it with a launch profile might be a better option during development.
Rider should automatically detect the launch profile which you can run with the Play button.
If you want you can still use docker-compose to run just the database.
You can do so by running `docker-compose up -d database`.
You can also add this as a Docker run profile in Rider.

## Running Locally

### Using .NET launch profiles

Using Rider or Visual studio you can run the project with .NET Launch profiles which will benefit you
of having debug capabilities. Which as of now is not possible with Rider+Docker combination.
To run the additional services like mongodb, postgresql and mock the api gateway run:

```bash
docker-compose up database
```

## Adding an (aggregate root) Entity

1. Create the entity under `Entities/{Entity}/{Entity}.cs`
2. Create a Repository interface implementing `IRepository<Entity>` under `Entities/<Entity>/I{Entity}Repository.cs`
3. Add the `DbSet<{Entity}>` to `HeldNodigContext.cs` under `Infrastructure`
4. Create the actual repository in `Infrastructure/Repositories/{Entity}/{Entity}Repository.cs`
4. You can just extend the `BaseRepository<{Entity}> for most functionality.
5. Use your repository interface in a controller, or where ever needed. It will be auto resolved.
