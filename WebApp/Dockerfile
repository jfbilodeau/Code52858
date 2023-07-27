# To build:
#  docker build -t jfwebapp:v2 .
# To run:
#  docker run -it -p 80:80 -e Message="Hello from command line!" jfwebapp:v2

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# mkdir -p /app
# cd /app
WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=build-env /app/out .

ENV Message="Hello from Dockerfile"

ENTRYPOINT ["dotnet", "WebApp.dll"]