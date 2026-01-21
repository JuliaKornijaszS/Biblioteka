# Dokumentacja projektu – System zarządzania biblioteką 

> Autor: Julia Kornijasz

---

## Informacje ogólne
```
Nazwa projektu: Biblioteka 
Typ aplikacji: Aplikacja webowa ASP.NET Core MVC  
```
Technologie: 
- .NET (ASP.NET Core MVC) 

- Entity Framework Core 

- SQLite 

- ASP.NET Core Identity (logowanie i role użytkowników) 

- HTML + CSS (Bootstrap) 

Projekt przedstawia prosty system do zarządzania biblioteką, umożliwiający przeglądanie książek, autorów i kategorii oraz zarządzanie nimi przez administratora.  

---

## Wymagania systemowe

Do uruchomienia projektu wymagane są:
- Visual Studio 2022 lub nowsze  
- .NET SDK 8.0 lub nowszy  
- Windows / macOS / Linux  
- Przeglądarka internetowa (Chrome, Edge, Firefox itp.)  
- Połączenie z Internetem (do pobrania pakietów NuGet przy pierwszym uruchomieniu)

---

## Instalacja projektu

1. Pobierz projekt z repozytorium GitHub  
   (Code → Download ZIP lub klonowanie repozytorium)

2. Otwórz plik rozwiązania `.sln` w Visual Studio  

3. Przy pierwszym uruchomieniu Visual Studio automatycznie przywróci pakiety NuGet  

4. Uruchom projekt przyciskiem **Start**  

5. Aplikacja będzie dostępna pod adresem podobnym do: https://localhost:xxxx/

---

## Konfiguracja bazy danych

Aplikacja korzysta z lokalnej bazy danych SQLite. 

Łańcuch połączenia znajduje się w pliku: 

> appsettings.json 
 

Przykład konfiguracji: 

```
"ConnectionStrings": { 
 "DefaultConnection": "Data Source=biblioteka.db" 
} 
```

Plik bazy danych biblioteka.db tworzony jest automatycznie przy pierwszym uruchomieniu aplikacji. 

Baza danych zawiera tabele m.in.: 

- Autorzy 

- Kategorie 

- Ksiazki 

- Wypozyczenia 

- AspNetUsers, AspNetRoles (obsługa logowania i ról użytkowników) 

---
 
## Konta testowe (użytkownicy) 

W aplikacji dostępne są role użytkowników: 

**Administrator:** 

Login: julka.kornijasz@gmail.com 

Hasło: Admin123! 

Uprawnienia: 

- dodawanie, edycja i usuwanie książek 

- dodawanie, edycja i usuwanie autorów 

- dodawanie, edycja i usuwanie kategorii 

- dodawanie, edycja i usuwanie wypożyczeń 

**Zwykły użytkownik (User):**

Login: user@biblioteka.pl 

Hasło: Admin123! 

Uprawnienia: 

- Może przeglądać listy i szczegóły 

- Nie ma dostępu do dodawania, edycji i usuwania danych

---

## Opis działania aplikacji (z punktu widzenia użytkownika) 

**Strona główna**

Po uruchomieniu aplikacji użytkownik widzi listę książek. W górnym menu dostępne są zakładki: 

- Książki 

- Autorzy 

- Kategorie 

- Zaloguj / Wyloguj 

**Książki** 

Użytkownik może: 

- przeglądać listę książek 

- zobaczyć szczegóły książki (tytuł, rok wydania, autor, kategoria) 

Administrator dodatkowo może: 

- dodać nową książkę 

- edytować istniejącą książkę 

- usunąć książkę 

**Autorzy** 

Użytkownik może: 

- przeglądać listę autorów 

- zobaczyć szczegóły autora 

Administrator dodatkowo może: 

- dodawać autorów 

- edytować autorów 

- usuwać autorów

**Kategorie** 

Użytkownik może:

- przeglądać listę kategorii 

- zobaczyć szczegóły kategorii 

Administrator dodatkowo może: 

- dodawać kategorie 

- edytować kategorie 

- usuwać kategorie

---

## Walidacja danych 

W aplikacji zaimplementowano walidację danych, m.in.: 

- Pole Tytuł jest wymagane 

- Rok wydania musi mieścić się w zakresie od 1455 do bieżącego roku. 

- Formularze pokazują komunikaty błędów przy niepoprawnych danych. 

Dzięki temu użytkownik nie może zapisać błędnych informacji do bazy danych. 

---

## Podsumowanie 

Projekt „Biblioteka” jest kompletną aplikacją webową umożliwiającą zarządzanie danymi bibliotecznymi. Zawiera: 

- bazę danych 

- relacje między encjami 

- system logowania i ról 

- zabezpieczenia dostępu 

- walidację danych 

- czytelny interfejs użytkownika 
