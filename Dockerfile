# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy all project files (individual projects) into the container for restore
COPY Web.Api/*.csproj Web.Api/
COPY Application/*.csproj Application/
COPY Domain/*.csproj Domain/
COPY Infrastructure/*.csproj Infrastructure/

# Restore all dependencies
RUN dotnet restore Web.Api/Web.Api.csproj

# Copy the rest of the application code
COPY . .

# Build and publish the application
RUN dotnet publish Web.Api/Web.Api.csproj -c Release -o /app/publish

# Use the official ASP.NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory for runtime
WORKDIR /app

# Copy the published application files from the build stage
COPY --from=build /app/publish .

# Expose the port the application runs on
EXPOSE 5177

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Web.Api.dll"]
