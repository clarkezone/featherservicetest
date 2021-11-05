RGNAME="feather-rg"
SPNAME="feathergithub"
SUBID=$(az account show | jq '.id' --raw-output)

az group create -g "$RGNAME" -l "West US"

CREDS=$(az ad sp create-for-rbac --name "$SPNAME" --sdk-auth --role contributor \
	--scopes /subscriptions/$SUBID/resourceGroups/$RGNAME)
SHRED=$(echo $CREDS | jq '.clientSecret' --raw-output)

echo $CREDS

#TODO this is failing, need to paste into github
gh secret set AZURE_CREDENTIALS -b $CREDS
gh secret set SUBSCRIPTION_ID -b $SUBID
gh secret set RESOURCE_GROUP -b $RGNAME

#TODO need to do this in the bicep:
#infrasetup git:(verifyarmimage) ✗ export registryid2=$(az acr show --name TODO --resource-group TODO-rg --query id --output tsv)
#➜  infrasetup git:(verifyarmimage) ✗ az role assignment create --assignee TODO --scope $registryid2 --role AcrPush
