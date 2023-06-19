# Porównanie stężenia CO2 w zależności od wielkości przemysłu i powierzchni lasów

Ten projekt ma na celu analizę i porównanie poziomów stężenia CO2 w różnych krajach świata, biorąc pod uwagę wielkość ich przemysłu i zasięg ich obszarów leśnych. Projekt wykorzystuje Node.js dla backendu, React dla frontendu i MongoDB jako bazę danych.

## Struktura projektu

Struktura projektu jest następująca:

```
.
├── backend
│   ├── Dockerfile
|   ├── .dockerignore
│   ...
├── compose.yaml
├── Dockerfile (MongoDB)
├── collections
│   ├── ...
├── frontend
│   ├── ...
│   └── Dockerfile
└── README.md
```

### Aplikacja React z backendem NodeJS i bazą danych MongoDB

- Katalog `backend`: Zawiera kod backendu Node.js.
  - `index.js`: Punkt wejścia serwera backendu.
  - `router/`: Katalog do przechowywania tras API.
  - `models/`: Katalog do definiowania modeli MongoDB.

- Katalog `frontend`: Zawiera kod frontendu React.
  - `public/`: Katalog dla plików statycznych i szablonu HTML.
  - `src/`: Katalog dla komponentów React i logiki aplikacji.
    - `Diagrams/`: Katalog dla wyświetlania danych w postaci diagramu w aplikacji webowej.
    - `Menu/`: Katalog dla defniowania wyglądu strony głównej, nagłówka, stopki oraz paska bocznego.
    - `styles/`: Katalog dla defniowania stylu dla każdej strony.
    - `Tables/`: Katalog dla wyświetlania danych w postaci tabeli w aplikacji webowej.
    - `User/`: Katalog dla komponentów React związanych z rejestracją/logowaniem użytkownika do aplikacji oraz definiowania administratora aplikacji.

- `collections/`: Katalog dla plików związanych z bazą danych.
  - `init-db.js`: Konfiguracja eksportowania danych do kontenera MongoDB.

## Backend

Backend projektu jest zbudowany przy użyciu Node.js, środowiska uruchomieniowego JavaScript. Służy jako aplikacja po stronie serwera, która obsługuje żądania API i współdziała z bazą danych.

Backend wykorzystuje następujące zależności:

- **axios** (`^0.27.2`): Biblioteka używana do wykonywania żądań HTTP z frontendu do API backendu.
- **bcrypt** (`^5.1.0`): Biblioteka używana do haszowania haseł.
- **cors** (`^2.8.5`): Oprogramowanie pośredniczące, które umożliwia Cross-Origin Resource Sharing (CORS) dla serwera zaplecza.
- **dotenv** (`^16.3.1`): Moduł ładujący zmienne środowiskowe z pliku `.env` do pliku `process.env`.
- **express** (`^4.18.2`): Framework aplikacji webowych dla Node.js, używany do routingu i obsługi żądań HTTP.
- **joi** (`^17.9.2`): Biblioteka używana do walidacji danych i definiowania schematów.
- **joi-password-complexity** (`^5.1.0`): Wtyczka dla Joi, która waliduje wymagania dotyczące złożoności hasła.
- **jsonwebtoken** (`^9.0.0`): Biblioteka służąca do generowania i weryfikacji JSON Web Token (JWT) w celu uwierzytelniania.
- **mongoose** (`^7.3.0`): Biblioteka Object Data Modeling (ODM) dla MongoDB, używana do definiowania modeli i interakcji z bazą danych.
- **nodemon** (`^2.0.22`): Narzędzie deweloperskie, które automatycznie restartuje serwer po wykryciu zmian.
- **soap** (`^1.0.0`): Biblioteka używana do konsumowania usług sieciowych SOAP.

Kod zaplecza jest podzielony na trasy i modele. Trasy definiują punkty końcowe API, a modele definiują strukturę i zachowanie dokumentów MongoDB.

## Frontend

Frontend projektu został zbudowany przy użyciu React, popularnej biblioteki JavaScript do tworzenia interfejsów użytkownika. Zapewnia interaktywny interfejs dla użytkowników do przeglądania i analizowania danych dotyczących stężenia CO2.

Frontend wykorzystuje następujące zależności:

