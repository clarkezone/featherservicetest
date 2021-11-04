RGNAME="feather-rg"
SPNAME="feathergithub"
SUBID=$(az account show | jq '.id' --raw-output)

az group create -g "$RGNAME" -l "West US"

CREDS=$(az ad sp create-for-rbac --name "$SPNAME" --sdk-auth --role contributor \
	--scopes /subscriptions/$SUBID/resourceGroups/$RGNAME)
SHRED=$(echo $CREDS | jq '.clientSecret' --raw-output)
gh secret set AZURE_CREDENTIALS -b $SHRED
gh secret set SUBSCRIPTION_ID -b $SUBID
gh secret set RESOURCE_GROUP -b $RGNAME
