//package org.example;
//
//import org.json.JSONArray;
//import org.json.JSONObject;
//
//import java.io.BufferedReader;
//import java.io.InputStream;
//import java.io.InputStreamReader;
//import java.net.URL;
//import java.util.stream.Collectors;
//
//public class Main {
//    public static void main(String[] args) {
//        try {
//            String temp_url = "http://localhost/IS_LAB6_RESTAPI/cities/read/";
//
//            URL url = new URL(temp_url);
//            System.out.println("Wysyłanie zapytania...");
//
//            InputStream is = url.openStream();
//
//            System.out.println("Pobieranie odpowiedzi...");
//
//            String source = new BufferedReader(new InputStreamReader(is)).lines().collect(Collectors.joining("\n"));
//
//            System.out.println("Przetwarzanie danych...");
//            JSONObject json = new JSONObject(source);
//            JSONArray receivedData = (JSONArray) json.get("cities");
//
//            System.out.println("\nCities name: ");
//            int[] CityID = {3425, 3427, 3429, 3431, 3433};
//            for (int i: CityID) {
//                JSONObject city = receivedData.getJSONObject(i);
//
//                System.out.println("City: " + city.getString("Name"));
//                System.out.println("ID: " + city.getInt("ID"));
//                System.out.println("Country: " + city.getString("CountryCode"));
//                System.out.println("District: " + city.getString("District"));
//                System.out.println("Population: " + city.getInt("Population"));
//                System.out.println("\n---------------\n");
//            }
//        }
//        catch (Exception e) {
//            System.err.println("Wystąpił nieoczekiwany błąd!!!");
//            e.printStackTrace(System.err);
//        }
//    }
//}

package org.example;

import java.util.List;

public class Main {
    public static void main(String[] args) {
        try {
            // Test działania lokalnego REST API
            String temp_url = "http://localhost/IS_LAB6_RESTAPI/cities/read";

            JsonReader jsonReader = new JsonReader();
            List<City> cities = jsonReader.readCitiesFromUrl(temp_url);

            System.out.println("Przetwarzanie danych...\n");
            for (City city : cities) {
                System.out.println("City name: " + city.getName());
                System.out.println("ID: " + city.getId());
                System.out.println("CountryCode: " + city.getCountryCode());
                System.out.println("District: " + city.getDistrict());
                System.out.println("Population: " + city.getPopulation());
                System.out.println("\n---------------\n");
            }

        } catch (Exception e) {
            System.err.println("Wystąpił nieoczekiwany błąd!!!");
            e.printStackTrace(System.err);
        }
    }
}

