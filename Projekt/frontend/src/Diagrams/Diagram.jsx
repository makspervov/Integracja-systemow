import React, { useState, useEffect } from 'react';
import { Bar } from 'react-chartjs-2';
import yaml from 'js-yaml';
import styles from "./diagram.module.css"
import 'chart.js/auto';
import { toast } from 'react-toastify'


const Chart = () => {
    const [selectedCountry, setSelectedCountry] = useState('');
    const [selectedSector, setSelectedSector] = useState('');
    const [countries, setCountries] = useState([]);
    const [countryNames, setCountryNames] = useState([]);
    const [yearOptions, setYearOptions] = useState([]);
    const [sectorOptions, setSectorOptions] = useState([]);
    const [co2Percentage, setCo2Percentage] = useState(0);
    const [industryPercentage, setIndustryPercentage] = useState(0);
    const [forestAreaPercentage, setForestAreaPercentage] = useState(0);
    const [averagePercentage, setAveragePercentage] = useState(0);
    const [sectorData, setSectorData] = useState([]);
    const [tableDataCO2, setTableDataCO2] = useState([]);
    const [tableDataIndustry, setTableDataIndustry] = useState([]);
    const [tableDataForestArea, setTableDataForestArea] = useState([]);
    const [percentageCO2Industry, setPercentageCO2Industry] = useState(0);
    const [percentageCO2ForestArea, setPercentageCO2ForestArea] = useState(0);
    const isAuthenticated = localStorage.getItem('token');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const responseCO2Emissions = await fetch('http://localhost:3000/api/co2-emissions');
                const responseCO2EmissionsBySector = await fetch('http://localhost:3000/api/co2-emissions-by-sector');
                const responseForestArea = await fetch('http://localhost:3000/api/forest-area');

                if (
                    !responseCO2Emissions.ok ||
                    !responseCO2EmissionsBySector.ok ||
                    !responseForestArea.ok
                ) {
                    throw new Error('Network error');
                }

                const dataCO2Emissions = await responseCO2Emissions.json();
                const dataCO2EmissionsBySector = await responseCO2EmissionsBySector.json();
                const dataForestArea = await responseForestArea.json();

                const mergeCountries = (
                    co2EmissionsData,
                    co2EmissionsBySectorData,
                    forestAreaData
                ) => {
                    const mergedData = {};
                    co2EmissionsData.forEach((country) => {
                        const { Entity, Year, 'Annual CO2 emissions': emissions } = country;

                        const co2EmissionsBySector = co2EmissionsBySectorData.find(
                            (sector) => sector.Entity === Entity && sector.Year === Year
                        );

                        const forestArea = forestAreaData.find(
                            (forest) => forest.Entity === Entity && forest.Year === Year
                        );

                        const countryData = {
                            Year,
                            emissions,
                            Buildings: co2EmissionsBySector ? co2EmissionsBySector.Buildings : 0,
                            Industry: co2EmissionsBySector ? co2EmissionsBySector.Industry : 0,
                            'Land-use change and forestry': co2EmissionsBySector ? co2EmissionsBySector['Land-use change and forestry'] : 0,
                            'Other fuel combustion': co2EmissionsBySector ? co2EmissionsBySector['Other fuel combustion'] : 0,
                            Transport: co2EmissionsBySector ? co2EmissionsBySector.Transport : 0,
                            'Manufacturing and construction': co2EmissionsBySector ? co2EmissionsBySector['Manufacturing and construction'] : 0,
                            'Electricity and heat': co2EmissionsBySector ? co2EmissionsBySector['Electricity and heat'] : 0,
                            forestArea: forestArea ? forestArea['Forest area'] : 0,

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

                const mergedCountries = mergeCountries(
                    dataCO2Emissions.co2EmissionByCountryData,
                    dataCO2EmissionsBySector,
                    dataForestArea
                );

                setCountries(mergedCountries);

                const yearOptions = getYearOptions(dataCO2Emissions.co2EmissionByCountryData);
                setYearOptions(yearOptions);

                const countryNames = getCountryNames(dataCO2Emissions.co2EmissionByCountryData);
                setCountryNames(countryNames);


                const sectorOptions = getSectorOptions(dataCO2EmissionsBySector);
                setSectorOptions(sectorOptions);

                const filteredSectorData = mergedCountries.map((country) => {
                    const selectedCountryData = country.data.find((data) => data.Year === selectedCountry);
                    console.log(selectedCountryData);
                    const sectorValue = selectedCountryData && selectedCountryData[selectedSector];
                    return {
                        name: country.Entity,
                        value: sectorValue !== undefined ? sectorValue : 0,
                    };
                });
                
                setSectorData(filteredSectorData);
            } catch (error) {
                console.error(error);
            }
        };

        fetchData();
    }, []);

    const countryOptions = Array.from(new Set(countryNames));
    const getYearOptions = (co2EmissionsData) => {
        const years = co2EmissionsData.map((country) => country.Year);
        const uniqueYears = [...new Set(years)];
        return uniqueYears;
    };

    const getCountryNames = (co2EmissionsData) => {
        const countryNames = co2EmissionsData.map((country) => country.Entity);
        return countryNames;
    };

    const getSectorOptions = (co2EmissionsBySectorData) => {
        const sectors = co2EmissionsBySectorData.reduce((options, country) => {
            Object.keys(country).forEach((key) => {
                if (
                    key !== 'Entity' &&
                    key !== 'Year' &&
                    key !== 'Code' &&
                    key !== '_id' &&
                    !key.startsWith('$')
                ) {
                    options[key] = country[key];
                }
            });
            return options;
        }, {});
        

        return Object.keys(sectors);
    };

    const handleCountryChange = (event) => {
        setSelectedCountry(event.target.value);
    };

    const handleSectorChange = (event) => {
        setSelectedSector(event.target.value);
        console.log('Selected Sector:', event.target.value);
    };

    useEffect(() => {
        const filteredSectorData = countries.map((country) => {
            const selectedCountryData = country.data.find((data) => data.Year === selectedCountry);
            return {
                name: country.Entity,
                value: selectedCountryData ? selectedCountryData[selectedSector] : 0,
            };
        });

        setSectorData(filteredSectorData);
    }, [selectedCountry, selectedSector, countries]);


    const chartData = {
        labels: yearOptions,
        datasets: [
            {
                type: 'bar',
                label: `${selectedCountry} - ${selectedSector}`,
                data: countries
                    .filter((country) => country.Entity === selectedCountry)
                    .flatMap((country) => country.data.map((item) => item[selectedSector])),
                backgroundColor: 'rgba(75, 255, 192, 0.6)',
            },
            {
                type: 'bar',
                label: `${selectedCountry} - Annual CO2 Emissions`,
                data: countries
                    .filter((country) => country.Entity === selectedCountry)
                    .flatMap((country) => country.data.map((item) => item.emissions)),
                backgroundColor: 'rgba(192, 75, 192, 0.6)',
            },
            {
                type: 'bar',
                label: `${selectedCountry} - Forest Area`,
                data: countries
                    .filter((country) => country.Entity === selectedCountry)
                    .flatMap((country) => country.data.map((item) => item.forestArea)),
                backgroundColor: 'rgba(255, 67, 86, 0.6)',
            },
        ],
    };


    const chartOptions = {
        responsive: true,
        scales: {
            y: {
                type: 'linear',
                grid: {
                    drawBorder: true,
                    drawOnChartArea: true,
                    drawTicks: true,
                },
                ticks: {
                    display: true,
                    callback: function (value) {
                        return value.toString() + ' Mt';
                    },
                },
            },
            x: {
                grid: {
                    drawBorder: true,
                    drawOnChartArea: true,
                    drawTicks: true,
                },
                ticks: {
                    display: true,
                    text: 'Year',
                },
            },
        },
        plugins: {
            legend: {
                display: true,
            },
            tooltip: {
                enabled: true,
            },
            scales: {
                y: {
                    type: 'linear',
                },
            },
        },
    };

    


    const calculatePercentages = () => {
        if (selectedCountry) {
          const countryData = countries.find((country) => country.Entity === selectedCountry);
          if (countryData) {
            const countryDataLatest = countryData.data[countryData.data.length - 1];
      
            if (countryDataLatest) {
              const co2Percentage = (countryDataLatest.co2Percentage * 100).toFixed(2);
              const industryPercentage = (countryDataLatest.industryPercentage * 100).toFixed(2);
              const forestAreaPercentage = (countryDataLatest.forestAreaPercentage * 100).toFixed(2);
              const averagePercentage = (
                (countryDataLatest.co2Percentage +
                  countryDataLatest.industryPercentage +
                  countryDataLatest.forestAreaPercentage) /
                3 *
                100
              ).toFixed(2);
      
              setCo2Percentage(co2Percentage);
              setIndustryPercentage(industryPercentage);
              setForestAreaPercentage(forestAreaPercentage);
              setAveragePercentage(averagePercentage);
      
              const tableDataCO2 = countryData.data.map((item) => ({
                year: item.Year,
                value: item.emissions !== undefined ? item.emissions : 0,
              }));
      
              const tableDataIndustry = countryData.data.map((item) => ({
                year: item.Year,
                value: item.Industry !== undefined ? item.industry : 0,
                percentage: ((item.Industry / item.emissions) * 100).toFixed(2),
              }));
      
              const tableDataForestArea = countryData.data.map((item) => ({
                year: item.Year,
                value: item.forestArea !== undefined ? item.forestArea : 0,
                percentage: ((item.emissions / item.forestArea) * 100).toFixed(2),
              }));
      
              const percentageCO2Industry = (
                (countryDataLatest.emissions / countryDataLatest.industryPercentage) *
                100
              ).toFixed(2);
      
              const percentageCO2ForestArea = (
                (countryDataLatest.co2Percentage / countryDataLatest.forestAreaPercentage) *
                100
              ).toFixed(2);
      
              setTableDataCO2(tableDataCO2);
              setTableDataIndustry(tableDataIndustry);
              setTableDataForestArea(tableDataForestArea);
              setPercentageCO2Industry(percentageCO2Industry);
              setPercentageCO2ForestArea(percentageCO2ForestArea);
      
              console.log(tableDataCO2);
              console.log(tableDataIndustry);
              console.log(tableDataForestArea);
              console.log(percentageCO2Industry);
              console.log(percentageCO2ForestArea);
            }
          }
        } else {
          setCo2Percentage(0);
          setIndustryPercentage(0);
          setForestAreaPercentage(0);
          setAveragePercentage(0);
          setTableDataCO2([]);
          setTableDataIndustry([]);
          setTableDataForestArea([]);
          setPercentageCO2Industry(0);
          setPercentageCO2ForestArea(0);
        }
      };
      
      useEffect(() => {
        calculatePercentages();
      }, [selectedCountry]);
      
      
      const exportDataJSON = () => {
        if (!isAuthenticated) {
          toast.error('You need to log in to download this file');
          return;
        }
      
        const data = {
          country: selectedCountry,
          sector: selectedSector,
          co2Percentage: co2Percentage ? `${co2Percentage}%` : 'N/A',
          industryPercentage: industryPercentage ? `${industryPercentage}%` : 'N/A',
          forestAreaPercentage: forestAreaPercentage ? `${forestAreaPercentage}%` : 'N/A',
          averagePercentage: averagePercentage ? `${averagePercentage}%` : 'N/A',
          sectorData: tableDataIndustry,
          co2PercentageData: percentageCO2Industry,
          sectorPercentageData: tableDataCO2,
          forestAreaPercentageData: tableDataForestArea,
          percentageCO2Industry: percentageCO2Industry ? `${percentageCO2Industry}%` : 'N/A',
          percentageCO2ForestArea: percentageCO2ForestArea ? `${percentageCO2ForestArea}%` : 'N/A',
        };
      
        const jsonData = JSON.stringify(data, null, 2);
        const element = document.createElement('a');
        const file = new Blob([jsonData], { type: 'application/json' });
        element.href = URL.createObjectURL(file);
        element.download = 'data.json';
        document.body.appendChild(element);
        element.click();
      };
      
      const exportDataYAML = () => {
        if (!isAuthenticated) {
          toast.error('You need to log in to download this file');
          return;
        }
      
        const data = {
          country: selectedCountry,
          sector: selectedSector,
          co2Percentage: co2Percentage ? `${co2Percentage}%` : 'N/A',
          industryPercentage: industryPercentage ? `${industryPercentage}%` : 'N/A',
          forestAreaPercentage: forestAreaPercentage ? `${forestAreaPercentage}%` : 'N/A',
          averagePercentage: averagePercentage ? `${averagePercentage}%` : 'N/A',
          sectorData: tableDataIndustry,
          co2PercentageData: percentageCO2Industry,
          sectorPercentageData: tableDataCO2,
          forestAreaPercentageData: tableDataForestArea,
          percentageCO2Industry: percentageCO2Industry ? `${percentageCO2Industry}%` : 'N/A',
          percentageCO2ForestArea: percentageCO2ForestArea ? `${percentageCO2ForestArea}%` : 'N/A',
        };
      
        const yamlData = yaml.dump(data);
        const element = document.createElement('a');
        const file = new Blob([yamlData], { type: 'text/plain' });
        element.href = URL.createObjectURL(file);
        element.download = 'data.yaml';
        document.body.appendChild(element);
        element.click();
      };
      
      const exportDataXML = () => {
        if (!isAuthenticated) {
          toast.error('You need to log in to download this file');
          return;
        }
      
        const data = {
          country: selectedCountry,
          sector: selectedSector,
          co2Percentage: co2Percentage ? `${co2Percentage}%` : 'N/A',
          industryPercentage: industryPercentage ? `${industryPercentage}%` : 'N/A',
          forestAreaPercentage: forestAreaPercentage ? `${forestAreaPercentage}%` : 'N/A',
          averagePercentage: averagePercentage ? `${averagePercentage}%` : 'N/A',
          sectorData: tableDataIndustry,
          co2PercentageData: percentageCO2Industry,
          sectorPercentageData: tableDataCO2,
          forestAreaPercentageData: tableDataForestArea,
          percentageCO2Industry: percentageCO2Industry ? `${percentageCO2Industry}%` : 'N/A',
          percentageCO2ForestArea: percentageCO2ForestArea ? `${percentageCO2ForestArea}%` : 'N/A',
        };
      
        const xml = generateXML(data);
        const blob = new Blob([xml], { type: 'text/xml' });
        const downloadLink = document.createElement('a');
        downloadLink.href = URL.createObjectURL(blob);
        downloadLink.download = 'data.xml';
        document.body.appendChild(downloadLink);
        downloadLink.click();
      };
      
      function generateXML(data) {
        let xml = '<?xml version="1.0" encoding="UTF-8"?>';
        xml += '<data>';
      
        for (const key in data) {
          if (data.hasOwnProperty(key)) {
            if (Array.isArray(data[key])) {
              xml += `<${key}>`;
              data[key].forEach((item) => {
                xml += '<item>';
                for (const prop in item) {
                  if (item.hasOwnProperty(prop)) {
                    xml += `<${prop}>${item[prop]}</${prop}>`;
                  }
                }
                xml += '</item>';
              });
              xml += `</${key}>`;
            } else {
              xml += `<${key}>${data[key]}</${key}>`;
            }
          }
        }
      
        xml += '</data>';
        return xml;
      }
      
      


    return (
        <div className={styles.container}>
            <h2 className={styles.title}>CO2 concentrations in relation to industry size and forest area in different countries of the world.</h2>

            <div className={styles.selectContainer}>
                <label>
                    Select Country:
                    <select
                        value={selectedCountry}
                        onChange={handleCountryChange}
                        className={styles.select}
                    >
                        <option value="">-- Select Country --</option>
                        {countryOptions.map((country, index) => (
                            <option value={country} key={index}>
                                {country}
                            </option>
                        ))}
                    </select>
                </label>
            </div>

            <div className={styles.selectContainer}>
                <label>
                    Select CO2 Sector:
                    <select
                        value={selectedSector}
                        onChange={handleSectorChange}
                        className={styles.select}
                    >
                        <option value="">-- Select Sector --</option>
                        {sectorOptions.map((sector, sectorIndex) => (
                            <option value={sector} key={sectorIndex}>
                                {sector}
                            </option>
                        ))}
                    </select>
                </label>
            </div>

            <div className={styles.chartContainer}>
                {sectorData.length > 0 ? (
                    <Bar data={chartData} options={chartOptions} />
                ) : (
                    <div>No data available</div>
                )}
            </div>
            <>
                {isAuthenticated ? (
                    <>

                        <button className={styles.exportButton} onClick={exportDataJSON}>
                            Export JSON
                        </button>
                        <button className={styles.exportButton} onClick={exportDataYAML}>
                            Export YAML
                        </button>
                        <button className={styles.exportButton} onClick={exportDataXML}>
                            Export XML
                        </button>
                    </>
                ) : (
                    <>
                        <button className={styles.disableButton} disabled>
                            Export JSON
                        </button>
                        <button className={styles.disableButton} disabled>
                            Export YAML
                        </button>
                        <button className={styles.disableButton} disabled>
                            Export XML
                        </button>
                    </>
                )}
            </>

        </div>
    );
};

export default Chart;