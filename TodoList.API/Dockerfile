# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and csproj files
COPY ["TodoList.API/TodoList.API.csproj", "TodoList.API/"]
COPY ["TodoList.Application/TodoList.Application.csproj", "TodoList.Application/"]
COPY ["TodoList.Domain/TodoList.Domain.csproj", "TodoList.Domain/"]
COPY ["TodoList.Infrastructure/TodoList.Infrastructure.csproj", "TodoList.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "TodoList.API/TodoList.API.csproj"

# Copy all project files
COPY TodoList.API/. TodoList.API/
COPY TodoList.Application/. TodoList.Application/
COPY TodoList.Domain/. TodoList.Domain/
COPY TodoList.Infrastructure/. TodoList.Infrastructure/

# Build and publish
WORKDIR "/src/TodoList.API"
RUN dotnet build "TodoList.API.csproj" -c Release -o /app/build
RUN dotnet publish "TodoList.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5199
ENV ASPNETCORE_URLS=http://+:5199
ENTRYPOINT ["dotnet", "TodoList.API.dll", "--launch-profile", "http"]