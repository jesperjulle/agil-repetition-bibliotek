# Bibliotek-API – Modul 8 repetition

Besvarelse af repetitionsopgaven i Agil udvikling (modul 8). API'et lader et medlem låne en bog. Opgaven dækker Git Flow, XP, unit tests, Moq, TDD, CI med GitHub Actions, Postman og Newman.

[![CI](https://github.com/jesperjulle/agil-repetition-bibliotek/actions/workflows/ci.yml/badge.svg?branch=develop)](https://github.com/jesperjulle/agil-repetition-bibliotek/actions/workflows/ci.yml?query=branch%3Adevelop)

## Struktur

- `src/Library.Core` – domæne: `Loan`, `ILoanRepository`, `IsbnValidator`, `LoanService`, `FeeCalculator`
- `src/Library.Api` – Web API (`POST /api/loans`, `GET /api/loans/{id}`)
- `tests/Library.Tests` – unit tests (MSTest + Moq), 18 tests
- `postman/` – collection med fem API-tests og environments til Postman (Local og CI)
- `.github/workflows/ci.yml` – pipeline: restore, build, unit tests, start API, Newman

## Kom i gang

```bash
dotnet build
dotnet test tests/Library.Tests
dotnet run --project src/Library.Api     # kører på http://localhost:5000
```

API-tests mod et nystartet API (data ligger kun i hukommelsen, så genstart API'et før hver kørsel):

```bash
npm install -g newman
newman run postman/Library.postman_collection.json -e postman/local.postman_environment.json
```

Gyldige ISBN-numre: `9780306406157`, `9781861972712`, `9780140449136`

To ting, der kan drille lokalt:

- **Port 5000 på macOS** bruges af AirPlay-modtageren. Start API'et med `--urls http://localhost:5050`, og giv Newman `--env-var baseUrl=http://localhost:5050`.
- **Kun nyere .NET end 8 installeret:** `dotnet test` og `dotnet run` kræver .NET 8-runtime. Sæt `export DOTNET_ROLL_FORWARD=Major` i terminalen, eller installér .NET 8.

## Sådan er opgaven løst

Hver del er lavet på sin egen feature-branch ud fra nyeste `develop` og flettet ind gennem en pull request med merge commit, så de enkelte commits er bevaret.

| Del | Branch | Pull request | Færdig når | Bevis |
|---|---|---|---|---|
| 1 Git Flow og XP | `main`, `develop` | – | `main` og `develop` findes lokalt og på GitHub | [Branches](https://github.com/jesperjulle/agil-repetition-bibliotek/branches) |
| 2 Unit tests | `feature/unit-tests` | [#1](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/1) | Fire input dækket af grønne, uafhængige tests | `IsbnValidatorTests.cs` (4 + 2 ekstra) |
| 3 Moq | `feature/moq` | [#2](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/2) | To nye tests grønne uden rigtigt repository | `LoanServiceTests.cs` (2 + 1 grænsetest) |
| 4 TDD | `feature/fee-calculator` | [#3](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/3) | Fem rækker grønne, `git log` viser red, green og refactor | [Commits i #3](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/3/commits) |
| 5 CI | `feature/ci` | [#4](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/4) | Workflowet kører grønt på `develop` | [Kørsel på develop](https://github.com/jesperjulle/agil-repetition-bibliotek/actions/runs/43843449914) |
| 5 Fejlende test | `feature/ci-fejlende-test` | [#5](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/5) (lukket uden merge) | En pull request er blevet blokeret af en fejlende test | Rød kørsel, afvist merge, rettet og grøn |
| 6 Postman | `feature/api-tests` | [#6](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/6) | Fem requests består i én kørsel efter genstart | `postman/Library.postman_collection.json` |
| 7 Newman i CI | `feature/newman` | [#7](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/7) | Newman-steppet grønt, resultater i Actions-loggen | [Kørsel med Newman-tabel](https://github.com/jesperjulle/agil-repetition-bibliotek/actions/runs/43844194968) |
| 7 Forkert statuskode | `feature/newman-forkert-statuskode` | [#8](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/8) (lukket uden merge) | En ændret statuskode gør jobbet rødt | Rød kørsel i Newman-steppet, rettet og grøn |

Ud over opgaven:

- [#9](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/9) står åben og rød med vilje, så blokeringen kan ses, mens den gælder. Med en indlogget `gh` svarer `gh api repos/jesperjulle/agil-repetition-bibliotek/pulls/9 --jq .mergeable_state` med `blocked` (afprøvet som repoets ejer). Uden login svarer API'et `unknown`, men pull requestens side viser det røde, påkrævede check.
- [#10](https://github.com/jesperjulle/agil-repetition-bibliotek/pull/10) er denne README.

TDD-forløbet kan ses med:

```bash
git log --oneline --reverse 8aad6f6..acebed7
```

Branch protection på `develop` kræver, at status-checket `build-and-test` er grønt før merge. Reglen gælder også administratorer.

## Refleksion

**Om besvarelsen.** Opgaven er løst af en AI (Claude) for gruppen som led i en AI-battle. Alle commits på feature-branches har AI'en som medforfatter. Det betyder tre ting, som afviger fra opgaven:

- Pair programming med driver og navigator er ikke praktiseret.
- Ingen pull request er formelt godkendt. Der er kun én konto, og GitHub lader ikke en bruger godkende sin egen pull request. I stedet er hver merget pull request hentet i en ren kopi, hvor testene er kørt, og resultatet står i en kommentar. For #6 og #7 er collectionen også kørt med Newman. For #4 er reviewet selve Actions-kørslen.
- Commits ligger tæt i tid, fordi en AI har skrevet dem. Hvert trin kan efterprøves ved at checke committen ud og køre `dotnet test`: en commit, der hedder `red:`, fejler, og `green:` og `refactor:` består.

**Ét feedback-system.** De syv dele er den samme idé i forskellig afstand fra koden. Unit testene svarer på millisekunder og fanger fejl i én klasse. Pull requesten er stedet, hvor et andet par øjne skulle have set med; her var det kun en ren kopi og en testkørsel. Pipelinen kører ved hver pull request og ved hvert push til `develop` og `main`, så testene ikke kan glemmes. Newman lægger API-testene oven på, så også ruter, JSON og statuskoder er dækket. I XP's sprog: værdien er feedback, princippet er hurtig feedback, og praktikkerne er TDD, continuous integration og pair programming, hvor den sidste her er erstattet af pull requests. Pipelinen kom først i del 5, så #1-#3 er merget uden automatisk check.

**En test skal have været rød.** Testene i del 2 og 3 er skrevet til kode, der allerede fandtes, så de bestod med det samme. En test, der aldrig har fejlet, kan være en test, der ikke tester noget. Derfor blev koden midlertidigt ødelagt 12 steder (fx grænsen for aktive lån sat til 99, eller kontrolcifferet ignoreret), og hver gang blev den forventede test rød. Ændringerne er ikke committet; resultaterne står i kommentarerne på #1, #2 og #3.

**TDD.** Reglen "ingen produktionskode uden en fejlende test" gav korte skridt: først `return 0m`, så et fast gebyr på 5 kr., og først da testen for 10 dage kom, en beregning pr. dag. Opgaven foreskriver formlen `(returned.Date - due.Date).Days`, men `.Date` kom først ind, da en test med klokkeslæt blev rød: forfald kl. 18 og aflevering næste morgen kl. 9 er kun 15 timer, og uden `.Date` kostede det 0 kr. Tre tests kunne ikke blive røde, fordi koden allerede dækkede dem: række 2 (afleveret før forfald) og de to grænsetests omkring loftet. De er committet som `test:` uden rød forgænger og er i stedet afprøvet ved at ødelægge den færdige kode. Uden tjekket for aflevering til tiden giver beregningen −25 kr., og række 2 fanger det. Refaktoreringerne (konstanterne `FeePerDay` og `MaxFee`, fælles forfaldsdato i testene) er lavet med grønne tests før og efter.

**Mock eller rigtigt API.** Moq-testene viser, at `LoanService` kaster en exception, når bogen er udlånt. De viser ikke, at controlleren oversætter exceptionen til 409. Det gør kun API-testen. Til gengæld er API-testene afhængige af rækkefølge og tilstand: de skal køres mod et nystartet API, og request 2 og 4 bygger på request 1. Unit testene har ingen af de bindinger. De to niveauer erstatter ikke hinanden.

**Fund i startkoden.** API-testene afslørede, at fejlformatet ikke er ens: 400 og 409 svarer med feltet `error`, mens 404 svarer med ASP.NET Cores problem details uden `error`. Testen i request 5 følger det, API'et gør. Derudover kan samme bog lånes to gange, hvis ISBN'et sendes med bindestreger den ene gang og uden den anden (afprøvet: begge kald gav 201), fordi controlleren gemmer ISBN'et, som det blev skrevet. Ingen af delene er rettet, da opgaven ikke beder om ændringer i API'et. Begge ville være oplagte næste opgaver at løse test-først.

**CI.** Den bevidst fejlende test i #5 gjorde jobbet rødt, og forsøget på at merge blev afvist med "the base branch policy prohibits the merge". I #8 blev Newman-steppet rødt af en ændret statuskode, mens unit testene stadig var grønne. Begge demonstrationer er holdt ude af `develop`. To detaljer i workflowet er værd at kunne forklare: `&` efter `dotnet run`, fordi API'et ellers blokerer steppet for evigt, og `curl --retry` i stedet for `sleep`, fordi en fast ventetid enten er for kort eller spildt.

**Begrænsninger.**
- Postman-appen er ikke brugt. Collectionen er skrevet direkte i formatet Collection v2.1 og kørt med Newman. Den er altså hverken kørt i Collection Runner eller eksporteret fra appen, som opgaven beskriver.
- `main` indeholder kun startkoden. I Git Flow kommer `develop` over i `main` gennem en release, og opgaven beder ikke om en. `develop` er derfor sat som standard-branch på GitHub.
- Pipelinen er continuous integration. Den pakker og udgiver ingenting, så der er hverken delivery eller deployment.
- .NET 8 er versionen fra startkoden. Microsofts support for .NET 8 udløb 10. november 2026, så et rigtigt projekt burde opgradere.
