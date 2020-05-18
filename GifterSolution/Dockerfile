FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish WebApp -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:SqlServerConnection="server=alpha.akaver.com;database=student2018_meeenm_gifterapp2019;user=student2018;password=student2018"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]