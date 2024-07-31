FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
 
COPY ./publish /app
ENV ASPNETCORE_URLS=http://*:8082
ENV ASPNETCORE_ENVIRONMENT docker
 
EXPOSE 8082
 
ENTRYPOINT ["dotnet", "/app/multitrabajos-deposit.dll"]