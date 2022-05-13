FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FileDisplayApp/FileDisplayApp.csproj", "FileDisplayApp/"]
RUN dotnet restore "FileDisplayApp/FileDisplayApp.csproj"
COPY . .
WORKDIR "/src/FileDisplayApp"
RUN dotnet build "FileDisplayApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileDisplayApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileDisplayApp.dll"]
