const express = require('express');
const router = express.Router();
const { CO2EmissionByCountry, CO2EmissionBySector, ForestAreaByCountry, LandArea } = require('../models/models');

router.get('/co2-emissions', async (req, res) => {
  try {
    const co2EmissionByCountryData = await CO2EmissionByCountry.find();
    const co2EmissionBySectorData = await CO2EmissionBySector.find();
    const forestAreaByCountryData = await ForestAreaByCountry.find();
    const landAreaData = await LandArea.find();

    res.json({
      co2EmissionByCountryData: co2EmissionByCountryData,
      co2EmissionBySectorData: co2EmissionBySectorData,
      forestAreaByCountryData: forestAreaByCountryData,
      landAreaData: landAreaData
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Something went wrong' });
  }
});

module.exports = router;
