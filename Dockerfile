# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY KurumsalEgitimSitesi/KurumsalEgitimSitesi.csproj KurumsalEgitimSitesi/
RUN dotnet restore KurumsalEgitimSitesi/KurumsalEgitimSitesi.csproj

# Copy everything and build
COPY KurumsalEgitimSitesi/ KurumsalEgitimSitesi/
RUN dotnet publish KurumsalEgitimSitesi/KurumsalEgitimSitesi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

ENTRYPOINT ["dotnet", "KurumsalEgitimSitesi.dll"]