- **@babel/plugin-proposal-private-property-in-object** (`^7.21.11`): Wtyczka Babel do obsługi prywatnych właściwości w obiektach.
- **@testing-library/jest-dom** (`^5.16.5`): Biblioteka rozszerzająca Jest o niestandardowe dopasowania elementów DOM.
- **@testing-library/react** (`^13.4.0`): Biblioteka do testowania komponentów React przy użyciu przyjaznego API.
- **@testing-library/user-event** (`^13.5.0`): Biblioteka do symulowania zdarzeń użytkownika w scenariuszach testowych.
- **authentication-library** (`^0.1.6`): Biblioteka do obsługi uwierzytelniania w aplikacji.
- **axios** (`^0.27.2`): Popularna biblioteka do wykonywania żądań HTTP z poziomu frontendu.
- **axios-ntlm** (`^1.4.1`): Biblioteka do wykonywania uwierzytelnionych NTLM żądań HTTP za pomocą Axios.
- **browserify-zlib** (`^0.2.0`): Implementacja zlib do użytku z systemem modułów Browserify.
- **buffer** (`^6.0.3`): JavaScriptowa implementacja obiektu Buffer.
- **chart.js** (`^4.3.0`): Potężna biblioteka do tworzenia interaktywnych wykresów.
- **chartjs-adapter-moment** (`^1.0.1`): Adapter Moment.js dla Chart.js do pracy z danymi opartymi na czasie.
- **crypto** (`^1.0.1`): JavaScriptowa implementacja funkcji kryptograficznych.
- **crypto-browserify** (`^3.12.0`): Implementacja kryptograficzna dla przeglądarki.
- **https-browserify** (`^1.0.0`): Kompatybilna z browserify implementacja modułu https.
- **jquery** (`^3.7.0`): Szybka, mała i bogata w funkcje biblioteka JavaScript.
- **jss** (`^10.10.0`): Biblioteka arkuszy stylów CSS w JavaScript dla React.
- **os-browserify** (`^0.3.0`): Implementacja modułu os zgodna z browserify.
- **react** (`^18.2.0`): Biblioteka JavaScript do tworzenia interfejsów użytkownika.
- **react-chartjs-2** (`^5.2.0`): wrapper dla Chart.js umożliwiający używanie go z Reactem.
- **react-dom** (`^18.2.0`): Punkt wejścia dla Reacta do interakcji z DOM.
- **react-router-dom** (`^6.13.0`): Biblioteka routingu dla aplikacji React.
- **react-scripts** (`5.0.1`): Zestaw skryptów i konfiguracji używanych przez Create React App.
- **react-toastify** (`^9.1.3`): Biblioteka powiadomień dla aplikacji React.
- **reactstrap** (`^9.2.0`): Biblioteka dostarczająca komponenty Bootstrap dla Reacta.
- **router** (`^1.3.8`): Prosty i elastyczny router dla Node.js.
- **soap** (`^1.0.0`): Biblioteka do tworzenia klientów i serwerów SOAP.
- **stream-browserify** (`^3.0.0`): Implementacja modułu stream zgodna z browserify.
- **stream-http** (`^3.2.0`): Implementacja modułu http zgodna z browserify.
- **util** (`^0.12.5`): Zbiór często używanych funkcji użytkowych.
- **web-vitals** (`^2.1.4`): Biblioteka do pomiaru parametrów życiowych w JavaScript.
- **webpack** (`^5.87.0`): Potężny bundler modułów.
- **xml-js** (`^1.6.11`): Biblioteka do konwersji XML na JSON i odwrotnie.
- **xmlbuilder** (`^15.1.1`): Biblioteka do programowego tworzenia dokumentów XML.

## Baza danych

Projekt wykorzystuje MongoDB jako bazę danych do przechowywania i pobierania danych związanych ze stężeniem CO2, wielkością przemysłu i powierzchnią lasów w różnych krajach. Plik `collections/init-db.js` zawiera konfigurację do eksportowania danych do kontenera MongoDB.

## Konfiguracja i instalacja

Aby skonfigurować projekt lokalnie, wykonaj następujące kroki:

1. Sklonuj repozytorium: `git clone <repository-url>`.
2. Przejdź do katalogu projektu: `cd <katalog projektu>`.
3. Otwórz program Docker Desktop i terminał (systemy Linux) lub wiersz poleceń/Powershell (dla Windows)
4. Uruchom aplikację Docker Compose: `docker compose up -d`
5. Poczekaj, aż Docker Compose pobierze obrazy i skonfiguruje aplikację.
6. Po skonfigurowaniu usługi w przeglądarce wpisz następujący adres: `localhost:3001` 

Upewnij się, że dane zostały zaimportowane do MongoDB z `collections/init-db.js`, aby sprawdzić działanie aplikacji. Możesz zrobić to za pomocą polecenia `docker exec -it mongodb_database mongosh` i w wierszu poleceń MongoSH wpisz polecenie `show dbs`. Wynik działania powinien wygłądać jak na przykładzie poniżej:

```
test> show dbs
**CO2      1.26 MiB**
admin   40.00 KiB
config  72.00 KiB
local   80.00 KiB
```

Jeżeli natomiast rozmiar bazy CO2 różni się z tym, co podano na przykładzie, zaimportuj dane ręcznie:

1. W wierszu poleceń wpisz polecenie `docker exec -it mongodb_database bash`
2. Dla każdej kolekcji zaimportuj odpowiednie dane: `mongoimport --db CO2 --collection ${collection.name} --type json ${collection.file} --jsonArray`, gdzie:
    `${collection.name}` - nazwa kolekcji,
    `${collection.file}` - Ścieżka do pliku (domyślnie jest to `/docker-entrypoint-initdb.d/nazwa_pliku.json`)
    
