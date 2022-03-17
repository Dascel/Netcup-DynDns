FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Netcup-DynDns/Netcup-DynDns.csproj", "Netcup-DynDns/"]
RUN dotnet restore "Netcup-DynDns/Netcup-DynDns.csproj"
COPY . .
WORKDIR "/src/Netcup-DynDns"
RUN dotnet build "Netcup-DynDns.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Netcup-DynDns.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Netcup-DynDns.dll"]
