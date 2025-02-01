FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TodoList.API/TodoList.API.csproj", "TodoList.API/"]
COPY ["TodoList.Domain/TodoList.Domain.csproj", "TodoList.Domain/"]
COPY ["TodoList.Infrastructure/TodoList.Infrastructure.csproj", "TodoList.Infrastructure/"]
COPY ["TodoList.Application/TodoList.Application.csproj", "TodoList.Application/"]
RUN dotnet restore "TodoList.API/TodoList.API.csproj"
COPY . .
WORKDIR "/src/TodoList.API"
RUN dotnet build "TodoList.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TodoList.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.API.dll"]
