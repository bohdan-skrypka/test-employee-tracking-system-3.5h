#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Employee/Employee.API/Employee.API.csproj", "Employee/Employee.API/"]
COPY ["Employee/Employee.API.DataContracts/Employee.API.DataContracts.csproj", "Employee/Employee.API.DataContracts/"]
COPY ["Employee/Employee.Services/Employee.Services.csproj", "Employee/Employee.Services/"]
COPY ["Employee.Repository/Employee.Repository.csproj", "Employee.Repository/"]
COPY ["Employee/Employee.DB.Context/Employee.DB.Context.csproj", "Employee/Employee.DB.Context/"]
COPY ["Employee/Employee.API.Common/Employee.API.Common.csproj", "Employee/Employee.API.Common/"]
COPY ["Employee/Employee.IoC.Configuration/Employee.IoC.Configuration.csproj", "Employee/Employee.IoC.Configuration/"]
RUN dotnet restore "Employee/Employee.API/Employee.API.csproj"
COPY . .
WORKDIR "/src/Employee/Employee.API"
RUN dotnet build "Employee.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Employee.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Employee.API.dll"]