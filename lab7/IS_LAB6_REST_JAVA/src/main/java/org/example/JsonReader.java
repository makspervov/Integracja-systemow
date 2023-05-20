package org.example;

import org.json.JSONObject;
import org.json.JSONArray;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class JsonReader {
    public List<City> readCitiesFromUrl(String url) throws Exception {
        InputStream is = new URL(url).openStream();
        String source = new BufferedReader(new InputStreamReader(is)).lines().collect(Collectors.joining("\n"));

        JSONObject json = new JSONObject(source);
        JSONArray jsonArray = json.getJSONArray("cities");

        List<City> cities = new ArrayList<>();
        int[] CityID = {3425, 3427, 3429, 3431, 3433};
        for (int i: CityID) {
            JSONObject cityJson = jsonArray.getJSONObject(i);
            City city = new City();

            city.setId(cityJson.getInt("ID"));
            city.setName(cityJson.getString("Name"));
            city.setCountryCode(cityJson.getString("CountryCode"));
            city.setDistrict(cityJson.getString("District"));
            city.setPopulation(cityJson.getInt("Population"));
            cities.add(city);
        }
        return cities;
    }
}
