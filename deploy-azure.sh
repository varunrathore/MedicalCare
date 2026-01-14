#!/bin/bash

# Azure Deployment Script for MedicalCare Application
# This script automates the deployment of the MedicalCare application to Azure

set -e

# Color codes for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Configuration - Edit these values
RESOURCE_GROUP="medicalcare-rg"
LOCATION="eastus"
SQL_SERVER_NAME="medicalcare-sql-$(date +%s)"
SQL_ADMIN_USER="sqladmin"
SQL_ADMIN_PASSWORD="" # Will prompt if empty
DATABASE_NAME="MedicalCareDb"
APP_SERVICE_PLAN="medicalcare-plan"
WEB_APP_NAME="medicalcare-app-$(date +%s)"
SKU="B1"

echo -e "${GREEN}╔════════════════════════════════════════════════════╗${NC}"
echo -e "${GREEN}║   MedicalCare Azure Deployment Script             ║${NC}"
echo -e "${GREEN}╚════════════════════════════════════════════════════╝${NC}"

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo -e "${RED}Error: Azure CLI is not installed.${NC}"
    echo "Please install it from: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli"
    exit 1
fi

# Check if logged in to Azure
echo -e "\n${YELLOW}Checking Azure login status...${NC}"
if ! az account show &> /dev/null; then
    echo -e "${YELLOW}Not logged in. Logging in to Azure...${NC}"
    az login
fi

# Prompt for SQL password if not set
if [ -z "$SQL_ADMIN_PASSWORD" ]; then
    echo -e "\n${YELLOW}Enter SQL Server admin password (min 8 chars, must include uppercase, lowercase, numbers):${NC}"
    read -s SQL_ADMIN_PASSWORD
    echo
fi

# Display configuration
echo -e "\n${GREEN}Deployment Configuration:${NC}"
echo "  Resource Group: $RESOURCE_GROUP"
echo "  Location: $LOCATION"
echo "  SQL Server: $SQL_SERVER_NAME"
echo "  Database: $DATABASE_NAME"
echo "  App Service: $WEB_APP_NAME"
echo "  SKU: $SKU"

echo -e "\n${YELLOW}Do you want to proceed with deployment? (yes/no)${NC}"
read -r CONFIRM
if [ "$CONFIRM" != "yes" ]; then
    echo -e "${RED}Deployment cancelled.${NC}"
    exit 0
fi

# Create Resource Group
echo -e "\n${GREEN}[1/8] Creating resource group...${NC}"
az group create --name "$RESOURCE_GROUP" --location "$LOCATION"

# Create SQL Server
echo -e "\n${GREEN}[2/8] Creating SQL Server...${NC}"
az sql server create \
    --name "$SQL_SERVER_NAME" \
    --resource-group "$RESOURCE_GROUP" \
    --location "$LOCATION" \
    --admin-user "$SQL_ADMIN_USER" \
    --admin-password "$SQL_ADMIN_PASSWORD"

# Create SQL Database
echo -e "\n${GREEN}[3/8] Creating SQL Database...${NC}"
az sql db create \
    --resource-group "$RESOURCE_GROUP" \
    --server "$SQL_SERVER_NAME" \
    --name "$DATABASE_NAME" \
    --service-objective S0

# Configure SQL Server firewall
echo -e "\n${GREEN}[4/8] Configuring SQL Server firewall...${NC}"
az sql server firewall-rule create \
    --resource-group "$RESOURCE_GROUP" \
    --server "$SQL_SERVER_NAME" \
    --name AllowAzureServices \
    --start-ip-address 0.0.0.0 \
    --end-ip-address 0.0.0.0

# Create App Service Plan
echo -e "\n${GREEN}[5/8] Creating App Service Plan...${NC}"
az appservice plan create \
    --name "$APP_SERVICE_PLAN" \
    --resource-group "$RESOURCE_GROUP" \
    --sku "$SKU" \
    --is-linux

# Create Web App
echo -e "\n${GREEN}[6/8] Creating Web App...${NC}"
az webapp create \
    --resource-group "$RESOURCE_GROUP" \
    --plan "$APP_SERVICE_PLAN" \
    --name "$WEB_APP_NAME" \
    --runtime "DOTNET:9.0"

# Configure Connection String
echo -e "\n${GREEN}[7/8] Configuring connection string...${NC}"
SQL_CONNECTION="Server=tcp:${SQL_SERVER_NAME}.database.windows.net,1433;Database=${DATABASE_NAME};User ID=${SQL_ADMIN_USER};Password=${SQL_ADMIN_PASSWORD};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

az webapp config connection-string set \
    --resource-group "$RESOURCE_GROUP" \
    --name "$WEB_APP_NAME" \
    --connection-string-type SQLAzure \
    --settings DefaultConnection="$SQL_CONNECTION"

# Publish Application
echo -e "\n${GREEN}[8/8] Publishing application...${NC}"
echo "  - Building project..."
dotnet publish MedicalCare.Presentation/MedicalCare.Presentation.csproj -c Release -o ./publish

echo "  - Creating deployment package..."
cd publish
zip -r -q ../deploy.zip .
cd ..

echo "  - Deploying to Azure..."
az webapp deployment source config-zip \
    --resource-group "$RESOURCE_GROUP" \
    --name "$WEB_APP_NAME" \
    --src deploy.zip

# Clean up local files
rm -rf publish deploy.zip

# Apply database migrations
echo -e "\n${GREEN}Applying database migrations...${NC}"
dotnet ef database update \
    --project MedicalCare.Infrastructure \
    --startup-project MedicalCare.Presentation \
    --connection "$SQL_CONNECTION"

echo -e "\n${GREEN}╔════════════════════════════════════════════════════╗${NC}"
echo -e "${GREEN}║   Deployment Completed Successfully!               ║${NC}"
echo -e "${GREEN}╚════════════════════════════════════════════════════╝${NC}"

echo -e "\n${GREEN}Application URL:${NC} https://${WEB_APP_NAME}.azurewebsites.net"
echo -e "${GREEN}SQL Server:${NC} ${SQL_SERVER_NAME}.database.windows.net"
echo -e "${GREEN}Database:${NC} ${DATABASE_NAME}"

echo -e "\n${YELLOW}To view logs, run:${NC}"
echo "  az webapp log tail --resource-group $RESOURCE_GROUP --name $WEB_APP_NAME"

echo -e "\n${YELLOW}To delete all resources, run:${NC}"
echo "  az group delete --name $RESOURCE_GROUP --yes --no-wait"

echo -e "\n${GREEN}Opening application in browser...${NC}"
az webapp browse --resource-group "$RESOURCE_GROUP" --name "$WEB_APP_NAME"
