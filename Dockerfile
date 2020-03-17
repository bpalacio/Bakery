# builder image stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /sources/
COPY Bakery.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish

# base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=0 /sources/bin/Debug/netcoreapp3.1/publish/ releases

# set up network (doesn't work on heroku)
EXPOSE 5000/tcp
EXPOSE 5001/tcp

ENV ASPNETCORE_URLS http://*:5000
WORKDIR "releases"
ENTRYPOINT ["dotnet", "Bakery.dll"]
