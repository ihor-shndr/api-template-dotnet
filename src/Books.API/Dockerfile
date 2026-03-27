FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/Books.API/Books.API.csproj src/Books.API/
COPY src/Books.Data/Books.Data.csproj src/Books.Data/
COPY src/Books.Domain/Books.Domain.csproj src/Books.Domain/
COPY src/Books.Common/Books.Common.csproj src/Books.Common/

RUN dotnet restore src/Books.API/Books.API.csproj

COPY src/ src/
RUN dotnet publish src/Books.API/Books.API.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "Books.API.dll"]
