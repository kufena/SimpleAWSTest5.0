FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./SimpleAWSTest5.0API.csproj", "SimpleAWSTest5.0API/"]
COPY ["./*.cs", "SimpleAWSTest5.0API/"]
COPY ["./Controllers", "SimpleAWSTest5.0API/Controllers/"]
COPY ["./Properties", "SimpleAWSTest5.0API/Properties/"]
COPY ["./Utils", "SimpleAWSTest5.0API/Utils/"]
COPY ["./appsettings*", "SimpleAWSTest5.0API/"]
RUN echo `ls`
WORKDIR "/src/SimpleAWSTest5.0API"
RUN echo `pwd`
RUN echo `ls -lf`
RUN dotnet build "SimpleAWSTest5.0API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleAWSTest5.0API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleAWSTest5.0API.dll"]
