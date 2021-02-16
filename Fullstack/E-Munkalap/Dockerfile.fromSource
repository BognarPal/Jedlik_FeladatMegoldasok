#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_15.x | bash \
    && apt-get install nodejs -yq \
	&& apt-get install subversion -yq
	
RUN svn checkout https://github.com/BognarPal/Jedlik_FeladatMegoldasok/trunk/Fullstack/E-Munkalap	
	
WORKDIR "/src/E-Munkalap/E-Munkalap"
RUN dotnet build "E-Munkalap.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "E-Munkalap.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Munkalap.dll"]