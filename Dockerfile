# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution & restore
COPY *.sln .
COPY MedicalCare.Domain/ MedicalCare.Domain/
COPY MedicalCare.Application/ MedicalCare.Application/
COPY MedicalCare.Infrastructure/ MedicalCare.Infrastructure/
COPY MedicalCare.Presentation/ MedicalCare.Presentation/

RUN dotnet restore

# Build & publish
WORKDIR /src/MedicalCare.Presentation
RUN dotnet publish -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "MedicalCare.Presentation.dll"]
