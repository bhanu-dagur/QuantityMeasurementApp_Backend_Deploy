# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY  . .
RUN dotnet restore QuantityMeasurementAPI/QuantityMeasurementAPI.csproj
RUN dotnet publish QuantityMeasurementAPI/QuantityMeasurementAPI.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy build output
COPY --from=build /app/publish .

Expose 10000
ENV PORT=10000


# Render requires dynamic PORT
ENV ASPNETCORE_URLS=http://+:10000


# Run app
ENTRYPOINT ["dotnet", "QuantityMeasurementAPI.dll"]