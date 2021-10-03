SPNAME="feathergithub"
RG="feather-rg"

SPID=$(az ad sp list --display-name=$SPNAME --query '[].objectId' | jq '.[0]' --raw-output)
az ad sp delete --id=$SPID
az group delete --resource-group=$RG
