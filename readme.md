Ifrit
=====

**Program demonstracyjny ze spotkania grupy z 24 maja 2016.**

Aplikacja składa się z dwóch solucji - Ifrit oraz Ifrit.Web. Pierwsza
z nich zawiera aplikację dla .NET Micro Framework dla Netduino2 Plus,
która odbiera dane z fotorezystora i przesyła przez Bluetooth do
komputera, na którym może zadziałać druga aplikacja: Ifrit.AzureBridge.

Ifrit.AzureBridge odbiera dane z Bluetootha i przesyła dalej do Azure
IoT Hub. Ifrit.UWP to aplikacja, która odczytuje dane o naświetleniu
z czujnika światła na telefonie i samodzielnie przesyła dane do Azure.

Obydwie muszą być poprawnie skonfigurowane poprzez SharedAccessToken,
który można łatwo wygenerować np. w [Device Explorer](https://github.com/Azure/azure-iot-sdks/releases/download/2016-05-20/SetupDeviceExplorer.msi).

Druga solucja, Ifrit.Web, to aplikacja ASP.NET Core 1.0 RC2, która
łączy się z bazą danych zbudowaną przez Streaming Analytics. W pliku
`appsettings.json` trzeba ustawić ConnectionString dla bazy danych.

## Streaming Analytics
Reguła SA, która była użyta w prezentacji:

```sql
SELECT
    AVG(Data) as data,
    System.Timestamp AS time,
    Device as device
INTO
    results
FROM
    iotdata
GROUP BY
    Device, TumblingWindow(second, 2)
```

Tworzy ona średnią z danych dla danego urządzenia spośród ostatnich
dwóch sekund i taki rekord opatruje datą utworzenia.