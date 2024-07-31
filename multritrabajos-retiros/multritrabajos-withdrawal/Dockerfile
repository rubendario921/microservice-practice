FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
 
COPY ./publish /app
ENV ASPNETCORE_URLS=http://*:8083
ENV ASPNETCORE_ENVIRONMENT docker
 
EXPOSE 8083
 
ENTRYPOINT ["dotnet", "/app/multritrabajos-withdrawal.dll"]