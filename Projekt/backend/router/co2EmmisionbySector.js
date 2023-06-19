const express = require('express');
const router = express.Router();
const { CO2EmissionBySector } = require('../models/models');

router.get('/co2-emissions-by-sector', async (req, res) => {
  try {
    const emissions = await CO2EmissionBySector.find();
    res.json(emissions);
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Something went wrong' });
  }
});

module.exports = router;
