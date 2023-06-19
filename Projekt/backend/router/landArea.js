const express = require('express');
const router = express.Router();
const { LandArea } = require('../models/models');

router.get('/land-area', async (req, res) => {
  try {
    const landArea = await LandArea.find();
    res.json(landArea);
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Something went wrong' });
  }
});

module.exports = router;
