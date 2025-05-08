# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY CustomerJobWeb.csproj ./
RUN dotnet restore ./CustomerJobWeb.csproj

# Copy the rest of the source code
COPY . .

# Build the app
RUN dotnet publish ./CustomerJobWeb.csproj -c Release -o /app/publish

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV PORT=8080
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE $PORT

ENTRYPOINT ["dotnet", "CustomerJobWeb.dll"]
