FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Authorization/PsychologicalSupportPlatform.Authorization.API/PsychologicalSupportPlatform.Authorization.API.csproj", "PsychologicalSupportPlatform.Authorization.API/"]
COPY ["./Authorization/PsychologicalSupportPlatform.Authorization.Infrastructure/PsychologicalSupportPlatform.Authorization.Infrastructure.csproj", "PsychologicalSupportPlatform.Authorization.Infrastructure/"]
COPY ["./Authorization/PsychologicalSupportPlatform.Authorization.Application/PsychologicalSupportPlatform.Authorization.Application.csproj", "PsychologicalSupportPlatform.Authorization.Application/"]
COPY ["./Authorization/PsychologicalSupportPlatform.Authorization.Domain/PsychologicalSupportPlatform.Authorization.Domain.csproj", "PsychologicalSupportPlatform.Authorization.Domain/"]
COPY ["./PsychologicalSupportPlatform.Common/PsychologicalSupportPlatform.Common.csproj", "PsychologicalSupportPlatform.Common/"]
RUN dotnet restore "./Authorization/PsychologicalSupportPlatform.Authorization.API/PsychologicalSupportPlatform.Authorization.API.csproj"
COPY . .
WORKDIR "/src/PsychologicalSupportPlatform.Authorization.API"
RUN dotnet build "PsychologicalSupportPlatform.Authorization.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PsychologicalSupportPlatform.Authorization.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PsychologicalSupportPlatform.Authorization.API.dll"]
