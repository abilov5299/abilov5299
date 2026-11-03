# Paylama: proqramı .NET quraşdırılmadan işləyən iki qovluq kimi hazırlayır.
#   win-x86 — 32 bit Windows-da (64 bit-də də işləyir)
#   win-x64 — 64 bit Windows-da
# İstifadə: powershell -ExecutionPolicy Bypass -File tools\Paylama.ps1
$ErrorActionPreference = 'Stop'
$kok = Split-Path $PSScriptRoot -Parent
$proj = Join-Path $kok 'TexnikaReyestri\TexnikaReyestri.csproj'
$cix = Join-Path $kok 'Paylama'

foreach ($rid in 'win-x86', 'win-x64') {
    $q = Join-Path $cix $rid
    if (Test-Path $q) { Remove-Item $q -Recurse -Force }
    dotnet publish $proj -c Release -r $rid --self-contained true -o $q
    if ($LASTEXITCODE -ne 0) { throw "$rid publish alınmadı" }
    $zip = Join-Path $cix "TemirReyestri-$rid.zip"
    if (Test-Path $zip) { Remove-Item $zip -Force }
    Compress-Archive -Path (Join-Path $q '*') -DestinationPath $zip
    Write-Host "Hazır: $zip"
}
