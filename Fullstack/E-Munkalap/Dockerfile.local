#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["E-Munkalap/E-Munkalap.csproj", "E-Munkalap/"]
COPY ["E-Munkalap.Data/E-Munkalap.Data.csproj", "E-Munkalap.Data/"]
RUN dotnet restore "E-Munkalap/E-Munkalap.csproj"
COPY . .
WORKDIR "/src/E-Munkalap"
RUN dotnet build "E-Munkalap.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "E-Munkalap.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Munkalap.dll"]