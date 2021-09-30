#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Galicia.Test.RestApi/Galicia.Test.RestApi.csproj", "Galicia.Test.RestApi/"]
COPY ["Galicia.Test.Infrastructure/Galicia.Test.Infrastructure.csproj", "Galicia.Test.Infrastructure/"]
COPY ["Galicia.Test.Core/Galicia.Test.Core.csproj", "Galicia.Test.Core/"]
COPY ["Galicia.Test.Shared/Galicia.Test.Shared.csproj", "Galicia.Test.Shared/"]
COPY ["Galicia.Test.BusinessRules/Galicia.Test.BusinessRules.csproj", "Galicia.Test.BusinessRules/"]
COPY ["Galicia.Test.BusinessRules.Implementation/Galicia.Test.BusinessRules.Implementation.csproj", "Galicia.Test.BusinessRules.Implementation/"]
RUN dotnet restore "Galicia.Test.RestApi/Galicia.Test.RestApi.csproj"
COPY . .
WORKDIR "/src/Galicia.Test.RestApi"
RUN dotnet build "Galicia.Test.RestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Galicia.Test.RestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Galicia.Test.RestApi.dll"]
