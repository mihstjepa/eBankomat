# eBankomat

Ova jednostavna web aplikacija simulira rad "online" bankomata te omogućuje korisnicima prikaz stanja računa, prijenos, uplatu i isplatu određenih valuta novca.
Aplikacija je napravljena pomoću ASP.NET MVC arhitekture i služi kao demonstracija savladanih znanja autora.

## Kako isprobati aplikaciju na vašem računalu?
1. Preuzmite sve datoteke repozitorija
2. Napravite import baze podataka (MS SQL Server) na localhostu pomoću već exportirane baze podataka (https://github.com/mihstjepa/eBankomat/blob/master/DatabaseExport/aspnet-AutomatedTellerMachine-20191012024503.bacpac)
3. Otvorite datoteku "AutomatedTellerMachine.sln"
4. Pritisnite tipku F5

# Korisnički zahtjevi

## Uloge korisnika
Autentikacija korisnika provjerava se putem dodjeljenih uloga, a svaka ima određene ovlasti u smislu razine pristupa. Administrator ima sva prava kao i Običan član, uz određene dodatne ovlasti.

## Administrator
* Pregled ukupnih transakcija svih korisnika
* Pregled stanja tekućih računa korisnika

## Običan korisnik
* Uplata novca na tekući račun
* Isplata novca na tekući račun
* Brza isplata ($100)
* Ispis vlastitih transakcija
* Pregled stanja vlastitog tekućeg računa
* Prijenos (slanje) novca na tuđi račun

# Korištene tehnologije/alati
* Visual Studio 2017
* C#
* ASP.NET MVC5
* SQL Server Management Studio
* Bootstrap
* Entity Framework
* HTML5, CSS3

# ERA model
![](https://github.com/mihstjepa/eBankomat/blob/master/AutomatedTellerMachine/Content/ERAmodel.png)
