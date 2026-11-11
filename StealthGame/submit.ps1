$ErrorActionPreference = "Stop"

$ProjectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$LogPath = Join-Path $ProjectRoot "submit_log.txt"
$AssignmentName = "UnityTask"
$SubmitFolderName = "Submit"

function Log([string]$text) {
    $stamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    "$stamp  $text" | Out-File -FilePath $LogPath -Append -Encoding utf8
}

try {
    "" | Out-File -FilePath $LogPath -Encoding utf8
    Log "Submit tool started."
    Log "ProjectRoot: $ProjectRoot"

    Write-Host ""
    Write-Host "========================================"
    Write-Host " Unity Project Submit Tool"
    Write-Host "========================================"
    Write-Host ""

    $RequiredFolders = @("Assets", "Packages", "ProjectSettings")

    foreach ($folder in $RequiredFolders) {
        $path = Join-Path $ProjectRoot $folder
        Log "Checking: $path"

        if (-not (Test-Path -LiteralPath $path -PathType Container)) {
            throw "$folder folder was not found. Put submit.bat and submit.ps1 in the Unity project root."
        }
    }

    $unity = Get-Process -Name "Unity" -ErrorAction SilentlyContinue
    if ($unity) {
        Write-Host "[WARNING] Unity is currently running." -ForegroundColor Yellow
        Write-Host "Please save your project before continuing."
        Write-Host ""
        $answer = Read-Host "Continue? (y/N)"
        if ($answer -notmatch '^[yY]$') {
            Log "Cancelled because Unity was running."
            exit 2
        }
    }

    do {
        $StudentId = Read-Host "Enter student ID"
        if ($null -ne $StudentId) { $StudentId = $StudentId.Trim() }
    } while ([string]::IsNullOrWhiteSpace($StudentId))

    foreach ($c in [IO.Path]::GetInvalidFileNameChars()) {
        $StudentId = $StudentId.Replace([string]$c, "_")
    }

    $SubmitDir = Join-Path $ProjectRoot $SubmitFolderName
    if (-not (Test-Path -LiteralPath $SubmitDir)) {
        New-Item -ItemType Directory -Path $SubmitDir | Out-Null
        Log "Created Submit directory."
    }

    $TimeStamp = Get-Date -Format "yyyyMMdd_HHmmss"
    $ZipName = "${StudentId}_${AssignmentName}_${TimeStamp}.zip"
    $ZipPath = Join-Path $SubmitDir $ZipName

    $Targets = @(
        (Join-Path $ProjectRoot "Assets"),
        (Join-Path $ProjectRoot "Packages"),
        (Join-Path $ProjectRoot "ProjectSettings")
    )

    Write-Host ""
    Write-Host "Creating ZIP..."
    Write-Host "Please wait."
    Write-Host ""

    Log "ZIP output: $ZipPath"
    Log "Starting Compress-Archive."

    Compress-Archive -LiteralPath $Targets -DestinationPath $ZipPath -CompressionLevel Optimal -Force

    if (-not (Test-Path -LiteralPath $ZipPath -PathType Leaf)) {
        throw "ZIP file was not created."
    }

    $zip = Get-Item -LiteralPath $ZipPath
    $sizeMB = [Math]::Round($zip.Length / 1MB, 1)

    Log "ZIP created successfully. Size: $sizeMB MB"

    Write-Host "========================================"
    Write-Host " Submit file created successfully"
    Write-Host "========================================"
    Write-Host ""
    Write-Host "File: $ZipName"
    Write-Host "Size: $sizeMB MB"
    Write-Host ""
    Write-Host "Only Assets / Packages / ProjectSettings were copied."
    Write-Host "The original Unity project was not modified."
    Write-Host ""

    Start-Process explorer.exe -ArgumentList "`"$SubmitDir`""
    exit 0
}
catch {
    $msg = $_.Exception.Message
    Log "ERROR: $msg"
    Write-Host ""
    Write-Host "[ERROR] $msg" -ForegroundColor Red
    Write-Host ""
    Write-Host "See submit_log.txt for details."
    exit 1
}
