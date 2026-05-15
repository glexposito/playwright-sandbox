FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["PlaywrightSandbox.Web/PlaywrightSandbox.Web.csproj", "PlaywrightSandbox.Web/"]
RUN dotnet restore "PlaywrightSandbox.Web/PlaywrightSandbox.Web.csproj"

COPY . .
WORKDIR /src/PlaywrightSandbox.Web
RUN dotnet publish "PlaywrightSandbox.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ConnectionStrings__Games="Data Source=/data/games.db"

RUN mkdir -p /data

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "PlaywrightSandbox.Web.dll"]
