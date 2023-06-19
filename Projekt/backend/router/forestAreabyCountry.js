const express = require('express');
const router = express.Router();
const { ForestAreaByCountry } = require('../models/models');

router.get('/forest-area', async (req, res) => {
  try {
    const forestArea = await ForestAreaByCountry.find();
    res.json(forestArea);
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Something went wrong' });
  }
});

module.exports = router;
