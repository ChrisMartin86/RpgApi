Add-Type -TypeDefinition @"

namespace RpgApi
{
    public enum DiceType
    {
        Standard = 0,
        Fudge = 1,
        XWingAttack = 2,
        XWingDefense = 3

    }
}
"@
$Global:ApiUrl = "http://localhost:61771/"
#$Global:ApiUrl = "http://10.0.0.40/"


function Roll-Dice
{
    Param(
        [Parameter(
            Position = 0,
            ParameterSetName = "Standard")]
        [int[]] $NumberOfSidesArray = (2,4,6,8,10,12,20),

        [Parameter(
            Position = 0,
            ParameterSetName = "Special")]
        [RpgApi.DiceType] $DiceType = [RpgApi.DiceType]::Standard,

        [Parameter(
            Position = 1,
            ParameterSetName = "Standard")]
        [int[]] $NumberOfDiceArray = (2,2,2,2,2,2,2),

                [Parameter(
            Position = 1,
            ParameterSetName = "Special")]
        [int] $NumberOfDice = 1,

        [Parameter(
            Position = 2)]
        [string] $Url = $Global:ApiUrl + "api/DiceRoller"
        )

        if ($PSCmdlet.ParameterSetName -eq "Standard")
        {
            $body = @{}

            for ($i = 0; $i -le ($numberOfSidesArray.Count - 1); $i++)
            {
                $body += @{$numberOfSidesArray[$i].ToString() = $numberOfDiceArray[$i].ToString()}
            }

            $webRequest = Invoke-RestMethod -Uri $url -Body ($body | ConvertTo-Json) -Method Post -ContentType "application/json"

            Write-Output -InputObject ($webRequest | Sort-Object -Property NumberOfSides)
        }
        else
        {
            $body = @{diceType=$DiceType; numberOfDice=$NumberOfDice}

            $webRequest = Invoke-RestMethod -Uri $Url -Method Get -Body $body

            Write-Output -InputObject $webRequest
        }
}

Export-ModuleMember -Function Roll-Dice -Variable $Global:ApiUrl
