FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101 AS build

ARG VERSION

WORKDIR /build/

COPY WashingTime/WashingTime.csproj WashingTime/WashingTime.csproj

                  
RUN dotnet restore WashingTime/WashingTime.csproj
COPY . .
RUN dotnet build WashingTime/WashingTime.csproj -c Release /p:Version=$([ -z "$VERSION" ] && echo "0.0.0" || echo $VERSION) -o /app

FROM build AS publish
RUN dotnet publish WashingTime/WashingTime.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WashingTime.dll"]
