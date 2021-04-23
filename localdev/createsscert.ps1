# Taken from https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide

$path = "C:\certs"
If(!(test-path $path))
{
      New-Item -ItemType Directory -Force -Path $path
} else {
    Write-Output("Found $path")
}

$cert = New-SelfSignedCertificate -DnsName @("contoso.com", "www.contoso.com") -CertStoreLocation "cert:\LocalMachine\My"
$certKeyPath = "c:\certs\contoso.com.pfx"
$password = ConvertTo-SecureString 'password' -AsPlainText -Force
$cert | Export-PfxCertificate -FilePath $certKeyPath -Password $password
$rootCert = $(Import-PfxCertificate -FilePath $certKeyPath -CertStoreLocation 'Cert:\LocalMachine\Root' -Password $password)

Write-Output "Created $rootCert"