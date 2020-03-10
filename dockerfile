# base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY /bin/Debug/netcoreapp3.1/publish/ app/

# set up network (doesn't work on heroku) 
EXPOSE 5000/tcp
EXPOSE 5001/tcp

ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "app/Bakery.dll"]
 

