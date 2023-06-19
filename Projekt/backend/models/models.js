const mongoose = require('mongoose');

const co2EmissionByCountrySchema = new mongoose.Schema({
  Entity: String,
  Code: String,
  Year: Number,
  'Annual CO2 emissions': Number,
});

const co2EmissionBySectorSchema = new mongoose.Schema({
  Entity: String,
  Code: String,
  Year: Number,
  Buildings: Number,
  Industry: Number,
  'Land-use change and forestry': Number,
  'Other fuel combustion': Number,
  Transport: Number,
  'Manufacturing and construction': Number,
  'Electricity and heat': Number,
});

const forestAreaByCountrySchema = new mongoose.Schema({
  Entity: String,
  Code: String,
  Year: Number,
  'Forest area': Number,
});

const landAreaSchema = new mongoose.Schema({
  Entity: String,
  Code: String,
  Year: Number,
  'Land area (sq km)': Number,
});

const CO2EmissionByCountry = mongoose.model('CO2EmissionByCountry', co2EmissionByCountrySchema);
const CO2EmissionBySector = mongoose.model('CO2EmissionBySector', co2EmissionBySectorSchema);
const ForestAreaByCountry = mongoose.model('ForestAreaByCountry', forestAreaByCountrySchema);
const LandArea = mongoose.model('LandArea', landAreaSchema);

module.exports = {
  CO2EmissionByCountry,
  CO2EmissionBySector,
  ForestAreaByCountry,
  LandArea,
  
};
