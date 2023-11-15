## Instruktioner

I detta projekt är tanken att du ska bygga en mer avancerad Console-app i C# som har databaskopplingar. Du behöver använda färdigheter från flera av de tidigare Workshop-uppgifterna och teori som läraren gått igenom. Ni jobbar i grupper men det är viktigt att alla är med och bygger appen och förstår logiken ni som grupp skrivit. Du förväntas kunna förklara ungefär vad varje rad kod gör i erat projekt.

### Vad du ska göra

Du ska skapa en bankomat, eller kanske mer av en enkel internetbank! Det kommer bli ett utmanande projekt där ni behöver strukturera er kod ordentligt för att undvika att det blir rörigt.

### **Start av programmet och inloggning**

- När programmet startar ska användaren välkomnas till banken
- Användaren ska mata in sitt användarnummer/användarnamn (valfritt hur detta ser ut) och en pin-kod som ska avgöra vilken användare det är som vill använda bankomaten
- Om användaren är “admin” så ska man komma till en vy där man kan lägga till användare

### Navigera som admin

- Efter inloggning ska admin se en lista på användare och få ett val att lägga till flera användare

### **Navigera som användare**

- När användaren lyckats logga in ska bankomaten fråga vad användaren vill göra. Det ska finnas fyra val:

```
1. Se dina konton och saldo
2. Överföring mellan konton
3. Ta ut pengar
4. Sätt in pengar
5. Öppna nytt konto
6. Logga ut
```

- Användaren ska kunna välja en av funktionerna ovan genom att skriva in en siffra.
- Alternativt kan man låta användaren välja med piltangenterna, detta gäller samtliga menyer i banken.
- När en funktion har kört klart ska användaren få upp texten "Tryck enter för att komma till huvudmenyn". När användaren tryckt enter kommer menyn upp igen.
- Om användaren väljer "Logga ut" ska programmet inte stänga av. Användaren ska komma till inloggningen igen.
- Om användaren skriver ett nummer som inte finns i menyn, eller något annat än ett nummer, ska systemet meddela att det är ett "ogiltigt val".

### **Se konton och saldo**

- Denna funktion ska köras när användaren navigerat in till alternativet "Se dina konton och saldo"
- Användaren ska få en utskrift av de olika konton som användaren har och hur mycket pengar det finns på dessa
- Konton ska kunna ha både kronor och ören
- Alla användare ska ha olika antal konton och alla ska ha minst ett konto
- Varje konto ska ha ett namn ex. "lönekonto" eller "sparkonto"

### **Överföring mellan konton**

- Denna funktion ska köras när användaren navigerat in till alternativet "Överföring mellan konton"
- Användaren ska kunna välja ett konto att ta pengar från, ett konto att flytta pengarna till och sen en summa som ska flyttas mellan dessa
- Denna summa ska sedan flyttas mellan dessa konton och efteråt ska användaren få se vilken summa som finns på dessa två konton som påverkades
- Det måste finnas täckning på konton man vill flytta pengar från för beloppet man vill flytta
- Det räcker att man kan överföra mellan sina egna konton här

### **Ta ut pengar**

- Denna funktion ska köras när användaren navigerat in till alternativet "Ta ut pengar"
- Användaren ska kunna välja ett av sina konton samt en summa
- Efter detta måste användaren skriva in sin pinkod för att bekräfta att de vill ta ut pengar
- Pengarna ska sedan tas bort från det konto som valdes
- Sist av allt ska systemet skriva ut det nya saldot på det kontot

### Sätt in pengar

- Denna funktion ska köras när användaren navigerat in till alternativet “Sätt in pengar”
- Användaren ska kunna välja ett av sina konton samt en summa
- (vi låtsas att användaren lägger in pengar i automaten här)
- Pengarna ska sättas in på kontot som valdes
- Sist av allt ska systemet skriva ut det nya saldot på det kontot

### Öppna nytt konto

- Denna funktion ska köras när användaren navigerat in till alternativet “Öppna nytt konto”
- Användaren ska kunna skriva in ett namn på kontot
- Systemet ska skapa ett nytt konto för användaren

**Extrautmaningar**

_Om du känner att du hinner och vill göra mer kommer här förslag på ytterligare funktionalitet du kan bygga in i systemet. Dessa utmaningar är helt frivilliga och inget krav!_

- Gör så att olika konton har olika valuta, inklusive att valuta omvandlas när pengar flyttas mellan dem
- Lägg till så att om användaren skriver fel pinkod tre gånger stängs inloggning för den användaren av i tre minuter istället för att programmet måste starta om.

### **Övergripande flödesschema för programmet för vanlig användare**

![bank_flowchart.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/76ff052e-5519-4c95-9c8a-d50fbda370ef/48ec3bca-fc8f-43ec-bdec-2f17553e9dfe/bank_flowchart.png)

**👉  G-kriterier**

- Projektet ska byggas i Visual Studio med C# och .NET Core 6 som en Console Application
- Alla namn på filer, variabler, metoder etc ska vara på engelska
- Projektet måste innehålla flera olika typer av datatyper varav array måste vara en av dessa
- Projektet måste använda flera typer av programstrukturer/programflöden; villkor och loopar
- Projektet måste innehålla minst flera olika metoder/funktioner som du skapat själv
- Projektet måste versionshanteras med Git. Du ska ha sparat löpande till Github under arbetet
- Det ska finnas en del kommentarer i koden. Dels som förklarar vad varje metod eller del av koden gör (ex. de olika funktionerna i programmet) samt kommentarer för kodrader som inte är helt uppenbara vad de gör eller hur de fungerar.

**👉  VG-kriterier**

- Lägg till så att användare kan flytta pengar sinsemellan, dvs mellan olika användare
- Se till att du har bra commit-meddelanden i din Git så det går att förstå vad du lagt till i varje version.
- Lägga in grundläggande innehåll i den Readme-fil som finns i erat Git-repository på Github så att någon som ser projektet för första gången får en kort introduktion till strukturen i koden.
- Skriv en **********\*\***********individuell**********\*\*********** (en per person) reflektion/ett resonemang där du motiverar för hur du valt att bygga upp ditt program. Du ska alltså resonera kring den lösning du valt, vilka andra du övervägt och kritiskt granska ditt val och eventuellt motivera för bättre lösningar som du ser men inte gjort. Var noga med att gå igenom den tekniska lösning ni valt Inlämningen för reflektionen är separat och individuell.

## Din inlämning

- En länk till ditt repository som ska vara publikt, innehållandes **all kod** + en **README.md**

## Kriterier för bedömning

Endast Icke Godkänd (IG), Godkänd (G) eller Väl Godkänd (VG)

### Godkänd (G)

- Din bankapp uppfyller alla punkter under G-kriterier

### Väl Godkänd (VG)

- Din bankapp uppfyller förutom alla punkter under G-kriterier, även alla punkter under VG-kriterier
