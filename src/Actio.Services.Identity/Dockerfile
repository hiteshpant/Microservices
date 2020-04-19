FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Actio.Services.Identity/Actio.Services.Identity.csproj", "Actio.Services.Identity/"]
RUN dotnet restore "Actio.Services.Identity/Actio.Services.Identity.csproj"
COPY . .
WORKDIR "/src/Actio.Services.Identity"
RUN dotnet build "Actio.Services.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Actio.Services.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Actio.Services.Identity.dll"]