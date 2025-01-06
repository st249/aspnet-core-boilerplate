#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CompanyName.ProjectName.Presenter/CompanyName.ProjectName.Presenter.csproj", "CompanyName.ProjectName.Presenter/"]
RUN dotnet restore "CompanyName.ProjectName.Presenter/CompanyName.ProjectName.Presenter.csproj"
COPY . .
WORKDIR "/src/CompanyName.ProjectName.Presenter"
RUN dotnet build "CompanyName.ProjectName.Presenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyName.ProjectName.Presenter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyName.ProjectName.Presenter.dll"]

