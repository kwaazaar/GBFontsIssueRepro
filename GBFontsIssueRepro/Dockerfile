#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GBFontsIssueRepro/GBFontsIssueRepro.csproj", "GBFontsIssueRepro/"]
RUN dotnet restore "GBFontsIssueRepro/GBFontsIssueRepro.csproj"
COPY . .
WORKDIR "/src/GBFontsIssueRepro"
RUN dotnet build "GBFontsIssueRepro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GBFontsIssueRepro.csproj" -c Release -o /app/publish

FROM base AS final

# Contrib debian packages enablen (vergelijkbaar met multiverse op ubuntu)
RUN sed -i 's/ buster main/ buster main contrib/g' /etc/apt/sources.list
RUN sed -i 's+ buster/updates main+ buster/updates main contrib+g' /etc/apt/sources.list
RUN sed -i 's/ buster-updates main/ buster-updates main contrib/g' /etc/apt/sources.list

# Install fonts
RUN echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections
RUN apt-get update \
	&& apt-get install fontconfig ttf-mscorefonts-installer -y
# Update fonts cache
RUN fc-cache -f -v

WORKDIR /app
COPY --from=publish /app/publish .

# Output folder (so it can be mapped)
RUN mkdir /app/output

ENTRYPOINT ["dotnet", "GBFontsIssueRepro.dll"]