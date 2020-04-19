FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Actio.API/Actio.API.csproj", "src/Actio.API/"]
COPY ["src/Actio.Common/Actio.Common.csproj", "src/Actio.Common/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
RUN dotnet restore "src/Actio.API/Actio.API.csproj"
COPY . .
WORKDIR "/src/src/Actio.API"
RUN dotnet build "Actio.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Actio.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Actio.API.dll"]