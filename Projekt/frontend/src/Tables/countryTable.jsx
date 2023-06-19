import React, { useState, useEffect } from 'react';
import styles from "../styles/table/table.module.css"
const CountryTable = () => {
  const [countries, setCountries] = useState([]);
  const [searchText, setSearchText] = useState('');
  useEffect(() => {
    const fetchCountries = async () => {
      try {
        const responseCO2Emissions = await fetch('http://localhost:3000/api/co2-emissions');
        const responseCO2EmissionsBySector = await fetch('http://localhost:3000/api/co2-emissions-by-sector');
        const responseForestArea = await fetch('http://localhost:3000/api/forest-area');
        

        if (!responseCO2Emissions.ok || !responseCO2EmissionsBySector.ok || !responseForestArea.ok ) {
          throw new Error('Błąd sieciowy');
        }

        const dataCO2Emissions = await responseCO2Emissions.json();
        const dataCO2EmissionsBySector = await responseCO2EmissionsBySector.json();
        const dataForestArea = await responseForestArea.json();
        
        console.log(dataCO2Emissions); // Sprawdź dane w konsoli

        const mergedCountries = mergeCountries(dataCO2Emissions.co2EmissionByCountryData, dataCO2EmissionsBySector, dataForestArea);
        setCountries(mergedCountries);
      } catch (error) {
        console.error(error);
      }
    };

    const mergeCountries = (co2EmissionsData, co2EmissionsBySectorData, forestAreaData) => {
      const mergedData = {};
      co2EmissionsData.forEach((country) => {
        const {
          Entity,
          Year,
          'Annual CO2 emissions': emissions,

        } = country;


        const co2EmissionsBySector = co2EmissionsBySectorData.find(item => item.Entity === Entity && item.Year === Year);
        const forestArea = forestAreaData.find(item => item.Entity === Entity && item.Year === Year);
       


        const countryData = {
          Year,
          emissions,
          Buildings: co2EmissionsBySector ? co2EmissionsBySector.Buildings : 'brak',
          Industry: co2EmissionsBySector ? co2EmissionsBySector.Industry : 'brak',
          landUseChange: co2EmissionsBySector ? co2EmissionsBySector['Land-use change and forestry'] : 'brak',
          otherFuelCombustion: co2EmissionsBySector ? co2EmissionsBySector['Other fuel combustion'] : 'brak',
          Transport: co2EmissionsBySector ? co2EmissionsBySector.Transport : 'brak',
          manufacturingConstruction: co2EmissionsBySector ? co2EmissionsBySector['Manufacturing and construction'] : 'brak',
          electricityHeat: co2EmissionsBySector ? co2EmissionsBySector['Electricity and heat'] : 'brak',
          forestArea: forestArea ? forestArea['Forest area'] : 'brak',
          
        };

        if (Entity in mergedData) {
          mergedData[Entity].data.push(countryData);
        } else {
          mergedData[Entity] = {
            Entity,
            showInfo: true,
            data: [countryData],
          };
        }
      });

      return Object.values(mergedData);
    };
    fetchCountries();
  }, []);



  const handleShowInfo = (countryIndex) => {
    setCountries((prevCountries) => {
      const updatedCountries = prevCountries.map((country, index) => {
        if (index === countryIndex) {
          return {
            ...country,
            showInfo: !country.showInfo,
          };
        }
        return country;
      });
      return updatedCountries;
    });
  };

  const handleSearch = (e) => {
    setSearchText(e.target.value);
  };

  const filteredCountries = countries.filter((country) => {
    return country.Entity.toLowerCase().includes(searchText.toLowerCase());
  });

  return (
    <div>
      <br/>
      <div style={{ fontSize: '16px', fontWeight: 'bold', marginBottom: '10px', textTransform: 'uppercase', textAlign: 'center' }}>
        CO2 concentration in different countries of the world
      </div>
      
      <div className={styles.tableContainer}>
        <table className={styles.table}>
        <thead>
          <tr>
            <th>Country</th>
            <th><input type="text" value={searchText} onChange={handleSearch} placeholder="Search" /></th>
          </tr>
        </thead>
        <tbody>
        {filteredCountries.map((country, index) => (
            <tr key={index}>
              <td>{country.Entity}</td>
              <td>
                {country.showInfo ? (
                  <>
                    <button onClick={() => handleShowInfo(index)}>Hide information</button>
                    <table>
                      <thead>
                        <tr>
                          <th>Year</th>
                          <th>CO2 Emissions</th>
                          <th>Buildings</th>
                          <th>Industry</th>
                          <th>Land-use change and forestry</th>
                          <th>Other fuel combustion</th>
                          <th>Transport</th>
                          <th>Manufacturing and construction</th>
                          <th>Electricity and heat</th>
                          <th>Forest area</th>
                          
                        </tr>
                      </thead>
                      <tbody>
                        {country.data.map((data, dataIndex) => (
                          <tr key={dataIndex}>
                            <td>{data.Year}</td>
                            <td>{data.emissions}</td>
                            <td>{data.Buildings || 'brak'}</td>
                            <td>{data.Industry || 'brak'}</td>
                            <td>{data.landUseChange || 'brak'}</td>
                            <td>{data.otherFuelCombustion || 'brak'}</td>
                            <td>{data.Transport || 'brak'}</td>
                            <td>{data.manufacturingConstruction || 'brak'}</td>
                            <td>{data.electricityHeat || 'brak'}</td>
                            <td>{data.forestArea || 'brak'}</td>
                            
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </>
                ) : (
                  <button onClick={() => handleShowInfo(index)}>Show information</button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      </div>
    </div>
  );
};

export default CountryTable;
