FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

WORKDIR /src

# Restore dependencies.
COPY *.csproj ./
RUN dotnet restore "PeminjamanRuangan.API.csproj"

# Copy source and publish the app.
COPY . ./
RUN dotnet publish "PeminjamanRuangan.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    -p:UseAppHost=false

# Image runtime.
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app

RUN apk add --no-cache krb5-libs

COPY --from=build /app/publish ./

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PeminjamanRuangan.API.dll"]